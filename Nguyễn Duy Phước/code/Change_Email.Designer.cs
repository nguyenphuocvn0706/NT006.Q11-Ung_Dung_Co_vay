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
            label1 = new Label();
            btn_BackMail = new Button();
            SuspendLayout();
            // 
            // btn_XacNhan2
            // 
            btn_XacNhan2.BackColor = Color.White;
            btn_XacNhan2.Font = new Font("Comic Sans MS", 20F, FontStyle.Bold);
            btn_XacNhan2.Location = new Point(505, 422);
            btn_XacNhan2.Name = "btn_XacNhan2";
            btn_XacNhan2.Size = new Size(360, 100);
            btn_XacNhan2.TabIndex = 22;
            btn_XacNhan2.Text = "CHANGE EMAIL";
            btn_XacNhan2.UseVisualStyleBackColor = false;
            // 👉 Gắn lại đúng sự kiện duy nhất:
            // 
            // txt_nEmail
            // 
            txt_nEmail.Font = new Font("Comic Sans MS", 12F);
            txt_nEmail.Location = new Point(432, 340);
            txt_nEmail.Name = "txt_nEmail";
            txt_nEmail.Size = new Size(500, 41);
            txt_nEmail.TabIndex = 21;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Comic Sans MS", 14F, FontStyle.Bold);
            label1.Location = new Point(432, 288);
            label1.Name = "label1";
            label1.Size = new Size(244, 39);
            label1.TabIndex = 20;
            label1.Text = "Enter New Email";
            // 
            // btn_BackMail
            // 
            btn_BackMail.BackColor = Color.White;
            btn_BackMail.Font = new Font("Comic Sans MS", 16F, FontStyle.Bold);
            btn_BackMail.Location = new Point(36, 20);
            btn_BackMail.Name = "btn_BackMail";
            btn_BackMail.Size = new Size(120, 60);
            btn_BackMail.TabIndex = 17;
            btn_BackMail.Text = "Back";
            btn_BackMail.UseVisualStyleBackColor = false;
            // 
            // Change_Email
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Nen;
            ClientSize = new Size(1363, 842);
            Controls.Add(btn_XacNhan2);
            Controls.Add(txt_nEmail);
            Controls.Add(label1);
            Controls.Add(btn_BackMail);
            Name = "Change_Email";
            Text = "Change_Email";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_XacNhan2;
        private TextBox txt_nEmail;
        private Label label1;
        private Button btn_BackMail;
    }
}
