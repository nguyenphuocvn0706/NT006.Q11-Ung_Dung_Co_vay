using System;
using System.Windows.Forms;

namespace Co_Vay
{
    public partial class Match_Setting_Comp : Form
    {
        private readonly Man_Hinh_Chinh mainForm;
        private readonly PlayerInfo localPlayer;
        private readonly string idToken;
               
        public Match_Setting_Comp(Man_Hinh_Chinh mainForm, PlayerInfo player, string idToken)
        {
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.mainForm = mainForm;
            this.idToken = idToken;

            localPlayer = player ?? new PlayerInfo();

            InitCombos();

            button1.Click += button1_Click;
            btn_Back.Click += btn_Back_Click;
        }

        private void InitCombos()
        {
            if (comboBox1.Items.Count == 0)
            {
                comboBox1.Items.Add("Japanese");
                comboBox1.Items.Add("Chinese");
            }
            comboBox1.SelectedIndex = 0;

            if (comboBox2.Items.Count == 0)
            {
                comboBox2.Items.Add("15 minutes");
                comboBox2.Items.Add("30 minutes");
                comboBox2.Items.Add("1 hour");
                comboBox2.Items.Add("2 hours");
                comboBox2.Items.Add("3 hours");
            }
            comboBox2.SelectedIndex = 1;
        }

        private string GetSelectedRule()
        {
            return comboBox1.SelectedItem?.ToString().Contains("Chinese") == true
                ? "Chinese"
                : "Japanese";
        }

        private int GetSelectedTimeMinutes()
        {
            string s = comboBox2.SelectedItem?.ToString() ?? "30";
            if (s.Contains("15")) return 15;
            if (s.Contains("1 hour")) return 60;
            if (s.Contains("2 hours")) return 120;
            if (s.Contains("3 hours")) return 180;
            return 30;
        }

        // ================= START =================
        private void button1_Click(object sender, EventArgs e)
        {
            string rule = GetSelectedRule();
            int minutes = GetSelectedTimeMinutes();

            // Gán userId đúng
            localPlayer.Id = mainForm.UserId;

            Mode_W_Comp mode = new Mode_W_Comp(localPlayer, rule, minutes, idToken);
            mode.StartPosition = FormStartPosition.CenterScreen;
            mode.Show();

            this.Close();
        }

        // ================= BACK =================
        private void btn_Back_Click(object sender, EventArgs e)
        {
            GameMode gm = new GameMode(mainForm);
            gm.StartPosition = FormStartPosition.CenterScreen;
            gm.Show();

            this.Close();
        }
    }
}
