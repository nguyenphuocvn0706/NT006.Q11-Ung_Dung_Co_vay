using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Co_Vay
{
    public partial class Mode_Online : Form
    {
        public class UserStats
        {
            public int play_ai { get; set; }
            public int win_ai { get; set; }
            public int play_pvp { get; set; }
            public int win_pvp { get; set; }
        }
        // Kích thước bàn cờ
        private readonly int boardSize = 19;
        private readonly int cellSize = 30;
        private int localColor = 0;
        //Rule + time
        private string scoringRule = "Japanese";
        private int timePerSideMinutes = 30;
        // Engine cờ vây
        private Game_Engine game;

        // Firebase
        private FirebaseService firebase;
        private RealtimeDatabaseService realtimeDb;

        // Thông tin trận đấu
        private MatchInfo currentMatch;
        private string currentMatchId;
        private PlayerInfo localPlayer;

        // Đồng bộ trạng thái game (board) từ Firebase
        private System.Windows.Forms.Timer timerSync;

        // Đếm ngược thời gian cho mỗi bên
        private System.Windows.Forms.Timer timerPerTurn;

        private int blackRemainingSeconds; // bên phải - Đen
        private int whiteRemainingSeconds; // bên trái - Trắng

        // Thời điểm bắt đầu trận để tính 3 phút cho nút Quit
        private DateTime matchStartTime;

        // Trạng thái kết thúc trận
        private bool matchFinished = false;

        // Để phân biệt đóng form do code hay do người dùng bấm X
        private bool closingFromCode = false;

        // Theo dõi trạng thái pass lần trước để biết đối thủ vừa pass hay không
        private bool lastBlackPassed = false;
        private bool lastWhitePassed = false;

        // Chặn timerSync_Tick chạy chồng (async re-entrancy)
        private bool syncTickRunning = false;

        // Chặn mở nhiều popup "Opponent Passed"
        private bool opponentPassPopupShown = false;

        // Đã xử lý double pass chưa (chặn gọi nhiều lần)
        private bool matchFinishedByDoublePass = false;
        private bool matchEndHandled = false;

        // Xin hòa
        private bool drawOfferDialogShown = false;      // Hiện hộp thoại trả lời đề nghị hòa của đối thủ
        private bool localDrawOfferPending = false;     // Chờ đối thủ trả lời đề nghị hòa

        //  CHAT 
        private class ChatEntry
        {
            public string Id { get; set; }
            public ChatMessage Msg { get; set; }
        }

        private List<ChatEntry> chatMessages = new List<ChatEntry>();

        // =====================================================================
        //  CÁC CONSTRUCTOR
        // =====================================================================

        /// <summary>
        /// Constructor chính:
        /// - matchId: ID trận đấu (ID phòng)
        /// - localPlayer: thông tin người chơi hiện tại (Id, Name)
        /// </summary>
        public Mode_Online(string matchId, PlayerInfo localPlayer)
        {
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            // Khởi tạo Firebase
            firebase = new FirebaseService();
            realtimeDb = new RealtimeDatabaseService();

            // Khởi tạo Game Engine 19x19
            game = new Game_Engine(boardSize);
            game.DoublePassHappened += Game_DoublePassHappened;

            // Gán thông tin truyền vào
            this.currentMatchId = matchId;

            // LocalPlayer từ Game_Mode_Online / Online_Privatematch
            if (localPlayer == null)
            {
                MessageBox.Show(
                    "Mode_Online must receive PlayerInfo from Game_Mode_Online / Online_Privatematch.",
                    "Configuration error");

                Close();
                return;
            }

            this.localPlayer = localPlayer;

            // Double-buffer cho panel vẽ bàn cờ
            typeof(Panel).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty |
                BindingFlags.Instance |
                BindingFlags.NonPublic,
                null, pl_Board, new object[] { true });

            // Gắn event cho Panel
            pl_Board.Paint += pl_Board_Paint;
            pl_Board.MouseClick += pl_Board_MouseClick;

            // Event Form
            this.Load += Mode_Online_Load;
            this.FormClosing += Mode_Online_FormClosing;

            // Timer sync game từ Firebase
            timerSync = new System.Windows.Forms.Timer();
            timerSync.Interval = 500; // 0.5 giây
            timerSync.Tick += timerSync_Tick;

            // Timer đếm ngược cho từng bên
            timerPerTurn = new System.Windows.Forms.Timer();
            timerPerTurn.Interval = 1000;
            timerPerTurn.Tick += timerPerTurn_Tick;
        }

        // =====================================================================
        //  FORM LOAD: LẤY THÔNG TIN MATCH + GAME TỪ FIREBASE, KHỞI ĐỘNG ĐỒNG HỒ
        // =====================================================================

        private async void Mode_Online_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(currentMatchId))
            {
                MessageBox.Show("No match ID (MatchId).", "Error");
                CloseBackToGamemode();
                return;
            }

            // Lấy thông tin trận đấu từ Firebase
            currentMatch = await firebase.LoadMatchAsync(currentMatchId);
            if (currentMatch == null)
            {
                MessageBox.Show("Match information not found on the server.", "Error");
                CloseBackToGamemode();
                return;
            }
            //  chỉ cho 2 người thuộc trận được ở lại form 
            bool localIsBlack = currentMatch.BlackPlayer != null &&
                                currentMatch.BlackPlayer.Id == localPlayer.Id;
            bool localIsWhite = currentMatch.WhitePlayer != null &&
                                currentMatch.WhitePlayer.Id == localPlayer.Id;

            if (localIsBlack)
            {
                localColor = 1;   // Đen
            }
            else if (localIsWhite)
            {
                localColor = 2;   // Trắng
            }
            else
            {
                // Không thuộc trận đấu này → thoát,               
                CloseBackToGamemode();
                return;
            }

            // ==================================
            // Đọc ScoringRule + Time từ MatchInfo
            // ==================================
            if (currentMatch != null)
            {
                // Japanese / Chinese
                scoringRule = string.IsNullOrWhiteSpace(currentMatch.ScoringRule)
                    ? "Japanese"
                    :  currentMatch.ScoringRule;

                // Thời gian mỗi bên (phút)
                timePerSideMinutes = currentMatch.TimePerSideMinutes > 0
                    ? currentMatch.TimePerSideMinutes
                    : 30;
            }

            // Chuyển phút → giây (CHỈ set ở đây)
            blackRemainingSeconds = timePerSideMinutes * 60;
            whiteRemainingSeconds = timePerSideMinutes * 60;

            UpdateTimeLabels();

            // Hiển thị ID phòng
            lbl_IDroom.Text = currentMatch.MatchId;

            // Reset name
            txt_Name_Left.Text = "";
            txt_Name_Right.Text = "";

            // Người chơi bên trái: TRẮNG (WhitePlayer)
            if (currentMatch.WhitePlayer != null)
            {
                txt_Name_Left.Text = string.IsNullOrWhiteSpace(currentMatch.WhitePlayer.Name)
                    ? "Guest1"
                    : currentMatch.WhitePlayer.Name;

                txt_Level_Left.Text = "White";
            }

            // Người chơi bên phải: ĐEN (BlackPlayer)
            if (currentMatch.BlackPlayer != null)
            {
                txt_Name_Right.Text = string.IsNullOrWhiteSpace(currentMatch.BlackPlayer.Name)
                    ? "Guest2"
                    : currentMatch.BlackPlayer.Name;

                txt_Level_Right.Text = "Black";
            }
            // Thử đọc lại tên từ /users/{uid} để ghi đè nếu có
            await LoadPlayerNamesFromUserDbAsync();

            // Lấy trạng thái bàn cờ từ Firebase, nếu chưa có thì tạo mới
            var loadedGame = await firebase.LoadGameAsync(currentMatch.MatchId);
            if (loadedGame != null)
            {
                game = loadedGame;
            }
            else
            {
                // Trận mới, lưu board rỗng lên server
                await firebase.SaveGameAsync(currentMatch.MatchId, game);
            }

            // Chắc chắn gắn lại event DoublePass sau khi load
            game.DoublePassHappened += Game_DoublePassHappened;

            // Ghi nhận thời điểm bắt đầu trận (có thể coi là lúc 2 người đã vào trận)
            matchStartTime = DateTime.Now;

            // Panel bàn cờ bật visible
            pl_Board.Visible = true;
            pl_Board.Invalidate();

            // Bắt đầu timer
            timerSync.Start();
            timerPerTurn.Start();

            //Khởi tạo last-pass khi vừa load trận
            lastBlackPassed = game.BlackPassed;
            lastWhitePassed = game.WhitePassed;

            //Đồng bộ chat
            await LoadChatMessagesAsync();

        }

        // =====================================================
        //  LẤY TÊN NGƯỜI CHƠI TỪ NODE /users/{uid} TRÊN FIREBASE
        // =====================================================
        private async Task LoadPlayerNamesFromUserDbAsync()
        {
            if (currentMatch == null || realtimeDb == null)
                return;

            try
            {
                // BÊN TRẮNG (trái)
                if (currentMatch.WhitePlayer != null &&
                    !string.IsNullOrWhiteSpace(currentMatch.WhitePlayer.Id))
                {
                    var userW = await realtimeDb.GetUserAsync(currentMatch.WhitePlayer.Id);
                    if (userW != null && !string.IsNullOrWhiteSpace(userW.name))
                    {
                        // Ghi đè tên lấy được từ /users
                        txt_Name_Left.Text = userW.name;
                    }
                }

                // BÊN ĐEN (phải)
                if (currentMatch.BlackPlayer != null &&
                    !string.IsNullOrWhiteSpace(currentMatch.BlackPlayer.Id))
                {
                    var userB = await realtimeDb.GetUserAsync(currentMatch.BlackPlayer.Id);
                    if (userB != null && !string.IsNullOrWhiteSpace(userB.name))
                    {
                        txt_Name_Right.Text = userB.name;
                    }
                }
            }
            catch
            {
                // Nếu lỗi (mạng, quyền đọc, v.v...) thì giữ nguyên tên cũ trong MatchInfo
            }
        }

        // =====================================================================
        //  VẼ BÀN CỜ
        // =====================================================================
        private Point boardOrigin = new Point(30, 30); // điểm khởi đầu
        private void pl_Board_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var old = g.Transform;                 // lưu transform cũ
            g.TranslateTransform(boardOrigin.X, boardOrigin.Y);  // dịch gốc vẽ

            DrawGrid(g);
            DrawHoshi(g);

            if (game != null)
            {
                DrawStones(g);
            }

            g.Transform = old;
        }

        private void DrawGrid(Graphics g)
        {
            using (var pen = new Pen(Color.Black, 1.5f))
            {
                for (int i = 0; i < boardSize; i++)
                {
                    // Ngang
                    g.DrawLine(pen,
                        cellSize,
                        cellSize + i * cellSize,
                        cellSize * boardSize,
                        cellSize + i * cellSize);

                    // Dọc
                    g.DrawLine(pen,
                        cellSize + i * cellSize,
                        cellSize,
                        cellSize + i * cellSize,
                        cellSize * boardSize);
                }
            }
        }

        private void DrawHoshi(Graphics g)
        {
            int[] star = { 3, 9, 15 };
            foreach (int sx in star)
                foreach (int sy in star)
                {
                    int px = cellSize + sx * cellSize;
                    int py = cellSize + sy * cellSize;
                    g.FillEllipse(Brushes.Black, px - 4, py - 4, 8, 8);
                }
        }

        private void DrawStones(Graphics g)
        {
            if (game?.Board == null) return;

            for (int y = 0; y < boardSize; y++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    int v = game.Board[y, x];
                    if (v != 0)
                        DrawStone(g, x, y, v);
                }
            }
        }

        private void DrawStone(Graphics g, int x, int y, int color)
        {
            int px = x * cellSize + cellSize;
            int py = y * cellSize + cellSize;

            Rectangle r = new Rectangle(px - 15, py - 15, 30, 30);
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(r);
                using (PathGradientBrush pgb = new PathGradientBrush(path))
                {
                    if (color == 1) // Đen
                    {
                        pgb.CenterColor = Color.Gray;
                        pgb.SurroundColors = new Color[] { Color.Black };
                    }
                    else // Trắng
                    {
                        pgb.CenterColor = Color.White;
                        pgb.SurroundColors = new Color[] { Color.LightGray };
                    }

                    g.FillEllipse(pgb, r);
                    g.DrawEllipse(Pens.Black, r);
                }
            }
        }

        // =====================================================================
        //  CLICK LÊN BÀN CỜ ĐÁNH QUÂN
        // =====================================================================

        private async void pl_Board_MouseClick(object sender, MouseEventArgs e)
        {
            if (matchFinished || currentMatch == null || currentMatch.Status != MatchStatus.InProgress)
            {
                MessageBox.Show("The match does not exist.");
                return;
            }

            // localColor đã được xác định ở Mode_Online_Load
            if (localColor != 1 && localColor != 2)
            {
                return;
            }

            // Không phải lượt mình thì không cho đánh
            if (game.CurrentPlayer != localColor)
            {
                return;
            }

            // quy đổi tọa độ click về hệ tọa độ của bàn cờ (đã dịch)
            int localX = e.X - boardOrigin.X;
            int localY = e.Y - boardOrigin.Y;

            int x = (localX - cellSize / 2) / cellSize;
            int y = (localY - cellSize / 2) / cellSize;

            if (!game.InBounds(x, y)) return;

            if (!game.TryPlayMove(x, y, out var captured, out var error))
            {
                if (!string.IsNullOrEmpty(error))
                    MessageBox.Show(error);
                return;
            }

            pl_Board.Invalidate();

            // Lưu trạng thái game lên Firebase
            if (!string.IsNullOrEmpty(currentMatch.MatchId))
            {
                await firebase.SaveGameAsync(currentMatch.MatchId, game);
            }
        }

        // =====================================================================
        //  TIMER SYNC GAME TỪ FIREBASE + BẬT NÚT QUIT SAU 3 PHÚT
        // =====================================================================

        private async void timerSync_Tick(object sender, EventArgs e)
        {
            if (syncTickRunning) return;   // <<< CHỐT: chống chạy chồng
            syncTickRunning = true;

            try
            {
                if (matchFinished || currentMatch == null) return;

                // Đọc game từ Firebase
                var latest = await firebase.LoadGameAsync(currentMatch.MatchId);

                // Nếu games/{matchId} đã bị xoá => trận đã kết thúc hoặc bị huỷ
                if (latest == null)
                {
                    if (matchEndHandled) return;

                    matchEndHandled = true;
                    matchFinished = true;

                    timerSync.Stop();
                    timerPerTurn.Stop();

                    // luôn load MatchInfo mới nhất 
                    var updatedMatch = await firebase.LoadMatchAsync(currentMatch.MatchId);
                    if (updatedMatch != null)
                        currentMatch = updatedMatch;

                    if (updatedMatch != null && updatedMatch.Status == MatchStatus.Finished)
                    {
                        HandleRemoteMatchFinished(updatedMatch);
                    }
                    else
                    {
                    MessageBox.Show(
                        "The match has been canceled by the opponent.",
                        "Match Canceled");


                    CloseBackToGamemode();
                    }

                    return;
                }

                // luôn reload MatchInfo để cập nhật DrawRequested, Status, ...
                var latestMatch = await firebase.LoadMatchAsync(currentMatch.MatchId);
                if (latestMatch != null)
                    currentMatch = latestMatch;

                // Nếu trận đã kết thúc thì thôi
                if (currentMatch.Status == MatchStatus.Finished)
                    return;

                // Nếu mình đã gửi đề nghị hòa mà bây giờ không còn DrawRequested nữa
                // => đối thủ đã từ chối
                if (localDrawOfferPending && !currentMatch.DrawRequested)
                {
                    localDrawOfferPending = false;
                    MessageBox.Show("The opponent has declined the draw offer.", "Draw Offer");
                }

                // Kiểm tra double pass (nếu cả hai đã Pass ở engine)
                if (latest.BlackPassed && latest.WhitePassed)
                {
                    Game_DoublePassHappened();
                    return;
                }

                // Lưu trạng thái pass cũ
                bool prevBlackPassed = lastBlackPassed;
                bool prevWhitePassed = lastWhitePassed;

                // Cập nhật trạng thái pass mới NGAY (tránh return sớm làm mất cập nhật)
                lastBlackPassed = latest.BlackPassed;
                lastWhitePassed = latest.WhitePassed;

                // Thông báo: đối thủ bỏ lượt (chỉ bắn 1 lần cho 1 sự kiện pass)
                if (!matchFinished && (localColor == 1 || localColor == 2))
                {
                    int opponentColor = 3 - localColor;

                    bool opponentJustPassed =
                        (opponentColor == 1 && latest.BlackPassed && !prevBlackPassed) ||
                        (opponentColor == 2 && latest.WhitePassed && !prevWhitePassed);

                    if (opponentJustPassed && latest.CurrentPlayer == localColor && !opponentPassPopupShown)
                    {
                        opponentPassPopupShown = true;

                        // dừng timer khi popup mở để tránh mọi side-effect UI
                        timerSync.Stop();
                        timerPerTurn.Stop();

                        ShowOpponentPassedPopup();

                        // popup đóng -> cho chạy lại
                        if (!matchFinished)
                        {
                            timerSync.Start();
                            timerPerTurn.Start();
                        }
                    }

                    // Reset cờ popup khi "sự kiện pass" đã qua (không còn just-passed)
                    if (!opponentJustPassed)
                        opponentPassPopupShown = false;
                }

                // Nếu đối thủ gửi đề nghị hòa cho mình
                if (currentMatch.DrawRequested &&
                !matchFinished &&
                localPlayer != null &&
                !string.IsNullOrEmpty(currentMatch.DrawRequesterId))
                {
                bool iAmRequester = currentMatch.DrawRequesterId == localPlayer.Id;

                // Chỉ hiện hộp thoại cho người KHÔNG phải là người gửi và mỗi lần đề nghị chỉ hiện đúng 1 lần
                    if (!iAmRequester && !drawOfferDialogShown)
                    {
                    drawOfferDialogShown = true;

                    // Tạm dừng timer khi hỏi
                    timerSync.Stop();
                    timerPerTurn.Stop();

                    var res = MessageBox.Show(
                        "The opponent has offered a draw. Do you agree?",
                        "Draw Offer",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                        if (res == DialogResult.Yes)
                        {
                        // Đồng ý: kết thúc trận với kết quả hòa
                        await FinishMatchAsync(MatchResultType.Cancelled, null, null, null, navigateBack: false);
                        ShowEndForm_Draw();
                        return;
                        }
                        else
                        {
                        // Từ chối: xóa đề nghị hòa trên Firebase
                        currentMatch.DrawRequested = false;
                        currentMatch.DrawRequesterId = null;
                        await firebase.SaveMatchAsync(currentMatch);

                        // Cho phép hiển thị lại nếu sau này có đề nghị mới
                        drawOfferDialogShown = false;

                        timerSync.Start();
                        timerPerTurn.Start();
                        }
                    }
                }

                // Cập nhật board local
                game = latest;
                pl_Board.Invalidate();

                // Đồng bộ chat theo từng tick
                await LoadChatMessagesAsync();
            }
            finally
            {
                syncTickRunning = false;
            }
        }

        // =====================================================================
        //  TIMER ĐẾM NGƯỢC THEO LƯỢT MỖI NGƯỜI
        // =====================================================================

        private async void timerPerTurn_Tick(object sender, EventArgs e)
        {
            if (matchFinished || currentMatch == null)
                return;

            // Người đến lượt sẽ bị trừ thời gian
            if (game.CurrentPlayer == 1)
            {
                blackRemainingSeconds--;
                if (blackRemainingSeconds <= 0)
                {
                    blackRemainingSeconds = 0;
                    UpdateTimeLabels();
                    await EndGameByTimeoutWithScoringAsync(timedOutColor: 1); // Đen hết giờ
                    return;
                }
            }
            else
            {
                whiteRemainingSeconds--;
                if (whiteRemainingSeconds <= 0)
                {
                    whiteRemainingSeconds = 0;
                    UpdateTimeLabels();
                    await EndGameByTimeoutWithScoringAsync(timedOutColor: 2); // Trắng hết giờ
                    return;
                }
            }

            UpdateTimeLabels();
        }

        private void UpdateTimeLabels()
        {
            lbl_Time_Right.Text = TimeSpan.FromSeconds(blackRemainingSeconds)
                .ToString(@"hh\:mm\:ss");
            lbl_Time_Left.Text = TimeSpan.FromSeconds(whiteRemainingSeconds)
                .ToString(@"hh\:mm\:ss");
        }

        // =====================================================================
        //  DOUBLE PASS → TÍNH ĐIỂM VÀ KẾT THÚC TRẬN
        // =====================================================================

        private async void Game_DoublePassHappened()
        {
            // tránh gọi 2 lần trên cùng 1 client
            if (matchFinishedByDoublePass) return;
            matchFinishedByDoublePass = true;

            if (currentMatch == null || currentMatch.Status != MatchStatus.InProgress)
                return;

            // dừng đồng hồ khi đã double pass
            timerPerTurn.Stop();
            timerSync.Stop();

            // Tính điểm theo rule đã chọn
            double blackScore = 0;
            double whiteScore = 0;

            if (string.Equals(scoringRule, "Chinese", StringComparison.OrdinalIgnoreCase))

            {
                var scorer = new ChineseScoring();
                (blackScore, whiteScore) = scorer.Calculate(game);
            }
            else
            {
                var scorer = new JapaneseScoring();
                (blackScore, whiteScore) = scorer.Calculate(game);
            }

            // Xác định người thắng (có thể hoà nếu điểm bằng nhau)
            string winnerId = null;
            if (blackScore > whiteScore)
                winnerId = currentMatch.BlackPlayer?.Id;
            else if (whiteScore > blackScore)
                winnerId = currentMatch.WhitePlayer?.Id;
            else
                winnerId = null; // hoà

            // Hiển thị kết quả bằng Form (Win/Lose/Draw) trên máy hiện tại
            bool isDraw = winnerId == null;
            string blackText = $"{txt_Name_Right.Text}: {blackScore}";
            string whiteText = $"{txt_Name_Left.Text}: {whiteScore}";
                        
            await FinishMatchAsync(MatchResultType.DoublePass, winnerId, blackScore, whiteScore, navigateBack: false);

            if (winnerId == null)
            {
                ShowEndForm_Draw();
            }
            else
            {
                bool localWon = localPlayer != null && winnerId == localPlayer.Id;
                ShowEndForm_WinLose(localWon, blackText, whiteText);
            }
            return;
        }

        // =====================================================================
        // XỬ LÝ TIMEOUT
        // =====================================================================

        private async Task EndGameByTimeoutWithScoringAsync(int timedOutColor)
        {
            if (matchFinished || currentMatch == null) return;

            // Dừng timer
            timerPerTurn.Stop();
            timerSync.Stop();

            // Tính điểm theo rule đã chọn (y như double pass)
            double blackScore = 0;
            double whiteScore = 0;

            if (string.Equals(scoringRule, "Chinese", StringComparison.OrdinalIgnoreCase))

            {
                var scorer = new ChineseScoring();
                (blackScore, whiteScore) = scorer.Calculate(game);
            }
            else
            {
                var scorer = new JapaneseScoring();
                (blackScore, whiteScore) = scorer.Calculate(game);
            }

            // Timeout: bên hết giờ thua -> đối thủ thắng (nhưng vẫn lưu điểm)
            string winnerId =
                (timedOutColor == 1) ? currentMatch.WhitePlayer?.Id :
                (timedOutColor == 2) ? currentMatch.BlackPlayer?.Id :
                null;

            string blackText = $"{txt_Name_Right.Text}: {blackScore}";
            string whiteText = $"{txt_Name_Left.Text}: {whiteScore}";

            // Lưu match + xóa board trên Firebase (giữ ResultType = Resign như hiện tại)
            await FinishMatchAsync(
                MatchResultType.Resign,
                winnerId,
                blackScore,
                whiteScore,
                navigateBack: false
            );

            // Hiển thị form Win/Lose với điểm thật
            bool localWon = localPlayer != null && winnerId == localPlayer.Id;
            ShowEndForm_WinLose(localWon, blackText, whiteText);
        }

        // =====================================================================
        //  NÚT SKIP (BỎ LƯỢT)
        // =====================================================================

        private async void btn_Skip_Click(object sender, EventArgs e)
        {
            if (matchFinished || currentMatch == null)
            {
                MessageBox.Show("There is no match currently in progress.");
                return;
            }

            bool localIsBlack = (currentMatch.BlackPlayer != null &&
                                 localPlayer.Id == currentMatch.BlackPlayer.Id);
            bool localIsWhite = (currentMatch.WhitePlayer != null &&
                                 localPlayer.Id == currentMatch.WhitePlayer.Id);

            if (!localIsBlack && !localIsWhite)
            {
                MessageBox.Show("You are not a player in this match.");
                return;
            }

            // Xác nhận bỏ lượt
            var confirm = MessageBox.Show(
                "Are you sure you want to pass your turn?\nIf both players pass, the match will end and be scored.",
                "Confirm Pass",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.No) return;

            // Bỏ lượt
            game.Pass();
            pl_Board.Invalidate();

            // Lưu lên Firebase
            if (!string.IsNullOrEmpty(currentMatch.MatchId))
            {
                await firebase.SaveGameAsync(currentMatch.MatchId, game);
            }                        
        }

        private void ShowOpponentPassedPopup()
        {
            // Lấy tên đối thủ theo phía của local
            string opponentName = "Opponent";
            if (localColor == 1) // mình là Đen => đối thủ là Trắng (bên trái)
                opponentName = string.IsNullOrWhiteSpace(txt_Name_Left.Text) ? "Opponent" : txt_Name_Left.Text;
            else if (localColor == 2) // mình là Trắng => đối thủ là Đen (bên phải)
                opponentName = string.IsNullOrWhiteSpace(txt_Name_Right.Text) ? "Opponent" : txt_Name_Right.Text;

            MessageBox.Show(
                $"{opponentName} has passed their turn.\nIt's your turn now.",
                "Opponent Passed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        // =====================================================================
        //  NÚT DRAW (HÒA, KHÔNG TÍNH ĐIỂM)
        // =====================================================================

        private async void btn_Draw_Click(object sender, EventArgs e)
        {
            if (matchFinished || currentMatch == null)
            {
                MessageBox.Show("There is no match currently in progress.");
                return;
            }

            // Lấy lại match mới nhất từ Firebase (phòng khi bên kia vừa gửi/huỷ đề nghị)
            var updatedMatch = await firebase.LoadMatchAsync(currentMatch.MatchId);
            if (updatedMatch != null)
                currentMatch = updatedMatch;

            // Nếu đã có đề nghị hòa đang tồn tại
            if (currentMatch.DrawRequested)
            {
                if (localPlayer != null && currentMatch.DrawRequesterId == localPlayer.Id)
                {
                    MessageBox.Show("You have sent a draw offer and are waiting for the opponent's response.", "Draw Offer");
                }
                else
                {
                    MessageBox.Show("The opponent has sent a draw offer. Please respond in the dialog that appears.", "Draw Offer");
                }
                return;
            }

            // Xác nhận phía mình
            var confirm = MessageBox.Show(
                "You want to offer a draw?\nIf the opponent agrees, the match will end in a draw.",
                "Offer Draw",
                MessageBoxButtons.YesNo,
               MessageBoxIcon.Question);

            if (confirm == DialogResult.No) return;

            // Gửi đề nghị hòa lên Firebase
            currentMatch.DrawRequested = true;
            currentMatch.DrawRequesterId = localPlayer?.Id;
            await firebase.SaveMatchAsync(currentMatch);

            localDrawOfferPending = true;   // đánh dấu là mình đang chờ đối thủ trả lời

            MessageBox.Show("You have sent a draw offer. Please wait for the opponent to accept.", "Draw Offer");
        }

        // =====================================================================
        //  NÚT RESIGN (CHỈ SAU 1 PHÚT, AI BẤM THÌ THUA, ĐỐI THỦ THẮNG – KHÔNG TÍNH ĐIỂM)
        // =====================================================================

        private async void btn_Resign_Click(object sender, EventArgs e)
        {
            if (matchFinished || currentMatch == null)
            {
                MessageBox.Show("There is no match currently in progress.");
                return;
            }


            var elapsed = DateTime.Now - matchStartTime;
            if (elapsed < TimeSpan.FromMinutes(1))
            {
                MessageBox.Show("You can only resign after 1 minutes from the start of the match.", "Notification");
                return;
            }

            var confirm = MessageBox.Show(
                "Are you sure you want to resign the match?\nIf you resign, the opponent will be awarded a win.",
                "Confirm Resign",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.No) return;

            bool localIsBlack = (currentMatch.BlackPlayer != null &&
                                 localPlayer.Id == currentMatch.BlackPlayer.Id);

            string winnerId = localIsBlack
                ? currentMatch.WhitePlayer?.Id
                : currentMatch.BlackPlayer?.Id;

            string winnerName = localIsBlack
                ? currentMatch.WhitePlayer?.Name
                : currentMatch.BlackPlayer?.Name;

            // Không tính điểm → để điểm null
            await FinishMatchAsync(
                MatchResultType.Resign,  // dùng Resign như xin thua
                winnerId,
                null,
                null,
                navigateBack: false);

            // Hiển thị Win/Lose bằng form, điểm N/A cho Quit
            bool localWon = localPlayer != null && winnerId == localPlayer.Id;
            ShowEndForm_WinLose(localWon, "N/A", "N/A");
            return;
        }

        // ==========================
        //  CHAT 
        // ==========================

        //hiển thị tin nhắn
        private void RenderChatMessages()
        {
            if (rtb_Chat == null) return;

            rtb_Chat.Clear();

            foreach (var entry in chatMessages
                .OrderBy(e => e.Msg.Timestamp)
                .ThenBy(e => e.Id))
            {
                var msg = entry.Msg;

                string name = (localPlayer != null && msg.SenderId == localPlayer.Id)
                    ? "You"
                    : (string.IsNullOrWhiteSpace(msg.SenderName) ? "Opponent" : msg.SenderName);

                rtb_Chat.AppendText($"{name}: {msg.Text}\n");
            }

            rtb_Chat.SelectionStart = rtb_Chat.TextLength;
            rtb_Chat.ScrollToCaret();
        }

        private async Task LoadChatMessagesAsync()
        {
            if (realtimeDb == null || string.IsNullOrEmpty(currentMatchId))
                return;

            try
            {
                var data = await realtimeDb.GetAsync<Dictionary<string, ChatMessage>>(
                    $"chats/{currentMatchId}");

                if (data == null)
                {
                    chatMessages.Clear();
                }
                else
                {
                    chatMessages = data
                        .Select(kv => new ChatEntry { Id = kv.Key, Msg = kv.Value })
                        .OrderBy(e => e.Msg.Timestamp)
                        .ThenBy(e => e.Id)
                        .ToList();
                }

                RenderChatMessages();
            }
            catch
            {
                // bỏ qua
            }
        }

        //Gủi tin nhắn
        private async void btn_SendChat_Click(object sender, EventArgs e)
        {
            if (realtimeDb == null || string.IsNullOrEmpty(currentMatchId) || localPlayer == null)
                return;

            string text = txt_ChatInput.Text.Trim();
            if (string.IsNullOrEmpty(text))
                return;

            string msgId = Guid.NewGuid().ToString("N");

            // Dùng server timestamp để mọi client có cùng mốc thời gian
            var payload = new Dictionary<string, object>
            {
                ["SenderId"] = localPlayer.Id,
                ["SenderName"] = string.IsNullOrWhiteSpace(localPlayer.Name) ? "Guest" : localPlayer.Name,
                ["Text"] = text,
                ["Timestamp"] = new Dictionary<string, object>
                {
                    [".sv"] = "timestamp"
                }
            };

            try
            {
                await realtimeDb.SetAsync($"chats/{currentMatchId}/{msgId}", payload);

                txt_ChatInput.Clear();

                // KHÔNG add local nữa để tránh lệch thứ tự / trùng dữ liệu
                await LoadChatMessagesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to send the message: " + ex.Message);
            }
        }

        // Mở / đóng khung emoji
        private void btn_EmojiPicker_Click(object sender, EventArgs e)
        {
            if (pnl_EmojiPicker == null) return;

            pnl_EmojiPicker.Visible = !pnl_EmojiPicker.Visible;
        }

        //Xử lý click từng emoji
        private void EmojiButton_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && txt_ChatInput != null)
            {
                txt_ChatInput.Text += btn.Text;
                txt_ChatInput.SelectionStart = txt_ChatInput.Text.Length;
                txt_ChatInput.Focus();
            }
        }

        // Khi click vào ô nhập tin nhắn
        private void txt_ChatInput_Click(object sender, EventArgs e)
        {
            ShowChatInputControls();
        }

        //Hiện nút gửi + nút emoji
        private void ShowChatInputControls()
        {
            if (btn_SendChat != null)
                btn_SendChat.Visible = true;

            if (btn_EmojiPicker != null)
                btn_EmojiPicker.Visible = true;
        }

        // =====================================================================
        //  HÀM KẾT THÚC TRẬN, LƯU LÊN FIREBASE, XÓA DỮ LIỆU BOARD, THOÁT VỀ GAMEMODE
        // =====================================================================

        private async Task FinishMatchAsync(
            MatchResultType resultType,
            string winnerPlayerId,
            double? blackScore,
            double? whiteScore,
            bool navigateBack = true)
        {
            if (currentMatch == null)
                return;

            matchFinished = true;

            currentMatch.ResultType = resultType;
            currentMatch.WinnerPlayerId = winnerPlayerId;
            currentMatch.BlackScore = blackScore;
            currentMatch.WhiteScore = whiteScore;
            currentMatch.Status = MatchStatus.Finished;

            // Lưu thời gian bắt đầu/kết thúc
            if (currentMatch.StartedAt == default)
                currentMatch.StartedAt = matchStartTime.ToUniversalTime();
            currentMatch.EndedAt = DateTime.UtcNow;

            // Lưu meta trận đấu
            await firebase.SaveMatchAsync(currentMatch);

            // Xóa board game trên Firebase
            if (!string.IsNullOrEmpty(currentMatch.MatchId))
            {
                await firebase.DeleteGameAsync(currentMatch.MatchId);
            }

            // Tắt UI
            timerSync.Stop();
            timerPerTurn.Stop();
            pl_Board.Enabled = false;
            btn_Skip.Enabled = false;
            btn_Draw.Enabled = false;
            btn_Resign.Enabled = false;            
            // ======================
            // CẬP NHẬT ELO (CHỈ KHI CHƠI RANDOM)
            // ======================
            bool amIWinner = (winnerPlayerId == localPlayer.Id);
            await UpdateMyStatsAsync(amIWinner, currentMatch.IsRandom);
            if (realtimeDb != null && currentMatch.IsRandom)
            {
                string blackId = currentMatch.BlackPlayer?.Id;
                string whiteId = currentMatch.WhitePlayer?.Id;

                // HÒA
                if (winnerPlayerId == null)
                {
                    if (!string.IsNullOrEmpty(blackId))
                        await realtimeDb.UpdateEloAsync(blackId, +5);

                    if (!string.IsNullOrEmpty(whiteId))
                        await realtimeDb.UpdateEloAsync(whiteId, +5);
                }
                else
                {
                    // THẮNG / THUA
                    if (winnerPlayerId == blackId)
                    {
                        await realtimeDb.UpdateEloAsync(blackId, +10);
                        await realtimeDb.UpdateEloAsync(whiteId, -5);
                    }
                    else if (winnerPlayerId == whiteId)
                    {
                        await realtimeDb.UpdateEloAsync(whiteId, +10);
                        await realtimeDb.UpdateEloAsync(blackId, -5);
                    }
                }
            }
            // Quay lại Gamemode
            if (navigateBack)
                CloseBackToGamemode();
        }

        // =====================================================================
        //  XỬ LÝ KHI TRẬN KẾT THÚC TỪ MÁY BÊN KIA (REMOTE)
        // =====================================================================

        private async void HandleRemoteMatchFinished(MatchInfo updatedMatch)
        {
            if (updatedMatch == null) { CloseBackToGamemode(); return; }

            matchFinished = true;
            currentMatch = updatedMatch;

            timerSync.Stop();
            timerPerTurn.Stop();

            pl_Board.Enabled = false;
            btn_Skip.Enabled = false;
            btn_Draw.Enabled = false;
            btn_Resign.Enabled = false;
            bool amIWinner = (updatedMatch.WinnerPlayerId == localPlayer.Id);
            await UpdateMyStatsAsync(amIWinner, updatedMatch.IsRandom);
            // Ưu tiên hiển thị form kết quả thay vì MessageBox
            if (updatedMatch.ResultType == MatchResultType.Cancelled &&
                string.IsNullOrEmpty(updatedMatch.WinnerPlayerId))
            {
                // Hòa (btn_Draw được chấp nhận)
                ShowEndForm_Draw();
                return;
            }

            // Có điểm => DoublePass
            if (updatedMatch.ResultType == MatchResultType.DoublePass &&
                updatedMatch.BlackScore.HasValue && updatedMatch.WhiteScore.HasValue)
            {
                string blackText = $"{txt_Name_Right.Text}: {updatedMatch.BlackScore.Value}";
                string whiteText = $"{txt_Name_Left.Text}: {updatedMatch.WhiteScore.Value}";

                // Nếu điểm bằng nhau thì coi như hòa
                if (Math.Abs(updatedMatch.BlackScore.Value - updatedMatch.WhiteScore.Value) < 0.000001)
                {
                    ShowEndForm_Draw();
                    return;
                }

                bool localWon = localPlayer != null && updatedMatch.WinnerPlayerId == localPlayer.Id;
                ShowEndForm_WinLose(localWon, blackText, whiteText);
                return;
            }

            // Các trường hợp còn lại (Resign/Quit/Timeout/Leave...) => không tính điểm
            if (!string.IsNullOrEmpty(updatedMatch.WinnerPlayerId))
            {
                bool localWon = localPlayer != null && updatedMatch.WinnerPlayerId == localPlayer.Id;
                ShowEndForm_WinLose(localWon, "N/A", "N/A");
                return;
            }
            // fallback
            ShowEndForm_Draw();
        }

        // =====================================================================
        //  ĐÓNG FORM BẰNG NÚT X 
        // =====================================================================

        private async void Mode_Online_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Nếu đã đóng bằng code hoặc trận đã kết thúc thì không xử lý thêm
            if (closingFromCode || matchFinished)
                return;

            // Chỉ xử lý khi người dùng tự đóng (click X, Alt+F4, ...)
            if (currentMatch != null && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;

                // Chỉ người bấm X thấy confirm
                var confirm = MessageBox.Show(
                    "Are you sure you want to leave the match?\n" +
                    "If you leave, the match will be canceled for both players and will not be saved.",
                    "Confirm Quit",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.No)
                    return;

                // tự xử lý hủy trận + thoát về GameMode
                await CancelMatchForBothAsync();

                closingFromCode = true;
                CloseBackToGamemode(force: true);
            }
        }

        private async Task CancelMatchForBothAsync()
        {
            if (currentMatch == null || string.IsNullOrWhiteSpace(currentMatch.MatchId))
                return;

            // Dừng mọi thứ ngay lập tức
            matchFinished = true;
            matchEndHandled = true;

            timerSync?.Stop();
            timerPerTurn?.Stop();

            try
            {
                // Xóa board => phía bên kia sẽ thấy latest == null trong timerSync_Tick
                await firebase.DeleteGameAsync(currentMatch.MatchId);

                // Xóa match => không lưu trận
                await firebase.DeleteMatchAsync(currentMatch.MatchId);

                //  Xóa chat của phòng
                if (realtimeDb != null)
                {
                    await realtimeDb.DeleteAsync($"chats/{currentMatch.MatchId}");
                }
            }
            catch
            {
                // Nếu xóa lỗi do mạng/quyền, vẫn cho thoát về GameMode để tránh kẹt UI
            }
        }

        // =====================================================================
        //  HIỂN THỊ FORM KẾT QUẢ (WIN / LOSE / DRAW) + TỰ QUAY VỀ GAMEMODE
        // =====================================================================

        private bool endFormShown = false;

        private void ShowEndForm_Draw()
        {
            if (endFormShown) return;
            endFormShown = true;

            var f = new Form_Draw();
            PrepareEndForm(f);
        }

        private void ShowEndForm_WinLose(bool localWon, string blackText, string whiteText)
        {
            if (endFormShown) return;
            endFormShown = true;

            Form f;
            if (localWon)
            {
                var win = new Form_Win();
                win.SetScores(blackText, whiteText);
                f = win;
            }
            else
            {
                var lose = new Form_Lose();
                lose.SetScores(blackText, whiteText);
                f = lose;
            }

            PrepareEndForm(f);
        }

        private void PrepareEndForm(Form f)
        {
            if (f == null) return;

            f.StartPosition = FormStartPosition.CenterScreen;
            f.TopMost = true;

            // Khi form kết quả đóng -> quay về GameMode và đóng Mode_Online
            f.FormClosed += (s, e) =>
            {
                if (this.IsDisposed) return;

                this.BeginInvoke(new Action(() =>
                {
                    if (!this.IsDisposed)
                        CloseBackToGamemode();
                }
                ));
            };

            f.Show(this);
        }

        // =====================================================================
        //  HÀM QUAY VỀ FORM GAMEMODE
        // =====================================================================

        private void CloseBackToGamemode(bool force = false)
        {
            if (closingFromCode && !force) return;

            closingFromCode = true;

            timerSync?.Stop();
            timerPerTurn?.Stop();

            try
            {
                var mainForm = Application.OpenForms["Man_Hinh_Chinh"] as Man_Hinh_Chinh;
                if (mainForm != null)
                {
                    GameMode gm = new GameMode(mainForm);
                    gm.Show();
                }
            }
            catch { }

            this.Close();
        }

        // =====================================================================
        // STATS
        // =====================================================================
        private async Task UpdateMyStatsAsync(bool isWinner, bool isRandom)
        {
            if (!isRandom) return;
            if (localPlayer == null || string.IsNullOrEmpty(localPlayer.Id)) return;

            try
            {
                // 1. Lấy stats hiện tại
                var stats = await realtimeDb.GetAsync<UserStats>($"users/{localPlayer.Id}/stats");
                if (stats == null) stats = new UserStats();

                // 2. Cộng chỉ số chơi PvP (Online mode luôn tính là PvP)
                stats.play_pvp += 1;

                // 3. Nếu thắng thì cộng chỉ số thắng
                if (isWinner)
                {
                    stats.win_pvp += 1;
                }

                // 4. Ghi ngược lại lên Firebase               
                await realtimeDb.SetAsync($"users/{localPlayer.Id}/stats", stats);
            }
            catch (Exception ex)
            {
                // Ghi log hoặc bỏ qua lỗi mạng để không làm crash game
                Console.WriteLine("Error updating stats: " + ex.Message);
            }
        }
    }
}
