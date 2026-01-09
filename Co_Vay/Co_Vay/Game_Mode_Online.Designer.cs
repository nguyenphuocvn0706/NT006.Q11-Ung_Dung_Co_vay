namespace Co_Vay
{
    partial class Game_Mode_Online
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game_Mode_Online));
            btn_Random = new Button();
            btn_Private = new Button();
            btn_Back = new Button();
            SuspendLayout();
            // 
            // btn_Random
            // 
            btn_Random.BackgroundImage = (Image)resources.GetObject("btn_Random.BackgroundImage");
            btn_Random.FlatAppearance.BorderSize = 0;
            btn_Random.FlatStyle = FlatStyle.Flat;
            btn_Random.Location = new Point(463, 399);
            btn_Random.Name = "btn_Random";
            btn_Random.Size = new Size(507, 95);
            btn_Random.TabIndex = 0;
            btn_Random.UseVisualStyleBackColor = true;
            btn_Random.Click += btn_Random_Click;
            // 
            // btn_Private
            // 
            btn_Private.BackgroundImage = (Image)resources.GetObject("btn_Private.BackgroundImage");
            btn_Private.FlatAppearance.BorderSize = 0;
            btn_Private.FlatStyle = FlatStyle.Flat;
            btn_Private.Location = new Point(463, 564);
            btn_Private.Name = "btn_Private";
            btn_Private.Size = new Size(507, 95);
            btn_Private.TabIndex = 1;
            btn_Private.UseVisualStyleBackColor = true;
            btn_Private.Click += btn_Private_Click;
            // 
            // btn_Back
            // 
            btn_Back.BackgroundImage = (Image)resources.GetObject("btn_Back.BackgroundImage");
            btn_Back.FlatAppearance.BorderSize = 0;
            btn_Back.FlatStyle = FlatStyle.Flat;
            btn_Back.Location = new Point(626, 708);
            btn_Back.Name = "btn_Back";
            btn_Back.Size = new Size(192, 123);
            btn_Back.TabIndex = 2;
            btn_Back.UseVisualStyleBackColor = true;
            btn_Back.Click += btn_Back_Click;
            // 
            // Game_Mode_Online
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1438, 1024);
            Controls.Add(btn_Back);
            Controls.Add(btn_Private);
            Controls.Add(btn_Random);
            Name = "Game_Mode_Online";
            Text = "Game_Mode_Online";
            ResumeLayout(false);
        }

        #endregion

        private Button btn_Random;
        private Button btn_Private;
        private Button btn_Back;
    }
}