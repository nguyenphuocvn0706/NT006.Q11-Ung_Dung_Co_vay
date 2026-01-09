using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Co_Vay
{
    public partial class Rankings : Form
    {
        private readonly Man_Hinh_Chinh mainForm;
        private readonly RealtimeDatabaseService realtimeDb;

        public Rankings(Man_Hinh_Chinh mainForm, string idToken)
        {
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.mainForm = mainForm;
            realtimeDb = new RealtimeDatabaseService(idToken);
        }

        private async void Rankings_Load(object sender, EventArgs e)
        {
            await LoadRankingsAsync();
        }

        private async Task LoadRankingsAsync()
        {
            pnl_List.Controls.Clear();

            var users = await realtimeDb.GetAsync<Dictionary<string, UserModel>>("users");
            if (users == null) return;

            var ranked = users.Values
                .OrderByDescending(u => u.elo)
                .ToList();

            int rank = 1;
            foreach (var u in ranked)
            {
                var row = CreateRow(rank, u.username ?? "Unknown", u.elo);
                pnl_List.Controls.Add(row);
                rank++;
            }
        }

        private Panel CreateRow(int rank, string name, int elo)
        {
            Panel row = new Panel
            {
                Width = pnl_List.Width - 40,
                Height = 45,
                BackColor = Color.Transparent
            };

            Label lblRank = new Label
            {
                Text = rank.ToString(),
                Location = new Point(20, 10),
                Size = new Size(80, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.DarkRed
            };

            Label lblName = new Label
            {
                Text = name,
                Location = new Point(220, 10),
                Size = new Size(200, 30),
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.DarkRed
            };

            Label lblElo = new Label
            {
                Text = elo.ToString(),
                Location = new Point(600, 10),
                Size = new Size(100, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.DarkRed
            };

            row.Controls.Add(lblRank);
            row.Controls.Add(lblName);
            row.Controls.Add(lblElo);

            return row;
        }

        //  BACK: 
        private void btn_Back_Click(object sender, EventArgs e)
        {
            mainForm.Show();
            this.Close();
        }
    }
}
