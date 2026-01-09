namespace Co_Vay
{
    partial class Form_Win
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Win));
            txt_Black_Score = new TextBox();
            txt_White_Score = new TextBox();
            SuspendLayout();
            // 
            // txt_Black_Score
            // 
            txt_Black_Score.BackColor = Color.FromArgb(255, 226, 195);
            txt_Black_Score.BorderStyle = BorderStyle.None;
            txt_Black_Score.Font = new Font("Arial", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_Black_Score.ForeColor = SystemColors.Window;
            txt_Black_Score.Location = new Point(249, 459);
            txt_Black_Score.Name = "txt_Black_Score";
            txt_Black_Score.Size = new Size(292, 37);
            txt_Black_Score.TabIndex = 0;
            // 
            // txt_White_Score
            // 
            txt_White_Score.BackColor = Color.FromArgb(255, 226, 195);
            txt_White_Score.BorderStyle = BorderStyle.None;
            txt_White_Score.Font = new Font("Arial", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_White_Score.ForeColor = SystemColors.Window;
            txt_White_Score.Location = new Point(249, 545);
            txt_White_Score.Name = "txt_White_Score";
            txt_White_Score.Size = new Size(292, 37);
            txt_White_Score.TabIndex = 1;
            // 
            // Form_Win
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(778, 634);
            Controls.Add(txt_White_Score);
            Controls.Add(txt_Black_Score);
            Name = "Form_Win";
            Text = "Form_Win";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txt_Black_Score;
        private TextBox txt_White_Score;
    }
}