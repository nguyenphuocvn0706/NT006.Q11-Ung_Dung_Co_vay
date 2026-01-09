namespace Co_Vay
{
    partial class Change_Password
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
            btn_Back = new Button();
            txb_Password2 = new TextBox();
            txb_Password1 = new TextBox();
            textBox1 = new TextBox();
            btn_XacNhan = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btn_Back
            // 
            btn_Back.BackColor = Color.White;
            btn_Back.Font = new Font("Comic Sans MS", 16F, FontStyle.Bold);
            btn_Back.Location = new Point(75, 36);
            btn_Back.Name = "btn_Back";
            btn_Back.Size = new Size(120, 60);
            btn_Back.TabIndex = 4;
            btn_Back.Text = "Back";
            btn_Back.UseVisualStyleBackColor = false;
            btn_Back.Click += btn_Back_Click;
            // 
            // txb_Password2
            // 
            txb_Password2.BorderStyle = BorderStyle.None;
            txb_Password2.Font = new Font("Arial", 16F);
            txb_Password2.Location = new Point(360, 502);
            txb_Password2.Name = "txb_Password2";
            txb_Password2.PasswordChar = '*';
            txb_Password2.Size = new Size(711, 37);
            txb_Password2.TabIndex = 13;
            // 
            // txb_Password1
            // 
            txb_Password1.BorderStyle = BorderStyle.None;
            txb_Password1.Font = new Font("Arial", 16F);
            txb_Password1.Location = new Point(360, 359);
            txb_Password1.Name = "txb_Password1";
            txb_Password1.PasswordChar = '*';
            txb_Password1.Size = new Size(711, 37);
            txb_Password1.TabIndex = 11;
            // 
            // textBox1
            // 
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Arial", 16F);
            textBox1.Location = new Point(360, 644);
            textBox1.Name = "textBox1";
            textBox1.PasswordChar = '*';
            textBox1.Size = new Size(711, 37);
            textBox1.TabIndex = 15;
            // 
            // btn_XacNhan
            // 
            btn_XacNhan.BackColor = Color.FromArgb(255, 211, 144);
            btn_XacNhan.FlatAppearance.BorderSize = 0;
            btn_XacNhan.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 211, 144);
            btn_XacNhan.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 211, 144);
            btn_XacNhan.FlatStyle = FlatStyle.Flat;
            btn_XacNhan.Font = new Font("Arial", 35F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_XacNhan.ForeColor = Color.FromArgb(255, 116, 65);
            btn_XacNhan.Location = new Point(479, 834);
            btn_XacNhan.Name = "btn_XacNhan";
            btn_XacNhan.Size = new Size(481, 105);
            btn_XacNhan.TabIndex = 16;
            btn_XacNhan.Text = "CONFIRM";
            btn_XacNhan.UseVisualStyleBackColor = false;
            btn_XacNhan.Click += btn_XacNhan_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = Properties.Resources.Screenshot_2025_12_22_231241;
            pictureBox1.Image = Properties.Resources.Screenshot_2025_12_22_231241;
            pictureBox1.Location = new Point(399, 100);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(630, 96);
            pictureBox1.TabIndex = 17;
            pictureBox1.TabStop = false;
            // 
            // Change_Password
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Change_Pass;
            ClientSize = new Size(1438, 1024);
            Controls.Add(pictureBox1);
            Controls.Add(btn_XacNhan);
            Controls.Add(textBox1);
            Controls.Add(txb_Password2);
            Controls.Add(txb_Password1);
            Controls.Add(btn_Back);
            Name = "Change_Password";
            Text = "Change_Password";
            Load += Change_Password_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btn_Back;
        private TextBox txb_Password2;
        private TextBox txb_Password1;
        private TextBox textBox1;
        private Button btn_XacNhan;
        private PictureBox pictureBox1;
    }
}