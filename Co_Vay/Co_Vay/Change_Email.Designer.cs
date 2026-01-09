namespace Co_Vay
{
    partial class Change_Email
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn_XacNhan2 = new Button();
            txt_nEmail = new TextBox();
            btn_BackMail = new Button();
            SuspendLayout();
            // 
            // btn_XacNhan2
            // 
            btn_XacNhan2.BackColor = Color.FromArgb(255, 174, 144);
            btn_XacNhan2.FlatAppearance.BorderSize = 0;
            btn_XacNhan2.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 174, 144);
            btn_XacNhan2.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 174, 144);
            btn_XacNhan2.FlatStyle = FlatStyle.Flat;
            btn_XacNhan2.Font = new Font("Arial", 25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_XacNhan2.ForeColor = Color.FromArgb(255, 255, 255);
            btn_XacNhan2.Location = new Point(466, 550);
            btn_XacNhan2.Name = "btn_XacNhan2";
            btn_XacNhan2.Size = new Size(500, 105);
            btn_XacNhan2.TabIndex = 22;
            btn_XacNhan2.Text = "CHANG EMAIL";
            btn_XacNhan2.UseVisualStyleBackColor = false;
            btn_XacNhan2.Click += btn_XacNhan2_Click;
            // 
            // txt_nEmail
            // 
            txt_nEmail.BorderStyle = BorderStyle.None;
            txt_nEmail.Font = new Font("Arial", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_nEmail.Location = new Point(360, 430);
            txt_nEmail.Name = "txt_nEmail";
            txt_nEmail.Size = new Size(621, 37);
            txt_nEmail.TabIndex = 21;
            // 
            // btn_BackMail
            // 
            btn_BackMail.BackColor = Color.White;
            btn_BackMail.Font = new Font("Arial", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_BackMail.Location = new Point(36, 20);
            btn_BackMail.Name = "btn_BackMail";
            btn_BackMail.Size = new Size(120, 60);
            btn_BackMail.TabIndex = 17;
            btn_BackMail.Text = "Back";
            btn_BackMail.UseVisualStyleBackColor = false;
            btn_BackMail.Click += btn_BackMail_Click;
            // 
            // Change_Email
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Change_Email;
            ClientSize = new Size(1438, 1024);
            Controls.Add(txt_nEmail);
            Controls.Add(btn_BackMail);
            Controls.Add(btn_XacNhan2);
            Name = "Change_Email";
            Text = "Change_Email";
            Load += Change_Email_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_XacNhan2;
        private TextBox txt_nEmail;
        private Button btn_BackMail;
    }
}
