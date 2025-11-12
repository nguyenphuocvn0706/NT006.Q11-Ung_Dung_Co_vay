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
            pictureBox1 = new PictureBox();
            btn_Back = new Button();
            label1 = new Label();
            txb_Username = new TextBox();
            label2 = new Label();
            txt_Email = new TextBox();
            label3 = new Label();
            txb_Password1 = new TextBox();
            label4 = new Label();
            txb_Password2 = new TextBox();
            btn_DangKy = new Button();
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
            label1.Location = new Point(350, 54);
            label1.Name = "label1";
            label1.Size = new Size(236, 39);
            label1.TabIndex = 2;
            label1.Text = "Enter Username";
            // 
            // txb_Username
            // 
            txb_Username.Font = new Font("Comic Sans MS", 12F);
            txb_Username.Location = new Point(350, 104);
            txb_Username.Name = "txb_Username";
            txb_Username.Size = new Size(500, 41);
            txb_Username.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Comic Sans MS", 14F, FontStyle.Bold);
            label2.Location = new Point(350, 176);
            label2.Name = "label2";
            label2.Size = new Size(174, 39);
            label2.TabIndex = 4;
            label2.Text = "Enter Email";
            // 
            // txt_Email
            // 
            txt_Email.Font = new Font("Comic Sans MS", 12F);
            txt_Email.Location = new Point(350, 227);
            txt_Email.Name = "txt_Email";
            txt_Email.Size = new Size(500, 41);
            txt_Email.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Comic Sans MS", 14F, FontStyle.Bold);
            label3.Location = new Point(350, 312);
            label3.Name = "label3";
            label3.Size = new Size(225, 39);
            label3.TabIndex = 6;
            label3.Text = "Enter Password";
            // 
            // txb_Password1
            // 
            txb_Password1.Font = new Font("Comic Sans MS", 12F);
            txb_Password1.Location = new Point(350, 366);
            txb_Password1.Name = "txb_Password1";
            txb_Password1.PasswordChar = '*';
            txb_Password1.Size = new Size(500, 41);
            txb_Password1.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Comic Sans MS", 14F, FontStyle.Bold);
            label4.Location = new Point(350, 437);
            label4.Name = "label4";
            label4.Size = new Size(275, 39);
            label4.TabIndex = 8;
            label4.Text = "Re-enter Password";
            // 
            // txb_Password2
            // 
            txb_Password2.Font = new Font("Comic Sans MS", 12F);
            txb_Password2.Location = new Point(350, 489);
            txb_Password2.Name = "txb_Password2";
            txb_Password2.PasswordChar = '*';
            txb_Password2.Size = new Size(500, 41);
            txb_Password2.TabIndex = 9;
            // 
            // btn_DangKy
            // 
            btn_DangKy.BackColor = Color.White;
            btn_DangKy.Font = new Font("Comic Sans MS", 20F, FontStyle.Bold);
            btn_DangKy.Location = new Point(470, 580);
            btn_DangKy.Name = "btn_DangKy";
            btn_DangKy.Size = new Size(260, 100);
            btn_DangKy.TabIndex = 10;
            btn_DangKy.Text = "SIGN UP";
            btn_DangKy.UseVisualStyleBackColor = false;
            btn_DangKy.Click += btn_DangKy_Click;
            // 
            // Dang_Ky
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 739);
            Controls.Add(btn_DangKy);
            Controls.Add(txb_Password2);
            Controls.Add(label4);
            Controls.Add(txb_Password1);
            Controls.Add(label3);
            Controls.Add(txt_Email);
            Controls.Add(label2);
            Controls.Add(txb_Username);
            Controls.Add(label1);
            Controls.Add(btn_Back);
            Controls.Add(pictureBox1);
            Name = "Dang_Ky";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sign Up";
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
        private System.Windows.Forms.TextBox txt_Email;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txb_Password1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txb_Password2;
        private System.Windows.Forms.Button btn_DangKy;
    }
}
