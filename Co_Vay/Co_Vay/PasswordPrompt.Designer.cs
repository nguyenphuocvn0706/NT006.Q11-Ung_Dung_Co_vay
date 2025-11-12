namespace Co_Vay
{
    partial class PasswordPrompt
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtPassword;
        private Button btnOK;
        private Button btnCancel;
        private Label lblPrompt;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtPassword = new TextBox();
            btnOK = new Button();
            btnCancel = new Button();
            lblPrompt = new Label();
            SuspendLayout();
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(20, 60);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(250, 31);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(50, 110);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 23);
            btnOK.TabIndex = 2;
            btnOK.Text = "OK";
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(150, 110);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // lblPrompt
            // 
            lblPrompt.AutoSize = true;
            lblPrompt.Location = new Point(20, 20);
            lblPrompt.Name = "lblPrompt";
            lblPrompt.Size = new Size(206, 25);
            lblPrompt.TabIndex = 0;
            lblPrompt.Text = "🔐 Enter Your Password:";
            // 
            // PasswordPrompt
            // 
            ClientSize = new Size(300, 170);
            Controls.Add(lblPrompt);
            Controls.Add(txtPassword);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            Name = "PasswordPrompt";
            Text = "Xác thực người dùng";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
