using System;
using System.Windows.Forms;
using Firebase.Auth;

namespace Co_Vay
{
    public partial class Man_Hinh_Chinh : Form
    {
        // ===== GIỮ TRẠNG THÁI ĐĂNG NHẬP DUY NHẤT =====
        private readonly FirebaseAuthClient authClient;

        // ===== EXPOSE DỮ LIỆU CẦN THIẾT (READ-ONLY) =====
        public string UserId => authClient.User.Uid;
        public string IdToken => authClient.User.Credential.IdToken;        

        //  CONSTRUCTOR 
        public Man_Hinh_Chinh(FirebaseAuthClient authClient)
        {
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            if (authClient == null || authClient.User == null)
            {
                MessageBox.Show(
                    "User not logged in!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                Close();
                return;
            }

            this.authClient = authClient;
        }

        // ================= PROFILE =================
        private void btn_Profile_Click(object sender, EventArgs e)
        {
            Profile f = new Profile(this, authClient);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
            this.Hide();
        }

        // ================= REWARDS =================
        private void btn_Rewards_Click(object sender, EventArgs e)
        {
            Reward f = new Reward(this, UserId, IdToken);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
            this.Hide();
        }

        // ================= MATCH HISTORY =================
        private void btn_History_Click(object sender, EventArgs e)
        {
            Match_History f = new Match_History(this, UserId, IdToken);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
            this.Hide();
        }

        // ================= PLAY =================
        private void btn_Play_Click(object sender, EventArgs e)
        {
            GameMode f = new GameMode(this);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
            this.Hide();
        }

        // ================= RANKINGS =================
        private void btn_Rankings_Click(object sender, EventArgs e)
        {
            Rankings f = new Rankings(this, IdToken);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
            this.Hide();
        }

        // ================= LOG OUT =================
        private void btn_Log_Out_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to log out?",
                "Confirm Log Out",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                authClient.SignOut();

                Trang_Chu trangChu = new Trang_Chu();
                trangChu.StartPosition = FormStartPosition.CenterScreen;
                trangChu.Show();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error during logout: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
