using System;
using System.Windows.Forms;
using Firebase.Auth;

namespace Co_Vay
{
    public partial class Dang_Nhap : Form
    {
        private Trang_Chu trangChuForm;

        public Dang_Nhap(Trang_Chu formTrangChu)
        {
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            trangChuForm = formTrangChu;
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            // Quay lại trang chủ
            this.Close();
            trangChuForm.Show();
        }

        private async void btn_DangNhap2_Click(object sender, EventArgs e)
        {
            string username = txb_Username.Text.Trim();  // Lấy username người dùng nhập
            string password = txb_Password.Text.Trim();  // Lấy mật khẩu người dùng nhập

            // Kiểm tra ô nhập trống
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password!",
                                "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Lấy email tương ứng với username trong Realtime Database
                var db = new RealtimeDatabaseService();
                string email = await db.GetEmailByUsernameAsync(username);

                if (string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Username does not exist!",
                                    "Invalid Username", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Đăng nhập Firebase bằng email và mật khẩu
                var firebase = new FirebaseService();
                var auth = await firebase.LoginAsync(email, password);

                // Kiểm tra xem email đã được xác minh hay chưa
                if (!auth.User.Info.IsEmailVerified)
                {
                    MessageBox.Show("Your email address is not verified yet! Please check your inbox and verify it before logging in.",
                                    "Email Not Verified", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Đăng nhập thành công
                MessageBox.Show("✅ Login successful!",
                                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Mở màn hình chính sau khi đăng nhập thành công
                Man_Hinh_Chinh mainForm = new Man_Hinh_Chinh(firebase.AuthClient);
                mainForm.Show();

                // Ẩn form đăng nhập để không bị chồng lên
                this.Hide();
            }
            catch (Firebase.Auth.FirebaseAuthException)
            {
                // Lỗi đăng nhập Firebase (sai mật khẩu hoặc tài khoản không tồn tại)
                MessageBox.Show("Incorrect username or password!",
                                "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Các lỗi khác (mất mạng, Firebase lỗi, v.v.)
                MessageBox.Show("Login error: " + ex.Message,
                                "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Quen_mat_khau_Click(object sender, EventArgs e)
        {
            // Mở form quên mật khẩu và ẩn form đăng nhập
            Quen_Mat_Khau f = new Quen_Mat_Khau(this);
            f.Show();
            this.Hide();
        }
    }
}
