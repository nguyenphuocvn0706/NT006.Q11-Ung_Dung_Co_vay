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
            btn_GM_Back = new Button();
            btn_W_Comp = new Button();
            btn_W_Player = new Button();
            btn_Online = new Button();
            SuspendLayout();
            // 
            // btn_GM_Back
            // 
            btn_GM_Back.FlatAppearance.BorderSize = 0;
            btn_GM_Back.FlatStyle = FlatStyle.Flat;
            btn_GM_Back.Image = Properties.Resources.GM_Back;
            btn_GM_Back.Location = new Point(485, 713);
            btn_GM_Back.Name = "btn_GM_Back";
            btn_GM_Back.Size = new Size(456, 134);
            btn_GM_Back.TabIndex = 1;
            btn_GM_Back.UseVisualStyleBackColor = true;
            btn_GM_Back.Click += btn_GM_Back_Click;
            // 
            // btn_W_Comp
            // 
            btn_W_Comp.FlatAppearance.BorderSize = 0;
            btn_W_Comp.FlatStyle = FlatStyle.Flat;
            btn_W_Comp.Image = Properties.Resources.Play_With_Comp;
            btn_W_Comp.Location = new Point(302, 297);
            btn_W_Comp.Name = "btn_W_Comp";
            btn_W_Comp.Size = new Size(834, 144);
            btn_W_Comp.TabIndex = 2;
            btn_W_Comp.UseVisualStyleBackColor = true;
            btn_W_Comp.Click += btn_W_Comp_Click;
            // 
            // btn_W_Player
            // 
            btn_W_Player.FlatAppearance.BorderSize = 0;
            btn_W_Player.FlatStyle = FlatStyle.Flat;
            btn_W_Player.Image = Properties.Resources.Play_W_Player;
            btn_W_Player.Location = new Point(305, 445);
            btn_W_Player.Name = "btn_W_Player";
            btn_W_Player.Size = new Size(828, 130);
            btn_W_Player.TabIndex = 3;
            btn_W_Player.UseVisualStyleBackColor = true;
            btn_W_Player.Click += btn_W_Player_Click;
            // 
            // btn_Online
            // 
            btn_Online.FlatAppearance.BorderSize = 0;
            btn_Online.FlatStyle = FlatStyle.Flat;
            btn_Online.Image = Properties.Resources.Play_Online;
            btn_Online.Location = new Point(304, 574);
            btn_Online.Name = "btn_Online";
            btn_Online.Size = new Size(831, 135);
            btn_Online.TabIndex = 4;
            btn_Online.UseVisualStyleBackColor = true;
            btn_Online.Click += btn_Online_Click;
            // 
            // GameMode
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.GameMode;
            ClientSize = new Size(1438, 1024);
            Controls.Add(btn_Online);
            Controls.Add(btn_W_Player);
            Controls.Add(btn_W_Comp);
            Controls.Add(btn_GM_Back);
            Name = "GameMode";
            Text = "GameMode";
            ResumeLayout(false);
        }

        #endregion
        private Button btn_GM_Back;
        private Button btn_W_Comp;
        private Button btn_W_Player;
        private Button btn_Online;
    }
}