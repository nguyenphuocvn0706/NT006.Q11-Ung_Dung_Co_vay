namespace Co_Vay
{
    partial class Quen_Mat_Khau
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support — do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn_Back = new Button();
            lbl_Username = new Label();
            txt_Username = new TextBox();
            lbl_Email = new Label();
            txt_Email = new TextBox();
            btn_XacNhan = new Button();
            SuspendLayout();
            // 
            // btn_Back
            // 
            btn_Back.BackColor = Color.White;
            btn_Back.Font = new Font("Comic Sans MS", 16F, FontStyle.Bold);
            btn_Back.Location = new Point(30, 25);
            btn_Back.Name = "btn_Back";
            btn_Back.Size = new Size(120, 60);
            btn_Back.TabIndex = 0;
            btn_Back.Text = "Back";
            btn_Back.UseVisualStyleBackColor = false;
            btn_Back.Click += btn_Back_Click;
            // 
            // lbl_Username
            // 
            lbl_Username.AutoSize = true;
            lbl_Username.BackColor = Color.White;
            lbl_Username.Font = new Font("Comic Sans MS", 14F, FontStyle.Bold);
            lbl_Username.Location = new Point(350, 180);
            lbl_Username.Name = "lbl_Username";
            lbl_Username.Size = new Size(236, 39);
            lbl_Username.TabIndex = 1;
            lbl_Username.Text = "Enter Username";
            // 
            // txt_Username
            // 
            txt_Username.Font = new Font("Segoe UI", 14F);
            txt_Username.Location = new Point(350, 230);
            txt_Username.Name = "txt_Username";
            txt_Username.Size = new Size(500, 45);
            txt_Username.TabIndex = 2;
            // 
            // lbl_Email
            // 
            lbl_Email.AutoSize = true;
            lbl_Email.BackColor = Color.White;
            lbl_Email.Font = new Font("Comic Sans MS", 14F, FontStyle.Bold);
            lbl_Email.Location = new Point(350, 310);
            lbl_Email.Name = "lbl_Email";
            lbl_Email.Size = new Size(174, 39);
            lbl_Email.TabIndex = 3;
            lbl_Email.Text = "Enter Email";
            // 
            // txt_Email
            // 
            txt_Email.Font = new Font("Segoe UI", 14F);
            txt_Email.Location = new Point(350, 360);
            txt_Email.Name = "txt_Email";
            txt_Email.Size = new Size(500, 45);
            txt_Email.TabIndex = 4;
            // 
            // btn_XacNhan
            // 
            btn_XacNhan.BackColor = Color.White;
            btn_XacNhan.Font = new Font("Comic Sans MS", 20F, FontStyle.Bold);
            btn_XacNhan.Location = new Point(430, 480);
            btn_XacNhan.Name = "btn_XacNhan";
            btn_XacNhan.Size = new Size(360, 100);
            btn_XacNhan.TabIndex = 5;
            btn_XacNhan.Text = "XÁC NHẬN";
            btn_XacNhan.UseVisualStyleBackColor = false;
            btn_XacNhan.Click += btn_XacNhan_Click;
            // 
            // Quen_Mat_Khau
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.SkyBlue;
            BackgroundImage = Properties.Resources.Nen;
            ClientSize = new Size(1200, 739);
            Controls.Add(btn_XacNhan);
            Controls.Add(txt_Email);
            Controls.Add(lbl_Email);
            Controls.Add(txt_Username);
            Controls.Add(lbl_Username);
            Controls.Add(btn_Back);
            Name = "Quen_Mat_Khau";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quên Mật Khẩu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btn_Back;
        private System.Windows.Forms.Label lbl_Username;
        private System.Windows.Forms.TextBox txt_Username;
        private System.Windows.Forms.Label lbl_Email;
        private System.Windows.Forms.TextBox txt_Email;
        private System.Windows.Forms.Button btn_XacNhan;
    }
}
