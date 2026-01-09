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
            btn_Play = new Button();
            btn_Rankings = new Button();
            btn_History = new Button();
            btn_Log_Out = new Button();
            btn_Rewards = new Button();
            btn_Profile = new Button();
            SuspendLayout();
            // 
            // btn_Play
            // 
            btn_Play.FlatAppearance.BorderSize = 0;
            btn_Play.FlatStyle = FlatStyle.Flat;
            btn_Play.Image = Properties.Resources.Play;
            btn_Play.Location = new Point(321, 434);
            btn_Play.Name = "btn_Play";
            btn_Play.Size = new Size(804, 129);
            btn_Play.TabIndex = 1;
            btn_Play.UseVisualStyleBackColor = true;
            btn_Play.Click += btn_Play_Click;
            // 
            // btn_Rankings
            // 
            btn_Rankings.FlatAppearance.BorderSize = 0;
            btn_Rankings.FlatStyle = FlatStyle.Flat;
            btn_Rankings.Image = Properties.Resources.Ranks;
            btn_Rankings.Location = new Point(319, 572);
            btn_Rankings.Name = "btn_Rankings";
            btn_Rankings.Size = new Size(810, 134);
            btn_Rankings.TabIndex = 2;
            btn_Rankings.UseVisualStyleBackColor = true;
            btn_Rankings.Click += btn_Rankings_Click;
            // 
            // btn_History
            // 
            btn_History.FlatAppearance.BorderSize = 0;
            btn_History.FlatStyle = FlatStyle.Flat;
            btn_History.Image = Properties.Resources.His;
            btn_History.Location = new Point(315, 706);
            btn_History.Name = "btn_History";
            btn_History.Size = new Size(810, 133);
            btn_History.TabIndex = 3;
            btn_History.UseVisualStyleBackColor = true;
            btn_History.Click += btn_History_Click;
            // 
            // btn_Log_Out
            // 
            btn_Log_Out.FlatAppearance.BorderSize = 0;
            btn_Log_Out.FlatStyle = FlatStyle.Flat;
            btn_Log_Out.Image = Properties.Resources.Screenshot_2025_12_26_115858;
            btn_Log_Out.Location = new Point(321, 197);
            btn_Log_Out.Name = "btn_Log_Out";
            btn_Log_Out.Size = new Size(252, 225);
            btn_Log_Out.TabIndex = 4;
            btn_Log_Out.UseVisualStyleBackColor = true;
            btn_Log_Out.Click += btn_Log_Out_Click;
            // 
            // btn_Rewards
            // 
            btn_Rewards.FlatAppearance.BorderSize = 0;
            btn_Rewards.FlatStyle = FlatStyle.Flat;
            btn_Rewards.Image = Properties.Resources.Rewards;
            btn_Rewards.Location = new Point(589, 176);
            btn_Rewards.Name = "btn_Rewards";
            btn_Rewards.Size = new Size(268, 254);
            btn_Rewards.TabIndex = 5;
            btn_Rewards.UseVisualStyleBackColor = true;
            btn_Rewards.Click += btn_Rewards_Click;
            // 
            // btn_Profile
            // 
            btn_Profile.FlatAppearance.BorderSize = 0;
            btn_Profile.FlatStyle = FlatStyle.Flat;
            btn_Profile.Image = Properties.Resources.Profile;
            btn_Profile.Location = new Point(868, 184);
            btn_Profile.Name = "btn_Profile";
            btn_Profile.Size = new Size(284, 250);
            btn_Profile.TabIndex = 6;
            btn_Profile.UseVisualStyleBackColor = true;
            btn_Profile.Click += btn_Profile_Click;
            // 
            // Man_Hinh_Chinh
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Home;
            ClientSize = new Size(1438, 1024);
            Controls.Add(btn_Profile);
            Controls.Add(btn_Rewards);
            Controls.Add(btn_Log_Out);
            Controls.Add(btn_History);
            Controls.Add(btn_Rankings);
            Controls.Add(btn_Play);
            Name = "Man_Hinh_Chinh";
            Text = "Man_Hinh_Chinh";
            ResumeLayout(false);
        }

        #endregion
        private Button btn_Play;
        private Button btn_Rankings;
        private Button btn_History;
        private Button btn_Log_Out;
        private Button btn_Rewards;
        private Button btn_Profile;
    }
}