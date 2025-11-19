namespace Co_Vay
{
    partial class GameMode
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
            btn_GM_Back = new Button();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.game_mode;
            pictureBox1.Location = new Point(-5, -2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1498, 1003);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // btn_GM_Back
            // 
            btn_GM_Back.FlatAppearance.BorderSize = 0;
            btn_GM_Back.FlatStyle = FlatStyle.Flat;
            btn_GM_Back.Image = Properties.Resources.gmback;
            btn_GM_Back.Location = new Point(2, 837);
            btn_GM_Back.Name = "btn_GM_Back";
            btn_GM_Back.Size = new Size(305, 130);
            btn_GM_Back.TabIndex = 1;
            btn_GM_Back.UseVisualStyleBackColor = true;
            btn_GM_Back.Click += btn_GM_Back_Click;
            // 
            // button1
            // 
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Image = Properties.Resources.playcomp;
            button1.Location = new Point(233, 244);
            button1.Name = "button1";
            button1.Size = new Size(1106, 193);
            button1.TabIndex = 2;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Image = Properties.Resources.playp;
            button2.Location = new Point(247, 443);
            button2.Name = "button2";
            button2.Size = new Size(1056, 174);
            button2.TabIndex = 3;
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Image = Properties.Resources.Playonl;
            button3.Location = new Point(246, 625);
            button3.Name = "button3";
            button3.Size = new Size(1056, 174);
            button3.TabIndex = 4;
            button3.UseVisualStyleBackColor = true;
            // 
            // GameMode
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1492, 995);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(btn_GM_Back);
            Controls.Add(pictureBox1);
            Name = "GameMode";
            Text = "GameMode";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private Button btn_GM_Back;
        private Button button1;
        private Button button2;
        private Button button3;
    }
}