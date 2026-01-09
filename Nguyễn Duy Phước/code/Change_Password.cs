using Firebase.Auth;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Co_Vay
{
    public partial class Change_Password : Form
    {
        private readonly Profile profileForm;
        private readonly FirebaseAuthClient authClient;

        // CONSTRUCTOR 
        public Change_Password(Profile profileForm, FirebaseAuthClient authClient)
        {
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.profileForm = profileForm;
            this.authClient = authClient;

            txb_Password1.UseSystemPasswordChar = true;
            txb_Password2.UseSystemPasswordChar = true;
            textBox1.UseSystemPasswordChar = true;
        }

        private void Change_Password_Load(object sender, EventArgs e)
        {
            this.ClientSize = new Size(1440, 1024);
        }

        // ================= BACK =================
        private void btn_Back_Click(object sender, EventArgs e)
        {
            profileForm.Show();
            this.Close();
        }

        // ================= CONFIRM =================
        private async void btn_XacNhan_Click(object sender, EventArgs e)
        {
            string oldPass = txb_Password1.Text.Trim();
            string newPass = txb_Password2.Text.Trim();
            string reNewPass = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(oldPass) ||
                string.IsNullOrEmpty(newPass) ||
                string.IsNullOrEmpty(reNewPass))
            {
                MessageBox.Show(
                    "Please fill in all required fields!",
                    "Missing Data",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (oldPass == newPass)
            {
                MessageBox.Show(
                    "New password cannot be the same as the old password!",
                    "Invalid Password",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (newPass.Length < 6)
            {
                MessageBox.Show(
                    "New password must be at least 6 characters long!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (newPass != reNewPass)
            {
                MessageBox.Show(
                    "New passwords do not match!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            try
            {
                var user = authClient?.User;
                if (user == null)
                {
                    MessageBox.Show(
                        "User not logged in!",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                // RE-AUTH
                string email = user.Info.Email;
                var firebase = new FirebaseService();
                await firebase.LoginAsync(email, oldPass);

                // CHANGE PASSWORD
                await user.ChangePasswordAsync(newPass);

                MessageBox.Show(
                    "Password changed successfully.\nYou will be logged out.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // ================= LOG OUT =================
                authClient.SignOut();

                // ================= GO TO HOME =================
                Trang_Chu home = new Trang_Chu();
                home.StartPosition = FormStartPosition.CenterScreen;
                home.Show();

                // ================= CLOSE FORMS =================
                profileForm.Close();
                this.Close();
            }
            catch (FirebaseAuthException)
            {
                MessageBox.Show(
                    "Incorrect old password!",
                    "Authentication Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error changing password: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
