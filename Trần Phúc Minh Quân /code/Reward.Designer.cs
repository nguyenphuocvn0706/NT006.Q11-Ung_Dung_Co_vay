namespace Co_Vay
{
    partial class Reward
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reward));
            btn_Back = new Button();
            pnl_Missions = new Panel();
            btn_Claim = new Button();
            lbl_Progress = new Label();
            lbl_MissionTitle = new Label();
            pnl_Missions.SuspendLayout();
            SuspendLayout();
            // 
            // btn_Back
            // 
            btn_Back.BackColor = Color.FromArgb(255, 230, 200);
            btn_Back.BackgroundImage = Properties.Resources.BACK_RANKS;
            btn_Back.FlatAppearance.BorderSize = 0;
            btn_Back.FlatStyle = FlatStyle.Flat;
            btn_Back.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            btn_Back.Location = new Point(498, 691);
            btn_Back.Name = "btn_Back";
            btn_Back.Size = new Size(435, 140);
            btn_Back.TabIndex = 2;
            btn_Back.UseVisualStyleBackColor = false;
            btn_Back.Click += btn_Back_Click;
            // 
            // pnl_Missions
            // 
            pnl_Missions.AutoScroll = true;
            pnl_Missions.BackColor = Color.Transparent;
            pnl_Missions.Controls.Add(btn_Claim);
            pnl_Missions.Controls.Add(lbl_Progress);
            pnl_Missions.Controls.Add(lbl_MissionTitle);
            pnl_Missions.Location = new Point(290, 206);
            pnl_Missions.Name = "pnl_Missions";
            pnl_Missions.Size = new Size(860, 488);
            pnl_Missions.TabIndex = 3;
            // 
            // btn_Claim
            // 
            btn_Claim.BackColor = Color.Coral;
            btn_Claim.FlatStyle = FlatStyle.Flat;
            btn_Claim.ForeColor = Color.White;
            btn_Claim.Location = new Point(695, 58);
            btn_Claim.Name = "btn_Claim";
            btn_Claim.Size = new Size(122, 39);
            btn_Claim.TabIndex = 2;
            btn_Claim.Text = "Nhận";
            btn_Claim.UseVisualStyleBackColor = false;
            // 
            // lbl_Progress
            // 
            lbl_Progress.AutoSize = true;
            lbl_Progress.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_Progress.Location = new Point(534, 59);
            lbl_Progress.Name = "lbl_Progress";
            lbl_Progress.Size = new Size(104, 32);
            lbl_Progress.TabIndex = 1;
            lbl_Progress.Text = "Progress";
            // 
            // lbl_MissionTitle
            // 
            lbl_MissionTitle.AutoSize = true;
            lbl_MissionTitle.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_MissionTitle.Location = new Point(58, 52);
            lbl_MissionTitle.Name = "lbl_MissionTitle";
            lbl_MissionTitle.Size = new Size(96, 32);
            lbl_MissionTitle.TabIndex = 0;
            lbl_MissionTitle.Text = "Mission";
            // 
            // Reward
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1443, 903);
            Controls.Add(pnl_Missions);
            Controls.Add(btn_Back);
            DoubleBuffered = true;
            Name = "Reward";
            Text = "Reward";
            pnl_Missions.ResumeLayout(false);
            pnl_Missions.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btn_Back;
        private Panel pnl_Missions;
        private Label lbl_Progress;
        private Label lbl_MissionTitle;
        private Button btn_Claim;
    }
}