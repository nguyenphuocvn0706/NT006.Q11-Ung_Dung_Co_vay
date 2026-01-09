using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Co_Vay
{
    public partial class Match_History : Form
    {
        private readonly RealtimeDatabaseService db;
        private readonly string currentUserId;
        private readonly Man_Hinh_Chinh mainForm;

        // CONSTRUCTOR 
        public Match_History(Man_Hinh_Chinh mainForm, string userId, string idToken)
        {
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.mainForm = mainForm;
            currentUserId = userId;
            db = new RealtimeDatabaseService(idToken);
        }

        private async void Match_History_Load(object sender, EventArgs e)
        {
            await LoadMatchesAsync();
        }

        private async Task LoadMatchesAsync()
        {
            dgvMatches.Rows.Clear();

            var matches = await db.GetMatchesAsync();
            if (matches == null) return;

            // ===== LỌC + CHUYỂN SANG DANH SÁCH CÓ THỜI GIAN =====
            var filteredMatches = new List<(dynamic Match, DateTime EndTime)>();

            foreach (var m in matches.Values)
            {
                // 1. Chỉ lấy trận RANDOM
                if (m.IsRandom != true)
                    continue;

                // 2. Chỉ lấy trận của user hiện tại
                bool isMyMatch =
                    m.WhitePlayer?.Id == currentUserId ||
                    m.BlackPlayer?.Id == currentUserId;

                if (!isMyMatch)
                    continue;

                // 3. Parse thời gian kết thúc
                if (!string.IsNullOrEmpty(m.EndedAt) &&
                    DateTime.TryParse(m.EndedAt, out DateTime endTime))
                {
                    filteredMatches.Add((m, endTime));
                }
            }

            // ===== SẮP XẾP: MỚI NHẤT → CŨ NHẤT =====
            foreach (var item in filteredMatches
                     .OrderByDescending(x => x.EndTime))
            {
                var m = item.Match;

                string whiteName = m.WhitePlayer?.Name ?? "Unknown";
                string blackName = m.BlackPlayer?.Name ?? "Unknown";
                string players = $"{whiteName} vs {blackName}";

                string result;
                if (m.ResultType == 3)
                {
                    result = "Draw";
                }
                else if (m.WinnerPlayerId == m.WhitePlayer?.Id)
                {
                    result = $"{whiteName} Win";
                }
                else if (m.WinnerPlayerId == m.BlackPlayer?.Id)
                {
                    result = $"{blackName} Win";
                }
                else
                {
                    result = "Unknown";
                }

                string time = item.EndTime
                    .ToLocalTime()
                    .ToString("dd/MM/yyyy HH:mm");

                dgvMatches.Rows.Add(result, players, time);
            }
        }

        // ✅ BACK: QUAY LẠI ĐÚNG MAN_HINH_CHINH ĐÃ LOGIN
        private void btnBack_Click(object sender, EventArgs e)
        {
            mainForm.Show();
            this.Close();
        }
    }
}
