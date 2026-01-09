using Firebase.Auth;
using System;
using System.ComponentModel;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace Co_Vay
{
    public partial class Profile : Form
    {
        private readonly Man_Hinh_Chinh mainForm;
        private readonly FirebaseAuthClient authClient;
        private bool isEditing = false;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public UserCredential CurrentUser { get; set; }
        
        public Profile(Man_Hinh_Chinh mainForm, FirebaseAuthClient authClient)
        {
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.mainForm = mainForm;
            this.authClient = authClient;

            if (authClient?.User == null)
            {
                MessageBox.Show("User not logged in!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            // Lock fields mặc định
            txt_Name.ReadOnly = true;
            txt_Sex.Enabled = false;
            txt_Date.Enabled = false;

            txt_Name.KeyDown += TextBox_KeyDown;
            txt_Sex.KeyDown += TextBox_KeyDown;
            txt_Date.KeyDown += TextBox_KeyDown;

            this.Load += Profile_Load;
        }

        // ================= LOAD =================
        private async void Profile_Load(object sender, EventArgs e)
        {
            try
            {
                var user = authClient.User;
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
                MessageBox.Show(
                    "Error loading user info: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ================= EDIT =================
        private async void txt_Name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || !isEditing)
                return;

            e.SuppressKeyPress = true;

            // ===== VALIDATE =====
            if (string.IsNullOrWhiteSpace(txt_Name.Text) ||
                string.IsNullOrWhiteSpace(txt_Sex.Text) ||
                txt_Date.Value == null)
            {
                MessageBox.Show(
                    "All fields must be filled.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                var user = authClient.User;
                var db = new RealtimeDatabaseService(user.Credential.IdToken);

                await db.UpdateUserFieldsAsync(user.Uid, new
                {
                    name = txt_Name.Text.Trim(),
                    sex = txt_Sex.Text,
                    dob = txt_Date.Value.ToString("yyyy-MM-dd")
                });

                txt_Name.ReadOnly = true;
                txt_Sex.Enabled = false;
                txt_Date.Enabled = false;
                isEditing = false;

                MessageBox.Show(
                    "Profile updated successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Save error: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private async void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || !isEditing)
                return;

            e.SuppressKeyPress = true;

            if (string.IsNullOrWhiteSpace(txt_Name.Text) ||
                string.IsNullOrWhiteSpace(txt_Sex.Text))
            {
                MessageBox.Show("Please fill in all fields!");
                return;
            }

            try
            {
                var user = authClient.User;
                string uid = user.Uid;

                var db = new RealtimeDatabaseService(user.Credential.IdToken);

                await db.UpdateUserFieldsAsync(uid, new
                {
                    name = txt_Name.Text.Trim(),
                    sex = txt_Sex.Text,
                    dob = txt_Date.Value.ToString("dd/MM/yyyy")
                });

                txt_Name.ReadOnly = true;
                txt_Sex.Enabled = false;
                txt_Date.Enabled = false;
                isEditing = false;

                MessageBox.Show("Information saved successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error saving info: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ================= BACK =================        
        private void btn_Back_Click(object sender, EventArgs e)
        {
            mainForm.Show();
            this.Close();
        }

        // ================= LOG OUT =================
        private void btn_Log_Out_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Are you sure you want to log out?",
                "Confirm Log Out",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                authClient.SignOut();

                Trang_Chu home = new Trang_Chu();
                home.Show();

                mainForm.Close();
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

        // ================= DELETE ACCOUNT =================
        private async void btn_Delete_Info_Click(object sender, EventArgs e)
        {
            try
            {
                var user = authClient.User;
                string uid = user.Uid;
                string email = user.Info.Email;

                if (MessageBox.Show(
                        "This will permanently delete your account.\nContinue?",
                        "Confirm",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;

                string password;
                using (var prompt = new PasswordPrompt(
                    "Please re-enter your password:",
                    "Verification"))
                {
                    if (prompt.ShowDialog() != DialogResult.OK)
                        return;

                    password = prompt.Password;
                }

                // Re-authenticate
                var firebaseService = new FirebaseService();
                var refreshed = await firebaseService.LoginAsync(email, password);
                string idToken = await refreshed.User.GetIdTokenAsync();

                // 1. DELETE AUTHENTICATION
                using (var client = new HttpClient())
                {
                    var body = new { idToken };
                    var json = JsonSerializer.Serialize(body);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(
                        "https://identitytoolkit.googleapis.com/v1/accounts:delete",
                        content);

                    if (!response.IsSuccessStatusCode)
                        throw new Exception("Failed to delete authentication account.");
                }

                // 2. DELETE REALTIME DATABASE DATA
                var db = new RealtimeDatabaseService(idToken);

                var info = await db.GetUserAsync(uid);
                if (info != null && !string.IsNullOrEmpty(info.username))
                {
                    await db.DeleteUsernameMappingAsync(info.username);
                }

                await db.DeleteUserAsync(uid);

                // 3. SIGN OUT & UI
                authClient.SignOut();
                new Trang_Chu().Show();

                mainForm.Close();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error deleting account: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // ================= CHANGE PASSWORD =================
        private void btn_Chang_Password_Click(object sender, EventArgs e)
        {
            Change_Password f = new Change_Password(this, authClient);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();

            this.Hide();
        }

        private void btn_Chang_Email_Click(object sender, EventArgs e)
        {
            Change_Email f = new Change_Email(this, authClient);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
            this.Hide();
        }

        private void btn_Change_Info_Click(object sender, EventArgs e)
        {
            isEditing = true;

            txt_Name.ReadOnly = false;
            txt_Sex.Enabled = true;
            txt_Date.Enabled = true;

            MessageBox.Show(
                "Edit your information.\nPress ENTER to save after finishing.\nAll fields are required.",
                "Edit Profile",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            txt_Name.Focus();
        }
    }
}
