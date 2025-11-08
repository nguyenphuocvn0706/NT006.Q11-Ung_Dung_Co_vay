namespace Co_Vay
{
    partial class Man_Hinh_Chinh
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
            btn_Play = new Button();
            btn_Rankings = new Button();
            btn_History = new Button();
            btn_Achievements = new Button();
            btn_Rewards = new Button();
            btn_Profile = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Man_Hinh_Chinh;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1536, 1006);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // btn_Play
            // 
            btn_Play.FlatAppearance.BorderSize = 0;
            btn_Play.FlatStyle = FlatStyle.Flat;
            btn_Play.Image = Properties.Resources.PLAY;
            btn_Play.Location = new Point(121, 69);
            btn_Play.Name = "btn_Play";
            btn_Play.Size = new Size(762, 251);
            btn_Play.TabIndex = 1;
            btn_Play.UseVisualStyleBackColor = true;
            // 
            // btn_Rankings
            // 
            btn_Rankings.FlatAppearance.BorderSize = 0;
            btn_Rankings.FlatStyle = FlatStyle.Flat;
            btn_Rankings.Image = Properties.Resources.RANKINGS;
            btn_Rankings.Location = new Point(182, 344);
            btn_Rankings.Name = "btn_Rankings";
            btn_Rankings.Size = new Size(705, 220);
            btn_Rankings.TabIndex = 2;
            btn_Rankings.UseVisualStyleBackColor = true;
            btn_Rankings.Click += btn_Rankings_Click;
            // 
            // btn_History
            // 
            btn_History.FlatAppearance.BorderSize = 0;
            btn_History.FlatStyle = FlatStyle.Flat;
            btn_History.Image = Properties.Resources.HISTORY;
            btn_History.Location = new Point(188, 583);
            btn_History.Name = "btn_History";
            btn_History.Size = new Size(699, 201);
            btn_History.TabIndex = 3;
            btn_History.UseVisualStyleBackColor = true;
            // 
            // btn_Achievements
            // 
            btn_Achievements.FlatAppearance.BorderSize = 0;
            btn_Achievements.FlatStyle = FlatStyle.Flat;
            btn_Achievements.Image = Properties.Resources.Achievements;
            btn_Achievements.Location = new Point(893, 84);
            btn_Achievements.Name = "btn_Achievements";
            btn_Achievements.Size = new Size(273, 240);
            btn_Achievements.TabIndex = 4;
            btn_Achievements.UseVisualStyleBackColor = true;
            // 
            // btn_Rewards
            // 
            btn_Rewards.FlatAppearance.BorderSize = 0;
            btn_Rewards.FlatStyle = FlatStyle.Flat;
            btn_Rewards.Image = Properties.Resources.REWARDS;
            btn_Rewards.Location = new Point(1172, 98);
            btn_Rewards.Name = "btn_Rewards";
            btn_Rewards.Size = new Size(268, 222);
            btn_Rewards.TabIndex = 5;
            btn_Rewards.UseVisualStyleBackColor = true;
            // 
            // btn_Profile
            // 
            btn_Profile.FlatAppearance.BorderSize = 0;
            btn_Profile.FlatStyle = FlatStyle.Flat;
            btn_Profile.Image = Properties.Resources.Profile;
            btn_Profile.Location = new Point(1148, 708);
            btn_Profile.Name = "btn_Profile";
            btn_Profile.Size = new Size(301, 265);
            btn_Profile.TabIndex = 6;
            btn_Profile.UseVisualStyleBackColor = true;
            // 
            // Man_Hinh_Chinh
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1532, 1002);
            Controls.Add(btn_Profile);
            Controls.Add(btn_Rewards);
            Controls.Add(btn_Achievements);
            Controls.Add(btn_History);
            Controls.Add(btn_Rankings);
            Controls.Add(btn_Play);
            Controls.Add(pictureBox1);
            Name = "Man_Hinh_Chinh";
            Text = "Man_Hinh_Chinh";
            Load += Man_Hinh_Chinh_Load_1;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private Button btn_Play;
        private Button btn_Rankings;
        private Button btn_History;
        private Button btn_Achievements;
        private Button btn_Rewards;
        private Button btn_Profile;
    }
}