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
            pictureBox1 = new PictureBox();
            btn_Back = new Button();
            txb_Password2 = new TextBox();
            label4 = new Label();
            txb_Password1 = new TextBox();
            label3 = new Label();
            textBox1 = new TextBox();
            label1 = new Label();
            btn_XacNhan = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = Properties.Resources.Nen1;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1241, 846);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
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
            txb_Password2.Font = new Font("Comic Sans MS", 12F);
            txb_Password2.Location = new Point(375, 352);
            txb_Password2.Name = "txb_Password2";
            txb_Password2.PasswordChar = '*';
            txb_Password2.Size = new Size(500, 41);
            txb_Password2.TabIndex = 13;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Comic Sans MS", 14F, FontStyle.Bold);
            label4.Location = new Point(375, 300);
            label4.Name = "label4";
            label4.Size = new Size(295, 39);
            label4.TabIndex = 12;
            label4.Text = "Enter New Password";
            // 
            // txb_Password1
            // 
            txb_Password1.Font = new Font("Comic Sans MS", 12F);
            txb_Password1.Location = new Point(375, 225);
            txb_Password1.Name = "txb_Password1";
            txb_Password1.PasswordChar = '*';
            txb_Password1.Size = new Size(500, 41);
            txb_Password1.TabIndex = 11;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Comic Sans MS", 14F, FontStyle.Bold);
            label3.Location = new Point(375, 171);
            label3.Name = "label3";
            label3.Size = new Size(295, 39);
            label3.TabIndex = 10;
            label3.Text = "Enter Last Password";
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Comic Sans MS", 12F);
            textBox1.Location = new Point(375, 464);
            textBox1.Name = "textBox1";
            textBox1.PasswordChar = '*';
            textBox1.Size = new Size(500, 41);
            textBox1.TabIndex = 15;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Comic Sans MS", 14F, FontStyle.Bold);
            label1.Location = new Point(375, 412);
            label1.Name = "label1";
            label1.Size = new Size(345, 39);
            label1.TabIndex = 14;
            label1.Text = "Re-enter New Password";
            // 
            // btn_XacNhan
            // 
            btn_XacNhan.BackColor = Color.White;
            btn_XacNhan.Font = new Font("Comic Sans MS", 20F, FontStyle.Bold);
            btn_XacNhan.Location = new Point(448, 546);
            btn_XacNhan.Name = "btn_XacNhan";
            btn_XacNhan.Size = new Size(361, 125);
            btn_XacNhan.TabIndex = 16;
            btn_XacNhan.Text = "CHANGE PASSWORD";
            btn_XacNhan.UseVisualStyleBackColor = false;
            btn_XacNhan.Click += btn_XacNhan_Click;
            // 
            // Change_Password
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1240, 844);
            Controls.Add(btn_XacNhan);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(txb_Password2);
            Controls.Add(label4);
            Controls.Add(txb_Password1);
            Controls.Add(label3);
            Controls.Add(btn_Back);
            Controls.Add(pictureBox1);
            Name = "Change_Password";
            Text = "Change_Password";
            Load += Change_Password_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Button btn_Back;
        private TextBox txb_Password2;
        private Label label4;
        private TextBox txb_Password1;
        private Label label3;
        private TextBox textBox1;
        private Label label1;
        private Button btn_XacNhan;
    }
}