using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Co_Vay
{
    public partial class Reward : Form
    {
        private readonly Man_Hinh_Chinh mainForm;
        private readonly string currentUserId;
        private readonly RealtimeDatabaseService realtimeDb;
        private List<Mission> missions;

        // ===================== MODELS =====================
        private class Mission
        {
            public string Id;
            public string Title;
            public int RewardElo;
            public Func<UserStats, bool> Condition;
        }

        private class UserStats
        {
            public int play_ai { get; set; }
            public int win_ai { get; set; }
            public int play_pvp { get; set; }
            public int win_pvp { get; set; }
        }

        // ===================== CONSTRUCTOR =====================
        // ✅ GIỐNG MATCH_HISTORY
        public Reward(Man_Hinh_Chinh mainForm, string userId, string idToken)
        {
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.mainForm = mainForm;
            currentUserId = userId;
            realtimeDb = new RealtimeDatabaseService(idToken);

            InitMissions();
            this.Load += Reward_Load;
        }

        private async void Reward_Load(object sender, EventArgs e)
        {
            await LoadMissionsAsync();
        }

        // ===================== INIT MISSIONS =====================
        private void InitMissions()
        {
            missions = new List<Mission>
            {
                new Mission { Id="play_ai_1", Title="Play 1 match against AI", RewardElo=5, Condition=s=>s.play_ai>=1 },
                new Mission { Id="win_ai_1", Title="Win against AI once", RewardElo=10, Condition=s=>s.win_ai>=1 },
                new Mission { Id="play_pvp_1", Title="Play 1 match against a player", RewardElo=10, Condition=s=>s.play_pvp>=1 },
                new Mission { Id="play_pvp_3", Title="Play 3 matches against players", RewardElo=20, Condition=s=>s.play_pvp>=3 },
                new Mission { Id="play_pvp_5", Title="Play 5 matches against players", RewardElo=30, Condition=s=>s.play_pvp>=5 },
                new Mission { Id="win_pvp_1", Title="Win against a player once", RewardElo=20, Condition=s=>s.win_pvp>=1 },
                new Mission { Id="win_pvp_5", Title="Win against players 5 times", RewardElo=50, Condition=s=>s.win_pvp>=5 },
                new Mission { Id="win_pvp_10", Title="Win against players 10 times", RewardElo=100, Condition=s=>s.win_pvp>=10 }
            };
        }

        // ===================== LOAD MISSIONS =====================
        private async Task LoadMissionsAsync()
        {
            pnl_Missions.Controls.Clear();

            var stats = await realtimeDb.GetAsync<UserStats>($"users/{currentUserId}/stats")
                        ?? new UserStats();

            var rewards = await realtimeDb.GetAsync<Dictionary<string, bool>>($"users/{currentUserId}/rewards")
                          ?? new Dictionary<string, bool>();

            int y = 10;

            foreach (var m in missions)
            {
                bool completed = m.Condition(stats);
                bool claimed = rewards.ContainsKey(m.Id);

                Panel row = new Panel
                {
                    Size = new Size(pnl_Missions.Width - 30, 50),
                    Location = new Point(10, y)
                };

                Label lblTitle = new Label
                {
                    Text = m.Title,
                    Location = new Point(20, 15),
                    Size = new Size(320, 25),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                };

                Label lblStatus = new Label
                {
                    Text = completed ? "Completed" : "Not completed",
                    Location = new Point(380, 15),
                    Size = new Size(150, 25)
                };

                Button btn = new Button
                {
                    Size = new Size(150, 30),
                    Location = new Point(550, 10),
                    Tag = m,
                    Enabled = completed && !claimed,
                    Text = claimed ? "Claimed" : completed ? "Claim" : "Not completed"
                };

                btn.Click += async (s, e) =>
                {
                    await ClaimRewardAsync((Mission)btn.Tag);
                };

                row.Controls.Add(lblTitle);
                row.Controls.Add(lblStatus);
                row.Controls.Add(btn);

                pnl_Missions.Controls.Add(row);
                y += 55;
            }
        }

        // ===================== CLAIM =====================
        private async Task ClaimRewardAsync(Mission m)
        {
            var claimed = await realtimeDb.GetAsync<bool?>(
                $"users/{currentUserId}/rewards/{m.Id}");

            if (claimed == true)
            {
                MessageBox.Show(
                    "You have already claimed this reward.",
                    "Reward",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            // ===== UPDATE ELO =====
            await realtimeDb.UpdateEloAsync(currentUserId, m.RewardElo);

            // ===== MARK AS CLAIMED =====
            await realtimeDb.SetAsync(
                $"users/{currentUserId}/rewards/{m.Id}", true);

            // ===== THÔNG BÁO RÕ RÀNG =====
            MessageBox.Show(
                $"🎉 Reward Claimed!\n\n" +
                $"Mission: {m.Title}\n" +
                $"ELO Received: +{m.RewardElo}",
                "Reward Claimed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            // ===== RELOAD UI =====
            await LoadMissionsAsync();
        }


        // ===================== BACK =====================
        // ✅ KHÔNG TẠO MAIN MỚI
        private void btn_Back_Click(object sender, EventArgs e)
        {
            mainForm.Show();
            this.Close();
        }
    }
}
