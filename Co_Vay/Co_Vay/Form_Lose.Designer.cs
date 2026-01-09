namespace Co_Vay
{
    partial class Form_Lose
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Lose));
            txt_White_Score = new TextBox();
            txt_Black_Score = new TextBox();
            SuspendLayout();
            // 
            // txt_White_Score
            // 
            txt_White_Score.BackColor = Color.FromArgb(126, 126, 126);
            txt_White_Score.BorderStyle = BorderStyle.None;
            txt_White_Score.Font = new Font("Arial", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_White_Score.ForeColor = SystemColors.Window;
            txt_White_Score.Location = new Point(242, 521);
            txt_White_Score.Name = "txt_White_Score";
            txt_White_Score.Size = new Size(292, 37);
            txt_White_Score.TabIndex = 3;
            // 
            // txt_Black_Score
            // 
            txt_Black_Score.BackColor = Color.Gray;
            txt_Black_Score.BorderStyle = BorderStyle.None;
            txt_Black_Score.Font = new Font("Arial", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_Black_Score.ForeColor = SystemColors.Window;
            txt_Black_Score.Location = new Point(242, 429);
            txt_Black_Score.Name = "txt_Black_Score";
            txt_Black_Score.Size = new Size(292, 37);
            txt_Black_Score.TabIndex = 2;
            // 
            // Form_Lose
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(768, 634);
            Controls.Add(txt_White_Score);
            Controls.Add(txt_Black_Score);
            Name = "Form_Lose";
            Text = "Form_Lose";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txt_White_Score;
        private TextBox txt_Black_Score;
    }
}