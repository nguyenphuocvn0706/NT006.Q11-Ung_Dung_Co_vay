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
    public partial class Change_Email : Form
    {
        private readonly Profile profileForm;
        private readonly string apiKey = "AIzaSyB2hBtJx5MgJ8R4dlImA06yCjIf3l1zilE"; // 🔑 Firebase API Key

        public Change_Email(Profile profileForm)
        {
            InitializeComponent();
            this.profileForm = profileForm;

            btn_BackMail.Click += btn_BackMail_Click;
            btn_XacNhan2.Click += btn_XacNhan2_Click;
        }

        private void btn_BackMail_Click(object sender, EventArgs e)
        {
            this.Close();
            profileForm.Show();
        }

        private async void btn_XacNhan2_Click(object sender, EventArgs e)
        {
            string newEmail = txt_nEmail.Text.Trim();

            // 🔹 Input validation
            if (string.IsNullOrEmpty(newEmail))
            {
                MessageBox.Show("Please enter your new email address.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(newEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Invalid email format! Please enter a valid email address (e.g., example@gmail.com).",
                                "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var user = profileForm.AuthClient?.User;
                if (user == null)
                {
                    MessageBox.Show("User is not logged in!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string currentEmail = user.Info.Email;

                // 🔹 Prevent changing to the same email
                if (string.Equals(currentEmail, newEmail, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("The new email cannot be the same as the current one. Please enter a different email.",
                                    "Duplicate Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 🔹 Confirm before password re-authentication
                DialogResult confirm = MessageBox.Show(
                    $"⚠️ You are about to change your account email.\n\n" +
                    $"Current Email: {currentEmail}\nNew Email: {newEmail}\n\n" +
                    "After changing, you must verify your new email before you can log in again.\n\n" +
                    "👉 Are you sure you want to continue?",
                    "Confirm Email Change",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Cancel)
                {
                    MessageBox.Show("Email change has been canceled.", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 🔹 Re-enter password for verification
                string password = Microsoft.VisualBasic.Interaction.InputBox(
                    "🔐 Please re-enter your current password for verification:",
                    "User Authentication", "");

                if (string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Authentication canceled. Your email will not be changed.",
                                    "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 🔹 Re-login to refresh token
                var firebase = new FirebaseService();
                var refreshedUser = await firebase.LoginAsync(currentEmail, password);
                string freshToken = await refreshedUser.User.GetIdTokenAsync();

                // 🔹 Send verification email to new address
                using (var client = new HttpClient())
                {
                    var body = new
                    {
                        requestType = "VERIFY_AND_CHANGE_EMAIL",
                        idToken = freshToken,
                        newEmail = newEmail
                    };

                    string json = JsonSerializer.Serialize(body);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(
                        $"https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key={apiKey}",
                        content);

                    string responseText = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Error sending verification email: " + responseText,
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MessageBox.Show($"📩 A verification email has been sent to: {newEmail}\n" +
                                    $"Please check your inbox and confirm to complete the email change.",
                                    "Verification Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                    profileForm.Show();
                }

                // 🔹 Update user info in Realtime Database
                var db = new RealtimeDatabaseService(refreshedUser.User.Credential.IdToken);
                string uid = refreshedUser.User.Uid;
                var info = await db.GetUserAsync(uid);
                if (info != null)
                {
                    info.email = newEmail;
                    await db.SetUserAsync(uid, info);

                    if (!string.IsNullOrEmpty(info.username))
                        await db.UpdateUsernameMappingAsync(info.username, newEmail);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while changing email: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
