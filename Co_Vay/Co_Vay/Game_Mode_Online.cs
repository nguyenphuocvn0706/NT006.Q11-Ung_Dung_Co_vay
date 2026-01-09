using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Co_Vay
{
    public partial class Game_Mode_Online : Form
    {
        private readonly FirebaseService firebase;
        private readonly RealtimeDatabaseService realtimeDb;

        // Người chơi hiện tại (nên truyền từ form đăng nhập)
        private PlayerInfo localPlayer;

        // Dùng cho chế độ Random
        private System.Windows.Forms.Timer timerRandomMatch;
        private bool isWaitingRandom = false;
        private string waitingRandomMatchId;

        // Form chờ ghép trận
        private Form_Match_Making matchMakingForm;

        // Thông tin match đang chờ (Random)
        private class RandomQueueEntry
        {
            public string MatchId { get; set; }
            public PlayerInfo FirstPlayer { get; set; }
            public DateTime CreatedAt { get; set; }
        }

        public Game_Mode_Online() : this(null)
        {
        }

        public Game_Mode_Online(PlayerInfo player)
        {
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            firebase = new FirebaseService();
            realtimeDb = new RealtimeDatabaseService();

            // Truyền PlayerInfo từ form đăng nhập
            if (player == null)
            {
                MessageBox.Show(
                    "Game_Mode_Online phải được mở với thông tin người chơi (PlayerInfo). " +
                    "Hãy truyền PlayerInfo từ form đăng nhập.",
                    "Lỗi cấu hình");

                Close();
                return;
            }

            localPlayer = player;

            // Timer dùng cho Random – polling xem đối thủ đã vào chưa
            timerRandomMatch = new System.Windows.Forms.Timer();
            timerRandomMatch.Interval = 1000;
            timerRandomMatch.Tick += timerRandomMatch_Tick;
        }

        // =========================================================
        //  NÚT RANDOM: GHÉP CẶP NGẪU NHIÊN + FORM CHỜ
        // =========================================================
        private async void btn_Random_Click(object sender, EventArgs e)
        {
            // Khoá nút trong lúc xử lý
            btn_Random.Enabled = false;
            btn_Private.Enabled = false;

            try
            {
                // Đọc slot chờ hiện tại (nếu có)
                var waiting = await realtimeDb.GetAsync<RandomQueueEntry>("matchmaking/random_waiting");

                bool joinAsSecond = false;
                RandomQueueEntry entryToJoin = null;

                if (waiting != null &&
                    !string.IsNullOrEmpty(waiting.MatchId) &&
                    waiting.FirstPlayer != null)
                {
                    // Kiểm tra match tương ứng slot chờ
                    var matchCheck = await firebase.LoadMatchAsync(waiting.MatchId);
                    if (matchCheck != null && matchCheck.Status == MatchStatus.Waiting)
                    {
                        // Có 1 người đang chờ, và trận vẫn Waiting -> mình là người thứ 2
                        joinAsSecond = true;
                        entryToJoin = waiting;
                    }
                }

                // ===============================
                // 1) KHÔNG CÓ AI CHỜ -> MÌNH LÀ NGƯỜI CHỜ
                // ===============================
                if (!joinAsSecond)
                {
                    string matchId = await firebase.GenerateMatchIdAsync();

                    waitingRandomMatchId = matchId;
                    isWaitingRandom = true;

                    var entry = new RandomQueueEntry
                    {
                        MatchId = matchId,
                        FirstPlayer = localPlayer,
                        CreatedAt = DateTime.UtcNow
                    };

                    // Ghi slot chờ
                    await realtimeDb.SetAsync("matchmaking/random_waiting", entry);

                    // Tạo match ở trạng thái Waiting
                    var match = new MatchInfo
                    {
                        IsRandom = true,
                        MatchId = matchId,
                        RoomType = RoomType.Random,
                        Status = MatchStatus.Waiting,
                        BlackPlayer = null,
                        WhitePlayer = null,
                        StartedAt = DateTime.UtcNow,
                        ResultType = MatchResultType.None
                    };

                    await firebase.SaveMatchAsync(match);

                    // HIỆN FORM CHỜ GHÉP TRẬN (KHÔNG BLOCK)
                    if (matchMakingForm == null || matchMakingForm.IsDisposed)
                    {
                        matchMakingForm = new Form_Match_Making();

                        // BẮT EVENT CANCEL
                        matchMakingForm.CancelMatchMaking += OnCancelMatchMaking;

                        matchMakingForm.StartPosition = FormStartPosition.CenterParent;
                        matchMakingForm.Show(this);
                    }
                    // Bắt đầu polling xem đối thủ đã vào chưa
                    timerRandomMatch.Start();
                }
                // ===============================
                // 2) ĐÃ CÓ 1 NGƯỜI CHỜ -> MÌNH LÀ NGƯỜI THỨ 2
                // ===============================
                else
                {
                    string matchId = entryToJoin.MatchId;
                    var firstPlayer = entryToJoin.FirstPlayer;

                    var match = await firebase.LoadMatchAsync(matchId);
                    if (match == null || match.Status != MatchStatus.Waiting)
                    {
                        // Slot chờ bị lỗi/không hợp lệ nữa
                        await realtimeDb.DeleteAsync("matchmaking/random_waiting");
                        MessageBox.Show(
                            "Slot chờ hiện tại không còn hợp lệ.\nVui lòng bấm Random lại.",
                            "Matchmaking Random");
                        return;
                    }

                    // Random màu quân
                    var rnd = new Random();
                    bool firstIsBlack = rnd.Next(2) == 0;

                    if (firstIsBlack)
                    {
                        match.BlackPlayer = firstPlayer;
                        match.WhitePlayer = localPlayer;
                    }
                    else
                    {
                        match.BlackPlayer = localPlayer;
                        match.WhitePlayer = firstPlayer;
                    }

                    match.RoomType = RoomType.Random;
                    match.IsRandom = true;
                    match.Status = MatchStatus.InProgress;
                    match.StartedAt = DateTime.UtcNow;
                    match.ResultType = MatchResultType.None;

                    await firebase.SaveMatchAsync(match);

                    // Xoá slot chờ sau khi đã ghép
                    await realtimeDb.DeleteAsync("matchmaking/random_waiting");

                    // Chắc chắn đóng form chờ nếu lỡ đang mở đâu đó
                    if (matchMakingForm != null && !matchMakingForm.IsDisposed)
                    {
                        matchMakingForm.Close();
                        matchMakingForm = null;
                    }

                    // Vào trận luôn cho người thứ 2
                    OpenModeOnline(match.MatchId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi ghép cặp Random:\n" + ex.Message, "Lỗi");
            }
            finally
            {
                // Nếu mình đang CHỜ (isWaitingRandom = true) thì không bật nút lại,
                // vì đang đợi timerRandomMatch xử lý.
                if (!isWaitingRandom)
                {
                    btn_Random.Enabled = true;
                    btn_Private.Enabled = true;
                }
            }
        }

        // Timer của NGƯỜI CHỜ ĐẦU TIÊN trong Random
        private async void timerRandomMatch_Tick(object sender, EventArgs e)
        {
            if (!isWaitingRandom || string.IsNullOrEmpty(waitingRandomMatchId))
                return;

            try
            {
                var match = await firebase.LoadMatchAsync(waitingRandomMatchId);
                if (match != null &&
                    match.Status == MatchStatus.InProgress &&
                    match.BlackPlayer != null &&
                    match.WhitePlayer != null)
                {
                    // Đối thủ đã join, trận đã InProgress -> vào trận
                    timerRandomMatch.Stop();
                    isWaitingRandom = false;

                    // TỰ ĐỘNG ĐÓNG FORM CHỜ
                    if (matchMakingForm != null && !matchMakingForm.IsDisposed)
                    {
                        matchMakingForm.Close();
                        matchMakingForm = null;
                    }

                    // Vào trận luôn, KHÔNG CẦN người chơi bấm gì thêm
                    OpenModeOnline(match.MatchId);
                }
            }
            catch
            {
                // Có lỗi thì lần tick sau kiểm tra lại cũng được
            }
        }


        // =========================================================
        //  NÚT PRIVATE: SANG FORM CẤU HÌNH PHÒNG PRIVATE
        // =========================================================
        private void btn_Private_Click(object sender, EventArgs e)
        {
            var f = new Online_Privatematch(localPlayer);
            f.Show();
            this.Hide();
        }

        // =========================================================
        //  NÚT BACK: QUAY LẠI MENU CHÍNH 
        // =========================================================
        private void btn_Back_Click(object sender, EventArgs e)
        {
            var mainForm = Application.OpenForms["Man_Hinh_Chinh"] as Form;
            if (mainForm != null)
                mainForm.Show();

            this.Close();
        }

        // =========================================================
        //  MỞ MODE_ONLINE VỚI MATCHID & LOCALPLAYER
        // =========================================================
        private void OpenModeOnline(string matchId)
        {
            var f = new Mode_Online(matchId, localPlayer);
            f.Show();
            this.Hide();
        }

        // hàm hủy chờ trận
        private async void OnCancelMatchMaking()
        {
            // Nếu không ở trạng thái chờ thì bỏ qua
            if (!isWaitingRandom)
                return;

            isWaitingRandom = false;
            timerRandomMatch.Stop();

            try
            {
                // Xoá slot chờ nếu mình là người tạo
                await realtimeDb.DeleteAsync("matchmaking/random_waiting");

                // Nếu match đã tạo nhưng chưa ai join → có thể xoá hoặc giữ tuỳ bạn
                if (!string.IsNullOrEmpty(waitingRandomMatchId))
                {
                    var match = await firebase.LoadMatchAsync(waitingRandomMatchId);
                    if (match != null && match.Status == MatchStatus.Waiting)
                    {
                        await firebase.DeleteGameAsync(waitingRandomMatchId);
                    }
                }
            }
            catch
            {
                // Không cần show lỗi, cancel là hành động người dùng
            }
            finally
            {
                waitingRandomMatchId = null;

                // Mở lại nút
                btn_Random.Enabled = true;
                btn_Private.Enabled = true;
            }
        }
    }
}
