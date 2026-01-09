using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Co_Vay
{
    public partial class Quen_Mat_Khau : Form
    {
        // Dịch vụ Firebase dùng để gửi email reset password
        private readonly FirebaseService firebaseService = new FirebaseService();

        // Giữ lại form đăng nhập để quay về
        private Dang_Nhap DangNhapForm;

        public Quen_Mat_Khau(Dang_Nhap formDangNhap)
        {
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            DangNhapForm = formDangNhap;
        }

        // Khi nhấn nút "Xác nhận" → gửi email đặt lại mật khẩu
        private async void btn_XacNhan_Click(object sender, EventArgs e)
        {
            string email = txt_Email.Text.Trim(); // Lấy email người dùng nhập

            // Kiểm tra người dùng có nhập hay không
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please enter your email address!",
                                "Missing Information",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra định dạng email hợp lệ bằng Regex
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Invalid email! Please enter a valid format (e.g., example@gmail.com)",
                                "Invalid Format",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Gửi email đặt lại mật khẩu qua Firebase
                await firebaseService.ResetPasswordAsync(email);

                MessageBox.Show($"✅ A password reset email has been sent to: {email}.",
                                "Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Bắt lỗi khi gửi email thất bại
                MessageBox.Show("Error while sending password reset email: " + ex.Message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        // Hàm kiểm tra định dạng email hợp lệ bằng biểu thức chính quy
        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$"; // Pattern kiểm tra email
            return Regex.IsMatch(email, pattern);
        }

        // Khi nhấn nút Back → đóng form quên mật khẩu và quay lại form đăng nhập
        private void btn_Back_Click(object sender, EventArgs e)
        {
            this.Close();
            DangNhapForm.Show();
        }        
    }
}
