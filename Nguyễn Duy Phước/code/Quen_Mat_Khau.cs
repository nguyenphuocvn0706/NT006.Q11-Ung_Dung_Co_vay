using System;
using System.Windows.Forms;

namespace Co_Vay
{
    public partial class Quen_Mat_Khau : Form
    {
        private readonly RealtimeDatabaseService dbService = new RealtimeDatabaseService();
        private readonly FirebaseService firebaseService = new FirebaseService();
        private Dang_Nhap DangNhapForm;
        public Quen_Mat_Khau(Dang_Nhap formDangNhap)
        {
            InitializeComponent();
            DangNhapForm = formDangNhap;
        }

        private async void btn_XacNhan_Click(object sender, EventArgs e)
        {
            string username = txt_Username.Text.Trim();
            string email = txt_Email.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập đủ Username và Email!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Lấy email từ Firebase theo username
                string storedEmail = await dbService.GetEmailByUsernameAsync(username);

                if (storedEmail == null)
                {
                    MessageBox.Show("Tên người dùng không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!storedEmail.Equals(email, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Email không khớp với Username.", "Sai thông tin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                await firebaseService.ResetPasswordAsync(email);
                MessageBox.Show($"✅ Đã gửi email đặt lại mật khẩu tới {email}.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi gửi email: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            this.Close();
            DangNhapForm.Show();
        }
    }
}
