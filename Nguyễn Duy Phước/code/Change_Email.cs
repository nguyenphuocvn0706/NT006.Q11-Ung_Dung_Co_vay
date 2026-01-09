using Firebase.Auth;
using System;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Co_Vay
{
    public partial class Change_Email : Form
    {
        private readonly Profile profileForm;
        private readonly FirebaseAuthClient authClient;

        // Firebase Web API Key
        private readonly string apiKey = "AIzaSyB2hBtJx5MgJ8R4dlImA06yCjIf3l1zilE";

        public Change_Email(Profile profileForm, FirebaseAuthClient authClient)
        {
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.profileForm = profileForm;
            this.authClient = authClient;
        }

        private void Change_Email_Load(object sender, EventArgs e)
        {
            this.ClientSize = new Size(1440, 1024);
        }

        // ================= BACK =================
        private void btn_BackMail_Click(object sender, EventArgs e)
        {
            profileForm.Show();
            this.Close();
        }

        // ================= CONFIRM =================
        private async void btn_XacNhan2_Click(object sender, EventArgs e)
        {
            string newEmail = txt_nEmail.Text.Trim();

            // VALIDATION 
            if (string.IsNullOrEmpty(newEmail))
            {
                MessageBox.Show("Please enter your new email address.");
                return;
            }

            if (!Regex.IsMatch(newEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Invalid email format.");
                return;
            }

            try
            {
                var user = authClient?.User;
                if (user == null)
                {
                    MessageBox.Show("User not logged in.");
                    return;
                }

                string currentEmail = user.Info.Email;

                if (string.Equals(currentEmail, newEmail, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("New email must be different from current email.");
                    return;
                }

                // RE-AUTH 
                string password;
                using (var prompt = new PasswordPrompt(
                    "Please re-enter your password:",
                    "Verification"))
                {
                    if (prompt.ShowDialog(this) != DialogResult.OK)
                        return;

                    password = prompt.Password;
                }

                var firebase = new FirebaseService();
                var refreshed = await firebase.LoginAsync(currentEmail, password);
                string freshToken = await refreshed.User.GetIdTokenAsync();

                // SEND VERIFY EMAIL 
                using (var client = new HttpClient())
                {
                    var body = new
                    {
                        requestType = "VERIFY_AND_CHANGE_EMAIL",
                        idToken = freshToken,
                        newEmail = newEmail
                    };

                    var content = new StringContent(
                        JsonSerializer.Serialize(body),
                        Encoding.UTF8,
                        "application/json");

                    var response = await client.PostAsync(
                        $"https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key={apiKey}",
                        content);

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Failed to send verification email.");
                        return;
                    }
                }

                // UPDATE REALTIME DATABASE 
                var db = new RealtimeDatabaseService(authClient.User.Credential.IdToken);

                // Lấy user info để biết username
                var userInfo = await db.GetUserAsync(authClient.User.Uid);
                if (userInfo == null || string.IsNullOrEmpty(userInfo.username))
                {
                    MessageBox.Show("Cannot find user info in database.");
                    return;
                }

                // 1. Update /users/{uid}/email
                await db.UpdateUserFieldsAsync(authClient.User.Uid, new
                {
                    email = newEmail
                });

                // 2. Update /usernames/{username}
                await db.SetAsync($"usernames/{userInfo.username}", newEmail);

                MessageBox.Show(
                    $"Verification email sent to {newEmail}.\n" +
                    $"Realtime Database updated successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                profileForm.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error changing email: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
