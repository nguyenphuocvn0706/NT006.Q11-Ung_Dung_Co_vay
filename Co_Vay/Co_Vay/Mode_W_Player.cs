using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;

namespace Co_Vay
{
    public partial class Mode_W_Player : Form
    {
        // ===== Board settings (giống Mode_Online) =====
        private readonly int boardSize = 19;
        private readonly int cellSize = 30;        

        // ===== Rule + time =====
        private string scoringRule = "Japanese";
        private int timePerSideMinutes = 30;

        // ===== Engine =====
        private Game_Engine game;

        // ===== Timers =====
        private System.Windows.Forms.Timer timerPerTurn;
        private int blackRemainingSeconds; // bên phải - Đen
        private int whiteRemainingSeconds; // bên trái - Trắng

        // ===== State =====
        private bool matchFinished = false;
        private bool closingFromCode = false;
        private bool matchFinishedByDoublePass = false;

        // ===== Prisoners (phục vụ Japanese scoring) =====
        private int blackPrisoners = 0; // số quân TRẮNG bị ĐEN bắt
        private int whitePrisoners = 0; // số quân ĐEN bị TRẮNG bắt

        public Mode_W_Player() : this("Japanese", 30, "Player1(White)", "Player2(Black)")
        {
        }
                
        public Mode_W_Player(string rule, int minutes, string player1NameLeft, string player2NameRight)
        {
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            scoringRule = string.IsNullOrWhiteSpace(rule) ? "Japanese" : rule;
            timePerSideMinutes = minutes > 0 ? minutes : 30;            

            // Init game
            game = new Game_Engine(boardSize);
            game.DoublePassHappened += Game_DoublePassHappened;

            // Double-buffer panel (giống Mode_Online) :contentReference[oaicite:2]{index=2}
            typeof(Panel).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, pl_Board, new object[] { true });

            // Hook events
            pl_Board.Paint += pl_Board_Paint;
            pl_Board.MouseClick += pl_Board_MouseClick;

            this.Load += Mode_W_Player_Load;
            this.FormClosing += Mode_W_Player_FormClosing;
          
            // Names
            txt_Name_Left.Text = string.IsNullOrWhiteSpace(player1NameLeft) ? "Player1(White)" : player1NameLeft;
            txt_Name_Right.Text = string.IsNullOrWhiteSpace(player2NameRight) ? "Player2(Black)" : player2NameRight;

            // Timer per turn
            timerPerTurn = new System.Windows.Forms.Timer();
            timerPerTurn.Interval = 1000;
            timerPerTurn.Tick += timerPerTurn_Tick;
        }

        private void Mode_W_Player_Load(object sender, EventArgs e)
        {
            // Init time
            blackRemainingSeconds = timePerSideMinutes * 60;
            whiteRemainingSeconds = timePerSideMinutes * 60;
            UpdateTimeLabels();

            // Show board
            pl_Board.Visible = true;
            pl_Board.Invalidate();

            timerPerTurn.Start();
        }

        // =========================================================
        // DRAW BOARD
        // =========================================================

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
                    // ngang
                    g.DrawLine(
                        pen,
                        cellSize,
                        cellSize + i * cellSize,
                        cellSize + cellSize * (boardSize-1),
                        cellSize + i * cellSize 
                    );

                    // dọc
                    g.DrawLine(
                        pen,
                        cellSize + i * cellSize,
                        cellSize,
                        cellSize + i * cellSize,
                        cellSize + cellSize * (boardSize - 1) 
                    );
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
        // PLAY MOVE (OFFLINE LUÂN PHIÊN)
        // =========================================================
               
        private void pl_Board_MouseClick(object sender, MouseEventArgs e)
        {
            if (matchFinished) return;

            // quy đổi tọa độ click về hệ tọa độ của bàn cờ (đã dịch)
            int localX = e.X - boardOrigin.X;
            int localY = e.Y - boardOrigin.Y;

            int x = (localX - cellSize / 2) / cellSize;
            int y = (localY - cellSize / 2) / cellSize;

            if (!game.InBounds(x, y))
                return;

            int mover = game.CurrentPlayer; // 1=Đen, 2=Trắng

            if (!game.TryPlayMove(x, y, out int captured, out string error))
            {
                if (!string.IsNullOrWhiteSpace(error))
                    MessageBox.Show(error);
                return;
            }

            // Update prisoners for Japanese scoring
            if (captured > 0)
            {
                if (mover == 1) blackPrisoners += captured; // đen bắt trắng
                else whitePrisoners += captured;            // trắng bắt đen
            }

            pl_Board.Invalidate();
        }

        // =========================================================
        // TIMER - COUNT DOWN FOR CURRENT TURN SIDE
        // =========================================================
        private void timerPerTurn_Tick(object sender, EventArgs e)
        {
            if (matchFinished) return;

            // Ai đến lượt thì trừ thời gian của bên đó (giống Mode_Online) :contentReference[oaicite:3]{index=3}
            if (game.CurrentPlayer == 1)
            {
                blackRemainingSeconds--;
                if (blackRemainingSeconds <= 0)
                {
                    blackRemainingSeconds = 0;
                    UpdateTimeLabels();
                    EndGameByTimeout(winnerIsWhite: true);
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
                    EndGameByTimeout(winnerIsWhite: false);
                    return;
                }

            }

            UpdateTimeLabels();
        }

        private void UpdateTimeLabels()
        {
            lbl_Time_Right.Text = TimeSpan.FromSeconds(Math.Max(0, blackRemainingSeconds)).ToString(@"hh\:mm\:ss");
            lbl_Time_Left.Text = TimeSpan.FromSeconds(Math.Max(0, whiteRemainingSeconds)).ToString(@"hh\:mm\:ss");
        }

        private void EndGameByTimeout(bool winnerIsWhite)
        {
            matchFinished = true;
            timerPerTurn.Stop();
            pl_Board.Enabled = false;
            btn_Skip.Enabled = false;
            btn_End.Enabled = false;
            btn_Quit.Enabled = false;

            string winnerName = winnerIsWhite ? txt_Name_Left.Text : txt_Name_Right.Text;
            ShowWinnerResultForm(winnerIsWhite, "N/A", "N/A");
        }

        // =========================================================
        // DOUBLE PASS -> CALC SCORE -> ANNOUNCE WINNER
        // =========================================================
        private void Game_DoublePassHappened()
        {
            if (matchFinishedByDoublePass) return;
            matchFinishedByDoublePass = true;

            FinishByScoring("Match Ended (Both players passed)");
        }

        // =========================================================
        // BUTTONS
        // =========================================================

        // btn_Skip: chỉ cần 1 nút (offline) => bên nào đang tới lượt bấm thì PASS lượt đó
        private void btn_Skip_Click(object sender, EventArgs e)
        {
            if (matchFinished) return;

            var confirm = MessageBox.Show(
                "Pass the current turn?\nIf both sides pass consecutively, the match will end and be scored.",
                "Confirm Pass",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            game.Pass(); // engine tự đổi lượt và tự trigger DoublePassHappened nếu double pass 
            pl_Board.Invalidate();
        }

        // btn_End: bất kể lượt ai, xác nhận rồi kết thúc + tính điểm + công bố thắng
        private void btn_End_Click(object sender, EventArgs e)
        {
            if (matchFinished) return;

            var confirm = MessageBox.Show(
                "Are you sure you want to end the match now?\nThe system will calculate the score and announce the winner.",
                "Confirm End Match",

                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            FinishByScoring("Match Ended (Manual End)");
        }

        // btn_Quit: xác nhận rồi thoát về GameMode (offline không cần đồng bộ)
        private void btn_Quit_Click(object sender, EventArgs e)
        {
            QuitWithConfirm();
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
        // FINISH & SCORE
        // =========================================================
        private void FinishByScoring(string title)
        {
            matchFinished = true;

            timerPerTurn.Stop();
            pl_Board.Enabled = false;
            btn_Skip.Enabled = false;
            btn_End.Enabled = false;
            btn_Quit.Enabled = false;

            double blackScore, whiteScore;

            if (scoringRule.Equals("Chinese", StringComparison.OrdinalIgnoreCase))
            {
                var scorer = new ChineseScoring();
                (blackScore, whiteScore) = scorer.Calculate(game);
            }
            else
            {
                // Japanese cần prisoners
                var scorer = new JapaneseScoring();
                (blackScore, whiteScore) = scorer.Calculate(game, blackPrisoners, whitePrisoners);
            }

            string blackText = $"{txt_Name_Right.Text}: {blackScore:0.##}";
            string whiteText = $"{txt_Name_Left.Text}: {whiteScore:0.##}";

            string winnerText;
            if (blackScore > whiteScore)
                winnerText = $"Winning side: BLACK (Player2 - {txt_Name_Right.Text})";
            else if (whiteScore > blackScore)
                winnerText = $"Winning side: WHITE (Player1 - {txt_Name_Left.Text})";
            else
                winnerText = "Result: DRAW";

            if (blackScore > whiteScore)
                ShowWinnerResultForm(false, blackText, whiteText);
            else if (whiteScore > blackScore)
                ShowWinnerResultForm(true, blackText, whiteText);
            else
                CloseBackToGamemode(); 
        }

        // =========================================================
        //  RESULT FORM 
        // =========================================================
        private bool resultFormShown = false;

        private void ShowWinnerResultForm(bool winnerIsWhite, string blackText, string whiteText)
        {
            if (resultFormShown) return;
            resultFormShown = true;

            var f = new Form_Result_Player();
            f.SetResult(winnerIsWhite ? "WHITE WON" : "BLACK WON");
            f.SetScores(blackText, whiteText);

            f.FormClosed += (s, e) =>
            {
                if (IsDisposed) return;
                BeginInvoke(new Action(() =>
                {
                    if (!IsDisposed) CloseBackToGamemode();
                }));
            };

            f.Show(this);
        }


        // =========================================================
        // FORM CLOSING (click X / Alt+F4) => xử lý giống btn_Quit
        // =========================================================
        private void Mode_W_Player_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (closingFromCode) return;

            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                QuitWithConfirm();
            }
        }

        // =========================================================
        // BACK TO GAMEMODE
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

            try { this.Close(); } catch { }
        }
    }
}
