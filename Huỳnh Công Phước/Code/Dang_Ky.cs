using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firebase.Auth;

namespace Co_Vay
{
    public partial class Dang_Ky : Form
    {
        private Trang_Chu trangChuForm;

        public Dang_Ky(Trang_Chu formTrangChu)
        {
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            trangChuForm = formTrangChu;
        }

        private async void btn_DangKy_Click(object sender, EventArgs e)
        {
            string username = txb_Username.Text.Trim();  // Tên đăng nhập
            string email = txt_Email.Text.Trim();        // Email người dùng
            string password = txb_Password1.Text.Trim(); // Mật khẩu
            string confirm = txb_Password2.Text.Trim();  // Nhập lại mật khẩu

            // Kiểm tra nhập đủ thông tin
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirm))
            {
                MessageBox.Show("Please fill in all required fields!", "Missing Data",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra định dạng email
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Invalid email format! Please enter a valid email (e.g., example@gmail.com).",
                                "Invalid Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra độ dài mật khẩu
            if (password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long!",
                                "Weak Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra xác nhận mật khẩu
            if (password != confirm)
            {
                MessageBox.Show("Password confirmation does not match!",
                                "Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xác nhận thông tin đăng ký trước khi gửi
            DialogResult confirmInfo = MessageBox.Show(
                $"⚠️ Please review your registration details carefully!\n\n" +
                $"👤 Username: {username}\n" +
                $"📧 Email: {email}\n\n" +
                $"If you entered an incorrect or fake email, you will NOT receive the verification email " +
                $"and will NOT be able to log in.\n\n" +
                $"👉 Are you sure you want to create this account?",
                "Confirm Registration Information",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning
            );

            // Nếu người dùng chọn Cancel thì hủy đăng ký
            if (confirmInfo != DialogResult.OK)
            {
                MessageBox.Show("Registration has been canceled.",
                                "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // Kiểm tra username đã tồn tại trong Realtime Database chưa
                var dbCheck = new RealtimeDatabaseService();
                var existingEmail = await dbCheck.GetEmailByUsernameAsync(username);
                if (!string.IsNullOrEmpty(existingEmail))
                {
                    MessageBox.Show("Username already exists! Please choose another one.",
                                    "Duplicate Username", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Đăng ký Firebase Authentication
                var firebase = new FirebaseService();
                var auth = await firebase.RegisterAsync(email, password);
                var user = auth.User;
                string token = await user.GetIdTokenAsync();

                // Gửi email xác minh sau khi tạo tài khoản
                string apiKey = "AIzaSyB2hBtJx5MgJ8R4dlImA06yCjIf3l1zilE";
                using (var client = new HttpClient())
                {
                    var body = new
                    {
                        requestType = "VERIFY_EMAIL",
                        idToken = token
                    };

                    string json = JsonSerializer.Serialize(body);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    await client.PostAsync(
                        $"https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key={apiKey}",
                        content);
                }

                // Lưu thông tin người dùng mới vào Realtime Database
                var db = new RealtimeDatabaseService(token);
                string uid = user.Uid;
                await db.SetUserAsync(uid, new UserModel
                {
                    username = username,
                    email = email
                });
                await db.UpdateUsernameMappingAsync(username, email);

                // Thông báo gửi email xác minh
                MessageBox.Show("📩 Verification email has been sent! Please check your inbox and confirm.",
                                "Email Verification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Quay lại màn hình chính
                this.Close();
                trangChuForm.Show();
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi phổ biến của Firebase
                if (ex.Message.Contains("EMAIL_EXISTS"))
                    MessageBox.Show("This email is already in use! Please choose another one.",
                                    "Duplicate Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (ex.Message.Contains("INVALID_EMAIL"))
                    MessageBox.Show("Invalid email! Please check and try again.",
                                    "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (ex.Message.Contains("WEAK_PASSWORD"))
                    MessageBox.Show("Weak password! Please choose a stronger one (at least 6 characters).",
                                    "Weak Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Registration error: " + ex.Message,
                                    "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            // Khi nhấn Back → đóng form đăng ký và quay lại trang chủ
            this.Close();
            trangChuForm.Show();
        }
    }
}
