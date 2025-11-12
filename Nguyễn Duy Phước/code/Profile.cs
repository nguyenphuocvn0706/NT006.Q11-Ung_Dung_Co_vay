using Firebase.Auth;
using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Text.Json;

namespace Co_Vay
{
    public partial class Profile : Form
    {
        private Man_Hinh_Chinh mainForm;
        private bool isEditing = false;

        // 🔹 Đối tượng FirebaseAuthClient (dùng để quản lý đăng nhập/đăng xuất)
        private FirebaseAuthClient authClient;
        public FirebaseAuthClient AuthClient => authClient;

        // 🔹 Lưu thông tin người dùng hiện tại để thực hiện các thao tác như đổi email, cập nhật token
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public UserCredential CurrentUser { get; set; }

        public Profile(Man_Hinh_Chinh mainForm, FirebaseAuthClient authClient = null, UserCredential currentUser = null)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.authClient = authClient;
            this.CurrentUser = currentUser;

            // 🔹 Mặc định các ô bị khóa, chỉ bật khi nhấn “Change Info”
            txt_Name.ReadOnly = true;
            txt_Sex.Enabled = false;
            txt_Date.Enabled = false;

            txt_Name.KeyDown += TextBox_KeyDown;
            txt_Sex.KeyDown += TextBox_KeyDown;
            txt_Date.KeyDown += TextBox_KeyDown;

            this.Load += Profile_Load;
        }

        // 🔹 Khi form load → tải thông tin người dùng từ Realtime Database
        private async void Profile_Load(object sender, EventArgs e)
        {
            try
            {
                var user = authClient?.User;
                if (user == null)
                {
                    MessageBox.Show("User not logged in!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string uid = user.Uid;
                var db = new RealtimeDatabaseService(user.Credential.IdToken);
                var res = await db.GetUserAsync(uid);

                if (res != null)
                {
                    txt_Name.Text = res.name;
                    txt_Sex.SelectedItem = res.sex;
                    if (DateTime.TryParse(res.dob, out var dob))
                        txt_Date.Value = dob;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user info: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 🔹 Khi nhấn nút Change Info → bật chế độ chỉnh sửa
        private void btn_Change_Info_Click(object sender, EventArgs e)
        {
            isEditing = true;
            txt_Name.ReadOnly = false;
            txt_Sex.Enabled = true;
            txt_Date.Enabled = true;

            MessageBox.Show("You can now edit your info. Press Enter to save.", "Edit Mode", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // 🔹 Khi nhấn Enter → lưu thông tin mới vào Firebase
        private async void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && isEditing)
            {
                e.SuppressKeyPress = true;

                if (string.IsNullOrWhiteSpace(txt_Name.Text) || string.IsNullOrWhiteSpace(txt_Sex.Text))
                {
                    MessageBox.Show("Please fill in all fields!", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    var user = authClient?.User;
                    if (user == null)
                    {
                        MessageBox.Show("User not logged in!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string uid = user.Uid;

                    var info = new UserModel
                    {
                        username = user.Info.DisplayName ?? user.Info.Email.Split('@')[0],
                        email = user.Info.Email,
                        name = txt_Name.Text.Trim(),
                        sex = txt_Sex.Text,
                        dob = txt_Date.Value.ToString("dd/MM/yyyy")
                    };

                    var db = new RealtimeDatabaseService(user.Credential.IdToken);
                    await db.SetUserAsync(uid, info);

                    MessageBox.Show("Information saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txt_Name.ReadOnly = true;
                    txt_Sex.Enabled = false;
                    txt_Date.Enabled = false;
                    isEditing = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving info to Firebase: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // 🔹 Nút Log Out → đăng xuất và quay lại trang chủ
        private void btn_Log_Out_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to log out?", "Confirm Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    authClient?.SignOut();
                    Trang_Chu home = new Trang_Chu();
                    home.Show();

                    this.Close();
                    mainForm?.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error during logout: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // 🔹 Quay lại màn hình chính
        private void btn_Back_Click(object sender, EventArgs e)
        {
            this.Close();
            mainForm.Show();
        }

        // 🔹 Nút Delete Info → Xóa toàn bộ tài khoản và dữ liệu khỏi Firebase
        private async void btn_Delete_Info_Click(object sender, EventArgs e)
        {
            try
            {
                var user = authClient?.User;
                if (user == null)
                {
                    MessageBox.Show("User not logged in!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 🔹 Hỏi xác nhận trước khi xóa
                DialogResult confirm = MessageBox.Show(
                    "⚠️ Are you sure you want to permanently delete your account?\n\n" +
                    "This action will permanently remove your data from the system, including:\n" +
                    "- Authentication data\n" +
                    "- Personal information\n\n" +
                    "❌ This action cannot be undone.\n\n" +
                    "Do you want to continue?",
                    "Confirm Account Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.No)
                {
                    MessageBox.Show("Account deletion canceled.", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 🔹 Yêu cầu người dùng nhập lại mật khẩu để xác thực
                string password = Microsoft.VisualBasic.Interaction.InputBox(
                    "🔐 Please re-enter your password to confirm account deletion:",
                    "User Verification", "");

                if (string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Account deletion has been canceled. Your data remains safe.",
                                    "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string currentEmail = user.Info.Email;

                // 🔹 Đăng nhập lại để lấy token mới (bắt buộc trong Firebase)
                var firebaseService = new FirebaseService();
                var refreshedUser = await firebaseService.LoginAsync(currentEmail, password);
                string idToken = await refreshedUser.User.GetIdTokenAsync();

                // 🔹 Xóa dữ liệu trong Realtime Database
                var db = new RealtimeDatabaseService(idToken);
                string uid = refreshedUser.User.Uid;

                // Lấy thông tin người dùng từ DB trước khi xóa
                var userInfo = await db.GetUserAsync(uid);
                if (userInfo != null && !string.IsNullOrEmpty(userInfo.username))
                {
                    await db.DeleteUsernameMappingAsync(userInfo.username);
                }

                // Sau đó xóa người dùng trong node /users
                await db.DeleteUserAsync(uid);


                // 🔹 Gọi Firebase REST API để xóa tài khoản Authentication
                using (var client = new HttpClient())
                {
                    var body = new { idToken = idToken };
                    string json = JsonSerializer.Serialize(body);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(
                        $"https://identitytoolkit.googleapis.com/v1/accounts:delete?key=AIzaSyB2hBtJx5MgJ8R4dlImA06yCjIf3l1zilE",
                        content);

                    if (!response.IsSuccessStatusCode)
                    {
                        string error = await response.Content.ReadAsStringAsync();
                        MessageBox.Show("Error deleting Firebase account: " + error,
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // 🔹 Thông báo thành công
                MessageBox.Show("✅ Your account has been permanently deleted from the system.",
                                "Account Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 🔹 Đăng xuất và trở về trang chủ
                authClient.SignOut();
                Trang_Chu home = new Trang_Chu();
                home.Show();
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting account: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // 🔹 Dành cho chức năng hiển thị ảnh đại diện (nếu bạn thêm sau)
        }

        private void btn_Chang_Password_Click(object sender, EventArgs e)
        {
            // 🔹 Mở form đổi mật khẩu
            Change_Password f = new Change_Password(this);
            f.Show();
            this.Hide();
        }

        private void btn_Chang_Email_Click(object sender, EventArgs e)
        {
            // 🔹 Mở form đổi email
            Change_Email f = new Change_Email(this);
            f.Show();
            this.Hide();
        }

        private void txt_Sex_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
