namespace Co_Vay
{
    partial class Trang_Chu
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
            btn_DangNhap1 = new Button();
            btn_DangKy1 = new Button();
            SuspendLayout();
            // 
            // btn_DangNhap1
            // 
            btn_DangNhap1.BackColor = Color.FromArgb(255, 218, 218);
            btn_DangNhap1.FlatAppearance.BorderSize = 0;
            btn_DangNhap1.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 218, 218);
            btn_DangNhap1.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 218, 218);
            btn_DangNhap1.FlatStyle = FlatStyle.Flat;
            btn_DangNhap1.Font = new Font("Arial", 65F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_DangNhap1.ForeColor = Color.FromArgb(255, 115, 115);
            btn_DangNhap1.Location = new Point(379, 311);
            btn_DangNhap1.Name = "btn_DangNhap1";
            btn_DangNhap1.Size = new Size(680, 151);
            btn_DangNhap1.TabIndex = 1;
            btn_DangNhap1.Text = "SIGN IN";
            btn_DangNhap1.UseVisualStyleBackColor = false;
            btn_DangNhap1.Click += btn_DangNhap1_Click;
            // 
            // btn_DangKy1
            // 
            btn_DangKy1.BackColor = Color.FromArgb(255, 211, 144);
            btn_DangKy1.FlatAppearance.BorderSize = 0;
            btn_DangKy1.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 211, 144);
            btn_DangKy1.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 211, 144);
            btn_DangKy1.FlatStyle = FlatStyle.Flat;
            btn_DangKy1.Font = new Font("Arial", 65F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_DangKy1.ForeColor = Color.FromArgb(255, 116, 65);
            btn_DangKy1.Location = new Point(379, 561);
            btn_DangKy1.Name = "btn_DangKy1";
            btn_DangKy1.Size = new Size(680, 150);
            btn_DangKy1.TabIndex = 2;
            btn_DangKy1.Text = "SIGN UP";
            btn_DangKy1.UseVisualStyleBackColor = false;
            btn_DangKy1.Click += btn_DangKy1_Click;
            // 
            // Trang_Chu
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BackgroundImage = Properties.Resources.Trang_Chu;
            ClientSize = new Size(1438, 1024);
            Controls.Add(btn_DangKy1);
            Controls.Add(btn_DangNhap1);
            Name = "Trang_Chu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Trang Chủ";
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Button btn_DangNhap1;
        private System.Windows.Forms.Button btn_DangKy1;
    }
}
