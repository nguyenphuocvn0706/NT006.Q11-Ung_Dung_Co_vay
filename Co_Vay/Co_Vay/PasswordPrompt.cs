using System;
using System.Windows.Forms;

namespace Co_Vay
{
    public partial class PasswordPrompt : Form
    {
        public string EnteredPassword => txtPassword.Text;

        public PasswordPrompt()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
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
