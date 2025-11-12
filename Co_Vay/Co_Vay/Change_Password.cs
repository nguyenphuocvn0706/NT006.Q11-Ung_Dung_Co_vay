using Firebase.Auth;
using System;
using System.Windows.Forms;

namespace Co_Vay
{
    public partial class Change_Password : Form
    {
        // 🔹 Lưu form Profile để có thể quay lại sau khi đổi mật khẩu
        private Profile profileForm;

        public Change_Password(Profile profile)
        {
            InitializeComponent();
            this.profileForm = profile;
        }

        private void Change_Password_Load(object sender, EventArgs e)
        {

        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            // 🔹 Khi bấm nút Back → đóng form hiện tại và quay về trang Profile
            this.Close();
            profileForm.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private async void btn_XacNhan_Click(object sender, EventArgs e)
        {
            // 🔹 Lấy dữ liệu người dùng nhập
            string oldPass = txb_Password1.Text.Trim();   // Mật khẩu cũ
            string newPass = txb_Password2.Text.Trim();   // Mật khẩu mới
            string reNewPass = textBox1.Text.Trim();      // Nhập lại mật khẩu mới

            // 🔹 Kiểm tra nhập thiếu
            if (string.IsNullOrEmpty(oldPass) || string.IsNullOrEmpty(newPass) || string.IsNullOrEmpty(reNewPass))
            {
                MessageBox.Show("Please fill in all required fields!", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 🔹 Kiểm tra độ dài mật khẩu mới (Firebase yêu cầu ít nhất 6 ký tự)
            if (newPass.Length < 6)
            {
                MessageBox.Show("New password must be at least 6 characters long!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 🔹 Kiểm tra 2 ô nhập mật khẩu mới có trùng nhau không
            if (newPass != reNewPass)
            {
                MessageBox.Show("New passwords do not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // 🔹 Lấy thông tin user hiện tại từ FirebaseAuthClient
                var user = profileForm.AuthClient?.User;
                if (user == null)
                {
                    MessageBox.Show("No logged-in user found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 🔹 Xác thực lại người dùng bằng mật khẩu cũ (Firebase yêu cầu re-auth trước khi đổi thông tin nhạy cảm)
                var email = user.Info.Email;
                var firebase = new FirebaseService();
                await firebase.LoginAsync(email, oldPass); // Nếu mật khẩu sai → sẽ ném lỗi và nhảy xuống catch

                // 🔹 Thực hiện đổi mật khẩu trên Firebase
                await user.ChangePasswordAsync(newPass);

                // 🔹 Hiển thị thông báo thành công
                MessageBox.Show("✅ Password changed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 🔹 Sau khi đổi mật khẩu → đóng form và quay lại Profile
                this.Close();
                profileForm.Show();
            }
            catch (FirebaseAuthException)
            {
                // 🔹 Nếu xác thực thất bại (mật khẩu cũ sai)
                MessageBox.Show("Incorrect old password!", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // 🔹 Bắt các lỗi khác (lỗi mạng, lỗi Firebase, v.v.)
                MessageBox.Show("An error occurred while changing password: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
