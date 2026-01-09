using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Co_Vay
{
    public partial class Online_Privatematch : Form
    {
        private readonly FirebaseService firebase;
        private readonly RealtimeDatabaseService realtimeDb;

        private PlayerInfo localPlayer;
        private MatchInfo currentMatch;

        // Timer cho người TẠO PHÒNG – đợi người khác vào
        private System.Windows.Forms.Timer timerWaitOpponent;

        public Online_Privatematch() : this(null)
        {
        }

        public Online_Privatematch(PlayerInfo player)
        {
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            // Đặt default cho combo rule + time
            if (cbo_Rule.Items.Count == 0)
            {
                cbo_Rule.Items.Add("Japanese");
                cbo_Rule.Items.Add("Chinese");
            }
            if (cbo_Rule.SelectedIndex < 0)
                cbo_Rule.SelectedIndex = 0;     // default: Japanese

            if (cbo_Time.Items.Count == 0)
            {
                cbo_Time.Items.AddRange(new object[]
                {
                    "15 minutes",
                    "30 minutes",
                    "1 hour",
                    "2 hours",
                    "3 hours"
                });

            }
            if (cbo_Time.SelectedIndex < 0)
                cbo_Time.SelectedIndex = 1;     // default: 30 phút

            firebase = new FirebaseService();
            realtimeDb = new RealtimeDatabaseService();

            this.FormClosing += Online_Privatematch_FormClosing;

            // BẮT BUỘC phải truyền PlayerInfo
            if (player == null)
            {
                MessageBox.Show(
                    "Online_Privatematch must be opened with player information (PlayerInfo). " +
                    "Please pass PlayerInfo from the login form / previous screen.",
                    "Configuration Error");

                Close();
                return;
            }

            localPlayer = player;

            timerWaitOpponent = new System.Windows.Forms.Timer();
            timerWaitOpponent.Interval = 1000;
            timerWaitOpponent.Tick += timerWaitOpponent_Tick;
        }

        // ===================== Helpers =========================

        // Lấy số phút tương ứng từ combo thời gian
        private int GetSelectedTimeMinutes()
        {
            switch (cbo_Time.SelectedIndex)
            {
                case 0: return 15;
                case 1: return 30;
                case 2: return 60;
                case 3: return 120;
                case 4: return 180;
                default: return 30;
            }
        }

        // Lấy key luật "Japanese" / "Chinese"
        private string GetSelectedRule()
        {
            var text = (cbo_Rule.SelectedItem ?? "Japanese").ToString();
            if (text.IndexOf("chinese", StringComparison.OrdinalIgnoreCase) >= 0 ||
                text.IndexOf("Trung", StringComparison.OrdinalIgnoreCase) >= 0)
                return "Chinese";
            return "Japanese";
        }

        // =========================================================
        //  TẠO PHÒNG PRIVATE – người tạo = ĐEN
        //  Lưu thêm: ScoringRule + TimePerSideMinutes
        // =========================================================
        private async void btn_CreateRoom_Click(object sender, EventArgs e)
        {
            btn_CreateRoom.Enabled = false;

            try
            {
                string matchId = await firebase.GenerateMatchIdAsync();

                string scoringRule = GetSelectedRule();
                int timeMinutes = GetSelectedTimeMinutes();

                currentMatch = new MatchInfo
                {
                    MatchId = matchId,
                    RoomType = RoomType.Private,
                    Status = MatchStatus.Waiting,
                    BlackPlayer = localPlayer,   // người tạo phòng cầm Đen
                    WhitePlayer = null,
                    StartedAt = DateTime.UtcNow,
                    ResultType = MatchResultType.None,

                    // 2 thuộc tính mới trong MatchInfo (xem phần 3)
                    ScoringRule = scoringRule,
                    TimePerSideMinutes = timeMinutes
                };

                await firebase.SaveMatchAsync(currentMatch);

                txt_YourRoomCode.Text = matchId;

                MessageBox.Show(
                    $"Private room created.\n" +
                    $"Rule: {scoringRule}\n" +
                    $"Time per side: {timeMinutes} minutes\n\n" +
                    "Send the room code to your friend to join.\n" +
                    "The match will start automatically when a player joins.",
                    "Room Created Successfully");

                timerWaitOpponent.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while creating the room:\n" + ex.Message, "Error");
                btn_CreateRoom.Enabled = true;
            }
        }

        // Người tạo phòng: polling xem WhitePlayer đã có chưa
        private async void timerWaitOpponent_Tick(object sender, EventArgs e)
        {
            if (currentMatch == null || string.IsNullOrWhiteSpace(currentMatch.MatchId))
                return;

            try
            {
                var match = await firebase.LoadMatchAsync(currentMatch.MatchId);
                if (match == null)
                    return;

                if (match.Status == MatchStatus.InProgress &&
                    match.WhitePlayer != null)
                {
                    timerWaitOpponent.Stop();
                    OpenModeOnline(match.MatchId);
                }
            }
            catch
            {
                // lỗi nhỏ bỏ qua
            }
        }

        // =========================================================
        //  THAM GIA PHÒNG PRIVATE BẰNG MÃ – người join = TRẮNG
        // =========================================================
        private async void btn_JoinRoom_Click(object sender, EventArgs e)
        {
            string code = txt_JoinCode.Text.Trim();

            if (string.IsNullOrEmpty(code))
            {
                MessageBox.Show("Please enter the room code.", "Notification");
                return;
            }

            try
            {
                var match = await firebase.LoadMatchAsync(code);
                if (match == null)
                {
                    MessageBox.Show("No room found with code: " + code, "Error");
                    return;
                }

                if (match.RoomType != RoomType.Private)
                {
                    MessageBox.Show("This room code is not a Private room.", "Error");
                    return;
                }

                if (match.Status != MatchStatus.Waiting)
                {
                    MessageBox.Show("This room is already full or the match has started.", "Error");
                    return;
                }

                if (match.BlackPlayer == null)
                {
                    MessageBox.Show("The room does not have a valid host.", "Error");
                    return;
                }

                // Người tham gia = TRẮNG
                match.WhitePlayer = localPlayer;
                match.Status = MatchStatus.InProgress;
                match.StartedAt = DateTime.UtcNow;

                await firebase.SaveMatchAsync(match);

                OpenModeOnline(match.MatchId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while joining the room:\n" + ex.Message, "Error");
            }
        }
        // =========================================================
        // THOÁT QUAY LẠI GAME_MODE_ONLINE
        // =========================================================
        private void Online_Privatematch_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Nếu đang bị đóng bởi người dùng (bấm X)
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;   // chặn đóng mặc định
                GoBack();          
            }
        }

        private void GoBack()
        {
            // dừng timer nếu đang chờ người chơi
            if (timerWaitOpponent != null)
                timerWaitOpponent.Stop();

            var f = new Game_Mode_Online(localPlayer);
            f.Show();

            this.FormClosing -= Online_Privatematch_FormClosing; // tránh gọi 2 lần
            this.Close();
        }

        // =========================================================
        //  BTN_BACK
        // =========================================================
        private void btn_Back_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        // =========================================================
        //  MỞ MODE_ONLINE
        // =========================================================
        private void OpenModeOnline(string matchId)
        {
            var f = new Mode_Online(matchId, localPlayer);
            f.Show();
            this.Hide();
        }
    }
}
