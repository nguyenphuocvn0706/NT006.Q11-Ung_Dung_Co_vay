using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Co_Vay
{
    public partial class Mode_W_Comp : Form
    {
        private string idToken;
        // Kích thước bàn cờ
        private readonly int boardSize = 19;
        private readonly int cellSize = 30;

        // Màu quân: 1 = Đen, 2 = Trắng
        private readonly int machineColor = 1;   // máy luôn ĐEN (bên phải)
        private readonly int localColor = 2;     // người chơi TRẮNG (bên trái)

        // Rule + Time
        private string scoringRule = "Japanese";
        private int timePerSideMinutes = 30;

        private Game_Engine game;
        private PlayerInfo localPlayer;
        private RealtimeDatabaseService realtimeDb;
        private Game_AIcore ai;

        // Đồng hồ
        private System.Windows.Forms.Timer timerPerTurn;
        private int blackRemainingSeconds;
        private int whiteRemainingSeconds;

        private bool matchFinished = false;
        private bool matchFinishedByDoublePass = false;
        private bool closingFromCode = false;

        // Tù nhân (luật Nhật)
        private int blackPrisoners = 0; // trắng bị đen bắt
        private int whitePrisoners = 0; // đen bị trắng bắt

        // Chống click khi AI đang nghĩ + chống apply nhầm nước
        private bool isComputerThinking = false;
        private int stateVersion = 0;

        // Bạn muốn “đứng im chờ AI nghĩ” bao lâu (ms)
        // Tăng/giảm số này theo ý bạn (vd: 500 ~ 1200)
        private const int THINK_DELAY_MS = 0;

        private readonly Random rng = new Random();
        private class UserStats
        {
            public int play_ai { get; set; }
            public int win_ai { get; set; }
            public int play_pvp { get; set; }
            public int win_pvp { get; set; }
        }
        private class MoveRecord
        {
            public bool IsPass { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
        }

        private readonly List<MoveRecord> moveHistory = new List<MoveRecord>();

        // =========================================================
        // CONSTRUCTORS 
        // =========================================================
        
        public Mode_W_Comp()
        {
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            localPlayer = new PlayerInfo { Id = null, Name = "Player" };
            scoringRule = "Japanese";
            timePerSideMinutes = 30;

            InitCommon();
        }

        // Dùng khi mở từ Match_Setting
        public Mode_W_Comp(PlayerInfo player, string rule, int timeMinutes, string token)
        {
            InitializeComponent();
            this.idToken = idToken;
            localPlayer = player ?? new PlayerInfo { Id = null, Name = "Player" };
            scoringRule = string.IsNullOrWhiteSpace(rule) ? "Japanese" : rule;
            timePerSideMinutes = timeMinutes > 0 ? timeMinutes : 30;

            InitCommon();
        }

        private void InitCommon()
        {
            // Double-buffer cho panel
            typeof(Panel).InvokeMember(
                "DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                pl_Board,
                new object[] { true });

            realtimeDb = new RealtimeDatabaseService(this.idToken);

            // Event control
            pl_Board.Paint += pl_Board_Paint;
            pl_Board.MouseClick += pl_Board_MouseClick;

            // Timer
            timerPerTurn = new System.Windows.Forms.Timer();
            timerPerTurn.Interval = 1000;
            timerPerTurn.Tick += timerPerTurn_Tick;

            this.Load += Mode_W_Comp_Load;
            this.FormClosing += Mode_W_Comp_FormClosing;

            // Tên hiển thị
            txt_Name_Right.Text = "Computer (Black)";
            txt_Name_Left.Text = string.IsNullOrWhiteSpace(localPlayer?.Name)
                ? "Player"
                : localPlayer.Name;            
        }

        // =========================================================
        // LOAD FORM 
        // =========================================================
        private async void Mode_W_Comp_Load(object sender, EventArgs e)
        {
            // Lấy username từ Firebase nếu có Id
            if (localPlayer != null && !string.IsNullOrWhiteSpace(localPlayer.Id))
            {
                try
                {
                    var user = await realtimeDb.GetUserAsync(localPlayer.Id);
                    if (user != null && !string.IsNullOrWhiteSpace(user.name))
                        txt_Name_Left.Text = user.name;
                }
                catch
                {                    
                }
            }

            // Khởi tạo game + AI
            game = new Game_Engine(boardSize);
            game.DoublePassHappened += Game_DoublePassHappened;

            ai = new Game_AIcore(game, machineColor, localColor, boardSize, rng);

            // Thời gian mỗi bên
            blackRemainingSeconds = timePerSideMinutes * 60;
            whiteRemainingSeconds = timePerSideMinutes * 60;
            UpdateTimeLabels();

            pl_Board.Visible = true;
            pl_Board.Invalidate();
            timerPerTurn.Start();

            // Máy (đen) đi trước
            if (game.CurrentPlayer == machineColor)
                _ = PlayComputerMoveAsync(++stateVersion);
            _ = UpdateUserStats_AI(false);
        }

        // =========================================================
        // DRAW BOARD
        // =========================================================

        private Point boardOrigin = new Point(30, 30); // điểm khởi đầu

        private void pl_Board_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var old = g.Transform;
            g.TranslateTransform(boardOrigin.X, boardOrigin.Y);

            DrawGrid(g);
            DrawHoshi(g);

            if (game != null)
                DrawStones(g);

            g.Transform = old;
        }

        private void DrawGrid(Graphics g)
        {
            using (var pen = new Pen(Color.Black, 1.5f))
            {
                for (int i = 0; i < boardSize; i++)
                {
                    // ngang
                    g.DrawLine(pen,
                        cellSize,
                        cellSize + i * cellSize,
                        cellSize * boardSize,
                        cellSize + i * cellSize);

                    // dọc
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
                    if (color == 1)
                    {
                        pgb.CenterColor = Color.Gray;
                        pgb.SurroundColors = new Color[] { Color.Black };
                    }
                    else
                    {
                        pgb.CenterColor = Color.White;
                        pgb.SurroundColors = new Color[] { Color.LightGray };
                    }

                    g.FillEllipse(pgb, r);
                    g.DrawEllipse(Pens.Black, r);
                }
            }
        }
        
        // =========================================================
        // CLICK ĐÁNH CỜ 
        // =========================================================
        
        private void pl_Board_MouseClick(object sender, MouseEventArgs e)
        {
            if (matchFinished || game == null) return;
            if (isComputerThinking) return;

            // chỉ cho đánh khi tới lượt TRẮNG
            if (game.CurrentPlayer != localColor) return;

            // quy đổi tọa độ click về hệ tọa độ của bàn cờ (đã dịch)
            int localX = e.X - boardOrigin.X;
            int localY = e.Y - boardOrigin.Y;

            int x = (localX - cellSize / 2) / cellSize;
            int y = (localY - cellSize / 2) / cellSize;

            if (!game.InBounds(x, y)) return;

            int colorBefore = game.CurrentPlayer;

            if (!game.TryPlayMove(x, y, out int captured, out string error))
            {
                if (!string.IsNullOrEmpty(error))
                    MessageBox.Show(error, "Invalid move");
                return;
            }

            // cập nhật tù nhân
            if (colorBefore == machineColor) blackPrisoners += captured;
            else if (colorBefore == localColor) whitePrisoners += captured;

            moveHistory.Add(new MoveRecord { IsPass = false, X = x, Y = y });

            //  ÉP UI VẼ NGAY QUÂN TRẮNG
            pl_Board.Invalidate();
            pl_Board.Update(); // repaint ngay lập tức (không chờ message loop)

            // Tăng version sau khi trạng thái đổi
            int ver = ++stateVersion;

            // Nếu sau nước của trắng, tới lượt đen thì mới gọi AI
            if (!matchFinished && game.CurrentPlayer == machineColor)
                _ = PlayComputerMoveAsync(ver);
        }

        // =========================================================
        // MÁY ĐÁNH
        // =========================================================

        private async Task PlayComputerMoveAsync(int versionAtStart)
        {
            if (matchFinished || game == null) return;
            if (game.CurrentPlayer != machineColor) return;
            if (isComputerThinking) return;

            try
            {
                isComputerThinking = true;
                pl_Board.Enabled = false;
                Cursor = Cursors.WaitCursor;

                //delay máy 
                await Task.Delay(THINK_DELAY_MS);

                // Nếu trong lúc chờ mà game đổi trạng thái thì bỏ
                if (matchFinished || game == null) return;
                if (versionAtStart != stateVersion) return;
                if (game.CurrentPlayer != machineColor) return;

                //  Tính ở background để không block UI
                var snapshot = new Game_Engine(boardSize)
                {
                    SerializableBoard = game.SerializableBoard
                };
                snapshot.CopyFrom(game);

                var aiSnapshot = new Game_AIcore(snapshot, machineColor, localColor, boardSize, rng);

                var best = await Task.Run(() => aiSnapshot.GetBestMove(maxDepth: 3));

                //  Apply về UI (nếu vẫn đúng version)
                if (matchFinished || game == null) return;
                if (versionAtStart != stateVersion) return;
                if (game.CurrentPlayer != machineColor) return;

                int colorBefore = game.CurrentPlayer;

                if (best.IsPass)
                {
                    game.Pass();
                    moveHistory.Add(new MoveRecord { IsPass = true });
                }
                else
                {
                    if (game.TryPlayMove(best.X, best.Y, out int captured, out string error))
                    {
                        if (colorBefore == machineColor) blackPrisoners += captured;
                        else if (colorBefore == localColor) whitePrisoners += captured;

                        moveHistory.Add(new MoveRecord { IsPass = false, X = best.X, Y = best.Y });
                    }
                    else
                    {
                        // fallback: nếu best lỗi, thử chọn nước hợp lệ đầu tiên
                        TryComputerFallbackMove();
                    }
                }

                // tăng version sau khi máy apply move
                stateVersion++;

                // vẽ quân đen
                pl_Board.Invalidate();
            }
            finally
            {
                isComputerThinking = false;
                pl_Board.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        // Fallback nhẹ: chọn nước hợp lệ đầu tiên từ danh sách candidates
        private void TryComputerFallbackMove()
        {
            if (matchFinished || game == null || ai == null) return;
            if (game.CurrentPlayer != machineColor) return;

            var candidates = ai.GetSortedMoves();
            foreach (var c in candidates)
            {
                int colorBefore = game.CurrentPlayer;

                if (game.TryPlayMove(c.X, c.Y, out int captured, out string error))
                {
                    if (colorBefore == machineColor) blackPrisoners += captured;
                    else if (colorBefore == localColor) whitePrisoners += captured;

                    moveHistory.Add(new MoveRecord { IsPass = false, X = c.X, Y = c.Y });
                    return;
                }
            }

            // Không đánh được nước nào hợp lệ → pass
            game.Pass();
            moveHistory.Add(new MoveRecord { IsPass = true });
        }

        // =========================================================
        // TIMER
        // =========================================================

        private void timerPerTurn_Tick(object sender, EventArgs e)
        {
            if (matchFinished || game == null) return;

            if (game.CurrentPlayer == machineColor)
            {
                blackRemainingSeconds--;
                if (blackRemainingSeconds < 0) blackRemainingSeconds = 0;
            }
            else
            {
                whiteRemainingSeconds--;
                if (whiteRemainingSeconds < 0) whiteRemainingSeconds = 0;
            }

            UpdateTimeLabels();

            if (blackRemainingSeconds == 0 || whiteRemainingSeconds == 0)
            {
                timerPerTurn.Stop();
                matchFinished = true;

                // Nếu trắng còn thời gian => người chơi (WHITE) thắng, ngược lại thua
                bool localWon = whiteRemainingSeconds > 0;

                // Hết giờ => N/A
                ShowEndForm_WinLose(localWon, "N/A", "N/A");
                return;
            }
        }

        private void UpdateTimeLabels()
        {
            lbl_Time_Right.Text = TimeSpan.FromSeconds(blackRemainingSeconds).ToString(@"hh\:mm\:ss");
            lbl_Time_Left.Text = TimeSpan.FromSeconds(whiteRemainingSeconds).ToString(@"hh\:mm\:ss");
        }
        // =========================================================
        // DOUBLE PASS – GET SCORE
        // =========================================================
        private void Game_DoublePassHappened()
        {
            if (matchFinishedByDoublePass) return;
            matchFinishedByDoublePass = true;
            matchFinished = true;

            timerPerTurn.Stop();

            double blackScore, whiteScore;

            if (scoringRule.Equals("Chinese", StringComparison.OrdinalIgnoreCase))
            {
                var scorer = new ChineseScoring();
                (blackScore, whiteScore) = scorer.Calculate(game);
            }
            else
            {
                var scorer = new JapaneseScoring();
                (blackScore, whiteScore) = scorer.Calculate(game, blackPrisoners, whitePrisoners);
            }

            string blackText = $"{txt_Name_Right.Text}: {blackScore}";
            string whiteText = $"{txt_Name_Left.Text}: {whiteScore}";

            if (Math.Abs(blackScore - whiteScore) < 1e-6)
            {                
                MessageBox.Show("Result: DRAW.", "Match Ended");
                CloseBackToGamemode();
                return;
            }
            else
            {
                bool localWon = (whiteScore > blackScore); // người chơi là WHITE
                ShowEndForm_WinLose(localWon, blackText, whiteText);
                return;
            }
        }

        // =========================================================
        // BTN_UNDO
        // =========================================================

        private void btn_Undo_Click(object sender, EventArgs e)
        {
            if (matchFinished || game == null)
            {
                MessageBox.Show("The match has ended. Undo is not allowed.", "Undo");
                return;
            }

            if (isComputerThinking)
            {
                MessageBox.Show("The computer is thinking. Please wait.", "Undo");
                return;
            }

            if (game.CurrentPlayer != localColor)
            {
                MessageBox.Show("You can only undo on your turn.", "Undo");
                return;
            }

            if (moveHistory.Count < 2)
            {
                MessageBox.Show("Not enough moves to undo.", "Undo");
                return;
            }

            // Xoá 2 nước cuối (máy + bạn)
            moveHistory.RemoveAt(moveHistory.Count - 1);
            moveHistory.RemoveAt(moveHistory.Count - 1);

            RebuildGameFromHistory();
            pl_Board.Invalidate();
            pl_Board.Update();
        }

        private void RebuildGameFromHistory()
        {
            game = new Game_Engine(boardSize);
            game.DoublePassHappened += Game_DoublePassHappened;
            ai = new Game_AIcore(game, machineColor, localColor, boardSize, rng);

            blackPrisoners = 0;
            whitePrisoners = 0;

            foreach (var mv in moveHistory)
            {
                if (mv.IsPass)
                {
                    game.Pass();
                }
                else
                {
                    int colorBefore = game.CurrentPlayer;
                    if (game.TryPlayMove(mv.X, mv.Y, out int captured, out string error))
                    {
                        if (colorBefore == machineColor) blackPrisoners += captured;
                        else if (colorBefore == localColor) whitePrisoners += captured;
                    }
                }
            }

            matchFinished = false;
            matchFinishedByDoublePass = false;

            // rebuild => đổi trạng thái
            stateVersion++;
        }

        // =========================================================
        // BTN_QUIT
        // =========================================================

        private void btn_Quit_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to leave the current match?\nYou will be counted as a LOSS.",
                "Leave Match",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if(result == DialogResult.Yes)
{
                matchFinished = true;
                timerPerTurn.Stop();

                // Quit => người chơi thua, điểm N/A
                ShowEndForm_WinLose(localWon: false, blackText: "N/A", whiteText: "N/A");
            }
        }

        // =========================================================
        //  END 
        // =========================================================

        private void btn_End_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to end the current match and score based on the current state?",
                "End Match",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);


            if (result != DialogResult.Yes) return;

            matchFinished = true;
            timerPerTurn.Stop();

            double blackScore, whiteScore;

            if (scoringRule.Equals("Chinese", StringComparison.OrdinalIgnoreCase))
            {
                var scorer = new ChineseScoring();
                (blackScore, whiteScore) = scorer.Calculate(game);
            }
            else
            {
                var scorer = new JapaneseScoring();
                (blackScore, whiteScore) = scorer.Calculate(game, blackPrisoners, whitePrisoners);
            }

            string blackText = $"{txt_Name_Right.Text}: {blackScore}";
            string whiteText = $"{txt_Name_Left.Text}: {whiteScore}";

            if (Math.Abs(blackScore - whiteScore) < 1e-6)
            {
                MessageBox.Show("Result: DRAW.", "Match Ended");
                CloseBackToGamemode();
                return;
            }

            bool localWon = whiteScore > blackScore; // người chơi là WHITE
            ShowEndForm_WinLose(localWon, blackText, whiteText);
        }

        // =========================================================
        // SHOW RESULT (WIN / LOSE) + BACK TO GAMEMODE 
        // =========================================================

        private bool endFormShown = false;
        private async void ShowEndForm_WinLose(bool localWon, string blackText, string whiteText)
        {
            if (endFormShown) return;
            endFormShown = true;
            await UpdateUserStats_AI(localWon);
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

            // Khi form kết quả đóng -> quay về GameMode và đóng Mode_W_Comp
            f.FormClosed += (s, e) =>
            {
                if (this.IsDisposed) return;

                this.BeginInvoke(new Action(() =>
                {
                    if (!this.IsDisposed)
                        CloseBackToGamemode();  
                }));
            };

            f.Show(this);
        }

        // =========================================================
        // FORM CLOSING (click X / Alt+F4) => xử lý giống btn_Quit
        // =========================================================
        private void Mode_W_Comp_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (closingFromCode) return;

            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                QuitWithConfirm();
            }
        }
        private void QuitWithConfirm()
        {
            if (matchFinished)
            {
                CloseBackToGamemode();
                return;
            }

            var confirm = MessageBox.Show(
                "Are you sure you want to leave the match?",
                "Confirm Exit",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            matchFinished = true;
            timerPerTurn.Stop();
            CloseBackToGamemode();
        }

        // =========================================================
        // BACK TO GAME MODE
        // =========================================================
        private void CloseBackToGamemode()
        {
            if (closingFromCode) return;
            closingFromCode = true;

            try { timerPerTurn?.Stop(); } catch { }

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

            Close();
        }
        private async Task UpdateUserStats_AI(bool localWon)
        {
            if (localPlayer == null || string.IsNullOrWhiteSpace(localPlayer.Id)) return;

            try
            {
                // Phải dùng idToken đã nhận từ lúc đăng nhập
                var db = new RealtimeDatabaseService(this.idToken);

                // Lấy stats cũ về để cộng dồn
                var stats = await db.GetAsync<UserStats>($"users/{localPlayer.Id}/stats") ?? new UserStats();

                stats.play_ai++; // Tăng số trận chơi với máy
                if (localWon) stats.win_ai++; // Nếu thắng thì tăng số trận thắng

                // Ghi đè lại node stats
                await db.SetAsync($"users/{localPlayer.Id}/stats", stats);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating stats: " + ex.Message);
            }
        }
    }
}
