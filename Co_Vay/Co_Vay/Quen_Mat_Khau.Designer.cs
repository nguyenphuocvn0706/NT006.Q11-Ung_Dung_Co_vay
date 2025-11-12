namespace Co_Vay
{
    partial class Quen_Mat_Khau
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            btn_Back = new Button();
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
            // lbl_Email
            // 
            lbl_Email.AutoSize = true;
            lbl_Email.BackColor = Color.White;
            lbl_Email.Font = new Font("Comic Sans MS", 16F, FontStyle.Bold);
            lbl_Email.Location = new Point(350, 260);
            lbl_Email.Name = "lbl_Email";
            lbl_Email.Size = new Size(214, 45);
            lbl_Email.TabIndex = 1;
            lbl_Email.Text = "Enter Email:";
            // 
            // txt_Email
            // 
            txt_Email.Font = new Font("Segoe UI", 14F);
            txt_Email.Location = new Point(350, 320);
            txt_Email.Name = "txt_Email";
            txt_Email.Size = new Size(500, 45);
            txt_Email.TabIndex = 2;
            // 
            // btn_XacNhan
            // 
            btn_XacNhan.BackColor = Color.White;
            btn_XacNhan.Font = new Font("Comic Sans MS", 20F, FontStyle.Bold);
            btn_XacNhan.Location = new Point(430, 450);
            btn_XacNhan.Name = "btn_XacNhan";
            btn_XacNhan.Size = new Size(357, 126);
            btn_XacNhan.TabIndex = 3;
            btn_XacNhan.Text = "CHANGE PASSWORD";
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
            Controls.Add(btn_Back);
            Name = "Quen_Mat_Khau";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quên Mật Khẩu";
            Load += Quen_Mat_Khau_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_Back;
        private Label lbl_Email;
        private TextBox txt_Email;
        private Button btn_XacNhan;
    }
}
