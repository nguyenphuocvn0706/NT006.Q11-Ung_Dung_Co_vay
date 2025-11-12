using System;
using System.Windows.Forms;
using Firebase.Auth;

namespace Co_Vay
{
    public partial class Man_Hinh_Chinh : Form
    {
        private FirebaseAuthClient authClient;

        // 🔹 Constructor mặc định (cũ)
        public Man_Hinh_Chinh()
        {
            InitializeComponent();
        }

        // 🔹 Constructor mới (nhận authClient từ form đăng nhập)
        public Man_Hinh_Chinh(FirebaseAuthClient authClient)
        {
            InitializeComponent();
            this.authClient = authClient;
        }

        private void btn_Profile_Click(object sender, EventArgs e)
        {
            // 🔹 Truyền authClient qua form Profile
            Profile profileForm = new Profile(this, authClient);
            profileForm.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btn_Rewards_Click(object sender, EventArgs e)
        {

        }
    }
}
