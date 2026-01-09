namespace Co_Vay
{
    partial class Match_Setting_Comp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Match_Setting_Comp));
            comboBox1 = new ComboBox();
            comboBox2 = new ComboBox();
            button1 = new Button();
            btn_Back = new Button();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.BackColor = Color.White;
            comboBox1.Font = new Font("Arial", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(743, 661);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(226, 40);
            comboBox1.TabIndex = 0;
            // 
            // comboBox2
            // 
            comboBox2.BackColor = Color.White;
            comboBox2.Font = new Font("Arial", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(743, 455);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(226, 40);
            comboBox2.TabIndex = 1;
            // 
            // button1
            // 
            button1.BackgroundImage = (Image)resources.GetObject("button1.BackgroundImage");
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = SystemColors.ControlLight;
            button1.Location = new Point(666, 881);
            button1.Name = "button1";
            button1.Size = new Size(112, 54);
            button1.TabIndex = 2;
            button1.UseVisualStyleBackColor = true;
            // 
            // btn_Back
            // 
            btn_Back.FlatAppearance.BorderSize = 0;
            btn_Back.FlatStyle = FlatStyle.Flat;
            btn_Back.Font = new Font("Arial", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_Back.ForeColor = Color.FromArgb(192, 64, 0);
            btn_Back.Location = new Point(50, 933);
            btn_Back.Name = "btn_Back";
            btn_Back.Size = new Size(140, 64);
            btn_Back.TabIndex = 3;
            btn_Back.Text = "Back";
            btn_Back.UseVisualStyleBackColor = true;
            // 
            // Match_Setting_Comp
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1438, 1024);
            Controls.Add(btn_Back);
            Controls.Add(button1);
            Controls.Add(comboBox2);
            Controls.Add(comboBox1);
            Name = "Match_Setting_Comp";
            Text = "Match_Setting";
            ResumeLayout(false);
        }

        #endregion

        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private Button button1;
        private Button btn_Back;
    }
}