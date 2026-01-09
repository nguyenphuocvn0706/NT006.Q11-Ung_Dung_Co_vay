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
            txt_Email = new TextBox();
            btn_XacNhan = new Button();
            SuspendLayout();
            // 
            // btn_Back
            // 
            btn_Back.BackColor = Color.White;
            btn_Back.Font = new Font("Arial Narrow", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_Back.Location = new Point(30, 25);
            btn_Back.Name = "btn_Back";
            btn_Back.Size = new Size(134, 54);
            btn_Back.TabIndex = 0;
            btn_Back.Text = "Back";
            btn_Back.UseVisualStyleBackColor = false;
            btn_Back.Click += btn_Back_Click;
            // 
            // txt_Email
            // 
            txt_Email.BorderStyle = BorderStyle.None;
            txt_Email.Font = new Font("Arial", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_Email.Location = new Point(362, 431);
            txt_Email.Name = "txt_Email";
            txt_Email.Size = new Size(619, 37);
            txt_Email.TabIndex = 2;
            
            // 
            // btn_XacNhan
            // 
            btn_XacNhan.BackColor = Color.FromArgb(255, 174, 144);
            btn_XacNhan.FlatAppearance.BorderSize = 0;
            btn_XacNhan.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 174, 144);
            btn_XacNhan.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 174, 144);
            btn_XacNhan.FlatStyle = FlatStyle.Flat;
            btn_XacNhan.Font = new Font("Arial", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_XacNhan.ForeColor = Color.FromArgb(255, 255, 255);
            btn_XacNhan.Location = new Point(468, 550);
            btn_XacNhan.Name = "btn_XacNhan";
            btn_XacNhan.Size = new Size(496, 106);
            btn_XacNhan.TabIndex = 3;
            btn_XacNhan.Text = "CONFIRM";
            btn_XacNhan.UseVisualStyleBackColor = false;
            btn_XacNhan.Click += btn_XacNhan_Click;
            // 
            // Quen_Mat_Khau
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = SystemColors.Control;
            BackgroundImage = Properties.Resources.Forgot_Pass;
            ClientSize = new Size(1438, 1024);
            Controls.Add(btn_XacNhan);
            Controls.Add(txt_Email);
            Controls.Add(btn_Back);
            Name = "Quen_Mat_Khau";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quên Mật Khẩu";
            
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_Back;
        private TextBox txt_Email;
        private Button btn_XacNhan;
    }
}
