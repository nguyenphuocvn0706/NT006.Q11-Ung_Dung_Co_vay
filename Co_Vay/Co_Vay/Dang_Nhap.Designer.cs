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
            pictureBox1 = new PictureBox();
            btn_Back = new Button();
            label1 = new Label();
            txb_Username = new TextBox();
            label2 = new Label();
            txb_Password = new TextBox();
            btn_Quen_mat_khau = new Button();
            btn_DangNhap2 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Nen;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1200, 739);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // btn_Back
            // 
            btn_Back.BackColor = Color.White;
            btn_Back.Font = new Font("Comic Sans MS", 16F, FontStyle.Bold);
            btn_Back.Location = new Point(40, 40);
            btn_Back.Name = "btn_Back";
            btn_Back.Size = new Size(100, 60);
            btn_Back.TabIndex = 1;
            btn_Back.Text = "Back";
            btn_Back.UseVisualStyleBackColor = false;
            btn_Back.Click += btn_Back_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Comic Sans MS", 14F, FontStyle.Bold);
            label1.Location = new Point(350, 180);
            label1.Name = "label1";
            label1.Size = new Size(236, 39);
            label1.TabIndex = 2;
            label1.Text = "Enter Username";
            // 
            // txb_Username
            // 
            txb_Username.Font = new Font("Comic Sans MS", 12F);
            txb_Username.Location = new Point(350, 225);
            txb_Username.Name = "txb_Username";
            txb_Username.Size = new Size(500, 41);
            txb_Username.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Comic Sans MS", 14F, FontStyle.Bold);
            label2.Location = new Point(350, 310);
            label2.Name = "label2";
            label2.Size = new Size(225, 39);
            label2.TabIndex = 4;
            label2.Text = "Enter Password";
            // 
            // txb_Password
            // 
            txb_Password.Font = new Font("Comic Sans MS", 12F);
            txb_Password.Location = new Point(350, 355);
            txb_Password.Name = "txb_Password";
            txb_Password.PasswordChar = '*';
            txb_Password.Size = new Size(500, 41);
            txb_Password.TabIndex = 5;
            // 
            // btn_Quen_mat_khau
            // 
            btn_Quen_mat_khau.BackColor = Color.White;
            btn_Quen_mat_khau.Font = new Font("Comic Sans MS", 10F);
            btn_Quen_mat_khau.Location = new Point(350, 420);
            btn_Quen_mat_khau.Name = "btn_Quen_mat_khau";
            btn_Quen_mat_khau.Size = new Size(194, 40);
            btn_Quen_mat_khau.TabIndex = 6;
            btn_Quen_mat_khau.Text = "Forget password";
            btn_Quen_mat_khau.UseVisualStyleBackColor = false;
            btn_Quen_mat_khau.Click += btn_Quen_mat_khau_Click;
            // 
            // btn_DangNhap2
            // 
            btn_DangNhap2.BackColor = Color.White;
            btn_DangNhap2.Font = new Font("Comic Sans MS", 20F, FontStyle.Bold);
            btn_DangNhap2.Location = new Point(470, 500);
            btn_DangNhap2.Name = "btn_DangNhap2";
            btn_DangNhap2.Size = new Size(260, 100);
            btn_DangNhap2.TabIndex = 7;
            btn_DangNhap2.Text = "LOGIN";
            btn_DangNhap2.UseVisualStyleBackColor = false;
            btn_DangNhap2.Click += btn_DangNhap2_Click;
            // 
            // Dang_Nhap
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 739);
            Controls.Add(btn_DangNhap2);
            Controls.Add(btn_Quen_mat_khau);
            Controls.Add(txb_Password);
            Controls.Add(label2);
            Controls.Add(txb_Username);
            Controls.Add(label1);
            Controls.Add(btn_Back);
            Controls.Add(pictureBox1);
            Name = "Dang_Nhap";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng Nhập";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_Back;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txb_Username;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txb_Password;
        private System.Windows.Forms.Button btn_Quen_mat_khau;
        private System.Windows.Forms.Button btn_DangNhap2;
    }
}
