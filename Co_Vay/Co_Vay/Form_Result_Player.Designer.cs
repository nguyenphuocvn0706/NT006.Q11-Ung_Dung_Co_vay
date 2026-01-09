namespace Co_Vay
{
    partial class Form_Result_Player
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Result_Player));
            txt_White_Score = new TextBox();
            txt_Black_Score = new TextBox();
            txt_Result = new TextBox();
            SuspendLayout();
            // 
            // txt_White_Score
            // 
            txt_White_Score.BackColor = Color.FromArgb(255, 226, 195);
            txt_White_Score.BorderStyle = BorderStyle.None;
            txt_White_Score.Font = new Font("Arial", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_White_Score.ForeColor = SystemColors.Window;
            txt_White_Score.Location = new Point(240, 549);
            txt_White_Score.Name = "txt_White_Score";
            txt_White_Score.Size = new Size(292, 37);
            txt_White_Score.TabIndex = 3;
            // 
            // txt_Black_Score
            // 
            txt_Black_Score.BackColor = Color.FromArgb(255, 226, 195);
            txt_Black_Score.BorderStyle = BorderStyle.None;
            txt_Black_Score.Font = new Font("Arial", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_Black_Score.ForeColor = SystemColors.Window;
            txt_Black_Score.Location = new Point(240, 463);
            txt_Black_Score.Name = "txt_Black_Score";
            txt_Black_Score.Size = new Size(292, 37);
            txt_Black_Score.TabIndex = 2;
            // 
            // txt_Result
            // 
            txt_Result.BackColor = Color.FromArgb(255, 191, 156);
            txt_Result.BorderStyle = BorderStyle.None;
            txt_Result.Font = new Font("Arial", 56F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txt_Result.ForeColor = SystemColors.Window;
            txt_Result.Location = new Point(12, 75);
            txt_Result.Name = "txt_Result";
            txt_Result.Size = new Size(744, 129);
            txt_Result.TabIndex = 4;
            txt_Result.Text = "BLACK WON";
            txt_Result.TextAlign = HorizontalAlignment.Center;
            // 
            // Form_Result_Player
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(768, 624);
            Controls.Add(txt_Result);
            Controls.Add(txt_White_Score);
            Controls.Add(txt_Black_Score);
            Name = "Form_Result_Player";
            Text = "Form_Result_Player";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txt_White_Score;
        private TextBox txt_Black_Score;
        private TextBox txt_Result;
    }
}