using System;
using System.Windows.Forms;

namespace Co_Vay
{
    public partial class PasswordPrompt : Form
    {
        public string Password => txtPassword.Text;

        public PasswordPrompt(string message, string title)
        {
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.Text = title;
            lblMessage.Text = message;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter your password!", "Missing Input",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }       
    }
}
