using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Co_Vay
{
    public partial class Match_Setting_Player : Form
    {

        private PlayerInfo localPlayer;

        public Match_Setting_Player() : this(null)
        {
        }

        // Constructor 
        public Match_Setting_Player(PlayerInfo player)
        {
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            localPlayer = player ?? new PlayerInfo();

            InitCombos();

            button1.Click += button1_Click;   // nút Start
            btn_Back.Click += btn_Back_Click;
        }

        private void InitCombos()
        {
            // comboBox1: luật tính điểm
            if (comboBox1.Items.Count == 0)
            {
                comboBox1.Items.Add("Japanese");
                comboBox1.Items.Add("Chinese");
            }
            if (comboBox1.SelectedIndex < 0)
                comboBox1.SelectedIndex = 0;

            // comboBox2: thời gian
            if (comboBox2.Items.Count == 0)
            {
                comboBox2.Items.Add("15 minutes");
                comboBox2.Items.Add("30 minutes");
                comboBox2.Items.Add("1 hour");
                comboBox2.Items.Add("2 hours");
                comboBox2.Items.Add("3 hours");
            }
            if (comboBox2.SelectedIndex < 0)
                comboBox2.SelectedIndex = 1; 
        }

        private string GetSelectedRule()
        {
            if (comboBox1.SelectedItem == null)
                return "Japanese";

            var s = comboBox1.SelectedItem.ToString();
            if (s.IndexOf("chinese", StringComparison.OrdinalIgnoreCase) >= 0 ||
                s.IndexOf("Trung", StringComparison.OrdinalIgnoreCase) >= 0)
                return "Chinese";

            return "Japanese";
        }

        private int GetSelectedTimeMinutes()
        {
            if (comboBox2.SelectedItem == null)
                return 30;

            string s = comboBox2.SelectedItem.ToString();
            if (s.Contains("15"))
                return 15;
            if (s.Contains("30"))
                return 30;
            if (s.Contains("1 hour") || s.Contains("1h"))
                return 60;
            if (s.Contains("2 hours") || s.Contains("2h"))
                return 120;
            if (s.Contains("3 hours") || s.Contains("3h"))
                return 180;

            return 30;
        }

        // Nút START
        private void button1_Click(object sender, EventArgs e)
        {
            string rule = GetSelectedRule();
            int minutes = GetSelectedTimeMinutes();

            // OFFLINE 2 người chơi: Player1 (trái - Trắng), Player2 (phải - Đen)
            string p1 = string.IsNullOrWhiteSpace(localPlayer?.Name) ? "Player1" : localPlayer.Name;
            string p2 = "Player2";

            var mode = new Mode_W_Player(rule, minutes, p1, p2);
            mode.StartPosition = FormStartPosition.CenterScreen;
            mode.Show();

            this.Close();
        }


        // Nút Back – quay về GameMode
        private void btn_Back_Click(object sender, EventArgs e)
        {
            try
            {
                var mainForm = Application.OpenForms["Man_Hinh_Chinh"] as Man_Hinh_Chinh;
                if (mainForm != null)
                {
                    GameMode gm = new GameMode(mainForm);
                    gm.Show();
                }
            }
            catch
            {
            }

            this.Close();
        }
    }
}
