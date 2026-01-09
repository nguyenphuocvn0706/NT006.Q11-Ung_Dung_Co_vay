namespace Co_Vay
{
    partial class Dang_Ky
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            btn_Back = new Button();
            txb_Username = new TextBox();
            txt_Email = new TextBox();
            txb_Password1 = new TextBox();
            label4 = new Label();
            txb_Password2 = new TextBox();
            btn_DangKy = new Button();
            SuspendLayout();
            // 
            // btn_Back
            // 
            btn_Back.BackColor = Color.White;
            btn_Back.Font = new Font("Arial", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_Back.Location = new Point(40, 40);
            btn_Back.Name = "btn_Back";
            btn_Back.Size = new Size(126, 54);
            btn_Back.TabIndex = 1;
            btn_Back.Text = "Back";
            btn_Back.UseVisualStyleBackColor = false;
            btn_Back.Click += btn_Back_Click;
            // 
            // txb_Username
            // 
            txb_Username.BorderStyle = BorderStyle.None;
            txb_Username.Font = new Font("Arial", 16F);
            txb_Username.Location = new Point(355, 293);
            txb_Username.Multiline = true;
            txb_Username.Name = "txb_Username";
            txb_Username.Size = new Size(619, 47);
            txb_Username.TabIndex = 3;
            
            // 
            // txt_Email
            // 
            txt_Email.BorderStyle = BorderStyle.None;
            txt_Email.Font = new Font("Arial", 16F);
            txt_Email.Location = new Point(355, 434);
            txt_Email.Multiline = true;
            txt_Email.Name = "txt_Email";
            txt_Email.Size = new Size(619, 48);
            txt_Email.TabIndex = 5;
            // 
            // txb_Password1
            // 
            txb_Password1.BorderStyle = BorderStyle.None;
            txb_Password1.Font = new Font("Arial", 16F);
            txb_Password1.Location = new Point(355, 574);
            txb_Password1.Multiline = true;
            txb_Password1.Name = "txb_Password1";
            txb_Password1.PasswordChar = '*';
            txb_Password1.Size = new Size(619, 60);
            txb_Password1.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Comic Sans MS", 14F, FontStyle.Bold);
            label4.Location = new Point(1076, 521);
            label4.Name = "label4";
            label4.Size = new Size(0, 39);
            label4.TabIndex = 8;
            
            // 
            // txb_Password2
            // 
            txb_Password2.BorderStyle = BorderStyle.None;
            txb_Password2.Font = new Font("Arial", 16F);
            txb_Password2.Location = new Point(355, 718);
            txb_Password2.Multiline = true;
            txb_Password2.Name = "txb_Password2";
            txb_Password2.PasswordChar = '*';
            txb_Password2.Size = new Size(619, 62);
            txb_Password2.TabIndex = 9;
            // 
            // btn_DangKy
            // 
            btn_DangKy.BackColor = Color.FromArgb(255, 211, 144);
            btn_DangKy.FlatAppearance.BorderSize = 0;
            btn_DangKy.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 211, 144);
            btn_DangKy.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 211, 144);
            btn_DangKy.FlatStyle = FlatStyle.Flat;
            btn_DangKy.Font = new Font("Arial", 45F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_DangKy.ForeColor = Color.FromArgb(255, 116, 65);
            btn_DangKy.Location = new Point(478, 835);
            btn_DangKy.Name = "btn_DangKy";
            btn_DangKy.Size = new Size(480, 104);
            btn_DangKy.TabIndex = 10;
            btn_DangKy.Text = "SIGN UP";
            btn_DangKy.UseVisualStyleBackColor = false;
            btn_DangKy.Click += btn_DangKy_Click;
            // 
            // Dang_Ky
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Sign_Up;
            ClientSize = new Size(1438, 1024);
            Controls.Add(btn_DangKy);
            Controls.Add(txb_Password2);
            Controls.Add(label4);
            Controls.Add(txb_Password1);
            Controls.Add(txt_Email);
            Controls.Add(txb_Username);
            Controls.Add(btn_Back);
            Name = "Dang_Ky";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sign Up";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button btn_Back;
        private System.Windows.Forms.TextBox txb_Username;
        private System.Windows.Forms.TextBox txt_Email;
        private System.Windows.Forms.TextBox txb_Password1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txb_Password2;
        private System.Windows.Forms.Button btn_DangKy;
    }
}
