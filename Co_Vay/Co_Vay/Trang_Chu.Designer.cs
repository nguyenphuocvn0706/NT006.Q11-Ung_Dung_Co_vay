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
            pictureBox1 = new PictureBox();
            btn_DangNhap1 = new Button();
            btn_DangKy1 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = Properties.Resources.Nen;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1200, 739);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // btn_DangNhap1
            // 
            btn_DangNhap1.BackColor = Color.White;
            btn_DangNhap1.Font = new Font("Comic Sans MS", 20F, FontStyle.Bold);
            btn_DangNhap1.Location = new Point(390, 200);
            btn_DangNhap1.Name = "btn_DangNhap1";
            btn_DangNhap1.Size = new Size(420, 120);
            btn_DangNhap1.TabIndex = 1;
            btn_DangNhap1.Text = "SIGN IN";
            btn_DangNhap1.UseVisualStyleBackColor = false;
            btn_DangNhap1.Click += btn_DangNhap1_Click;
            // 
            // btn_DangKy1
            // 
            btn_DangKy1.BackColor = Color.White;
            btn_DangKy1.Font = new Font("Comic Sans MS", 20F, FontStyle.Bold);
            btn_DangKy1.Location = new Point(390, 400);
            btn_DangKy1.Name = "btn_DangKy1";
            btn_DangKy1.Size = new Size(420, 120);
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
            ClientSize = new Size(1200, 739);
            Controls.Add(btn_DangKy1);
            Controls.Add(btn_DangNhap1);
            Controls.Add(pictureBox1);
            Name = "Trang_Chu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Trang Chủ";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_DangNhap1;
        private System.Windows.Forms.Button btn_DangKy1;
    }
}
