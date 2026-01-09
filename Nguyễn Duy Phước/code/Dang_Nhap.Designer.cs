namespace Co_Vay
{
    partial class Dang_Nhap
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
            txb_Password = new TextBox();
            btn_Quen_mat_khau = new Button();
            btn_DangNhap2 = new Button();
            SuspendLayout();
            // 
            // btn_Back
            // 
            btn_Back.BackColor = Color.White;
            btn_Back.Font = new Font("Arial", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_Back.Location = new Point(40, 40);
            btn_Back.Name = "btn_Back";
            btn_Back.Size = new Size(137, 54);
            btn_Back.TabIndex = 1;
            btn_Back.Text = "Back";
            btn_Back.UseVisualStyleBackColor = false;
            btn_Back.Click += btn_Back_Click;
            // 
            // txb_Username
            // 
            txb_Username.BorderStyle = BorderStyle.None;
            txb_Username.Font = new Font("Arial", 16F);
            txb_Username.Location = new Point(362, 346);
            txb_Username.Name = "txb_Username";
            txb_Username.Size = new Size(611, 37);
            txb_Username.TabIndex = 3;
            // 
            // txb_Password
            // 
            txb_Password.BorderStyle = BorderStyle.None;
            txb_Password.Font = new Font("Arial", 16F);
            txb_Password.Location = new Point(362, 498);
            txb_Password.Name = "txb_Password";
            txb_Password.PasswordChar = '*';
            txb_Password.Size = new Size(611, 37);
            txb_Password.TabIndex = 5;
            // 
            // btn_Quen_mat_khau
            // 
            btn_Quen_mat_khau.BackColor = Color.White;
            btn_Quen_mat_khau.FlatAppearance.BorderSize = 0;
            btn_Quen_mat_khau.FlatStyle = FlatStyle.Flat;
            btn_Quen_mat_khau.Font = new Font("Arial", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_Quen_mat_khau.Location = new Point(908, 254);
            btn_Quen_mat_khau.Name = "btn_Quen_mat_khau";
            btn_Quen_mat_khau.Size = new Size(184, 36);
            btn_Quen_mat_khau.TabIndex = 6;
            btn_Quen_mat_khau.Text = "Forget password";
            btn_Quen_mat_khau.UseVisualStyleBackColor = false;
            btn_Quen_mat_khau.Click += btn_Quen_mat_khau_Click;
            // 
            // btn_DangNhap2
            // 
            btn_DangNhap2.BackColor = Color.FromArgb(255, 218, 218);
            btn_DangNhap2.FlatAppearance.BorderSize = 0;
            btn_DangNhap2.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 218, 218);
            btn_DangNhap2.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 218, 218);
            btn_DangNhap2.FlatStyle = FlatStyle.Flat;
            btn_DangNhap2.Font = new Font("Comic Sans MS", 20F, FontStyle.Bold);
            btn_DangNhap2.ForeColor = Color.FromArgb(255, 115, 115);
            btn_DangNhap2.Location = new Point(478, 604);
            btn_DangNhap2.Name = "btn_DangNhap2";
            btn_DangNhap2.Size = new Size(483, 101);
            btn_DangNhap2.TabIndex = 7;
            btn_DangNhap2.Text = "LOGIN";
            btn_DangNhap2.UseVisualStyleBackColor = false;
            btn_DangNhap2.Click += btn_DangNhap2_Click;
            // 
            // Dang_Nhap
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Dang_Nhap;
            ClientSize = new Size(1438, 1024);
            Controls.Add(btn_DangNhap2);
            Controls.Add(btn_Quen_mat_khau);
            Controls.Add(txb_Password);
            Controls.Add(txb_Username);
            Controls.Add(btn_Back);
            Name = "Dang_Nhap";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng Nhập";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button btn_Back;
        private System.Windows.Forms.TextBox txb_Username;
        private System.Windows.Forms.TextBox txb_Password;
        private System.Windows.Forms.Button btn_Quen_mat_khau;
        private System.Windows.Forms.Button btn_DangNhap2;
    }
}
