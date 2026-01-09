namespace Co_Vay
{
    partial class Form_Match_Making
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Match_Making));
            btn_Cancel = new Button();
            SuspendLayout();
            // 
            // btn_Cancel
            // 
            btn_Cancel.BackgroundImage = (Image)resources.GetObject("btn_Cancel.BackgroundImage");
            btn_Cancel.FlatAppearance.BorderSize = 0;
            btn_Cancel.FlatStyle = FlatStyle.Flat;
            btn_Cancel.Location = new Point(506, 648);
            btn_Cancel.Name = "btn_Cancel";
            btn_Cancel.Size = new Size(438, 133);
            btn_Cancel.TabIndex = 0;
            btn_Cancel.UseVisualStyleBackColor = true;
            btn_Cancel.Click += Btn_Cancel_Click;
            // 
            // Form_Match_Making
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1438, 1024);
            Controls.Add(btn_Cancel);
            Name = "Form_Match_Making";
            Text = "Form_Match_Making";
            ResumeLayout(false);
        }

        #endregion

        private Button btn_Cancel;
    }
}