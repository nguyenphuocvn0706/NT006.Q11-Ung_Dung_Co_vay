namespace Co_Vay
{
    partial class PasswordPrompt
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblMessage;
        private TextBox txtPassword;
        private Button btnOK;
        private Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblMessage = new Label();
            txtPassword = new TextBox();
            btnOK = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // lblMessage
            // 
            lblMessage.Font = new Font("Segoe UI", 10F);
            lblMessage.Location = new Point(15, 11);
            lblMessage.Margin = new Padding(4, 0, 4, 0);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(512, 50);
            lblMessage.TabIndex = 0;
            lblMessage.Text = "Enter password:";
            
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Arial Narrow", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPassword.Location = new Point(19, 65);
            txtPassword.Margin = new Padding(4);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(508, 44);
            txtPassword.TabIndex = 1;
            // 
            // btnOK
            // 
            btnOK.Font = new Font("Segoe UI", 10F);
            btnOK.Location = new Point(312, 125);
            btnOK.Margin = new Padding(4);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(100, 44);
            btnOK.TabIndex = 2;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Segoe UI", 10F);
            btnCancel.Location = new Point(425, 125);
            btnCancel.Margin = new Padding(4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 44);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // PasswordPrompt
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(546, 188);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(txtPassword);
            Controls.Add(lblMessage);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PasswordPrompt";
            StartPosition = FormStartPosition.CenterParent;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
