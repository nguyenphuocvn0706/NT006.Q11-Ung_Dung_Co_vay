namespace Co_Vay
{
    partial class Mode_W_Player
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mode_W_Player));
            btn_Quit = new Button();
            btn_Skip = new Button();
            btn_End = new Button();
            lbl_Time_Left = new Label();
            lbl_Time_Right = new Label();
            txt_Name_Left = new TextBox();
            txt_Name_Right = new TextBox();
            pl_Board = new Panel();
            SuspendLayout();
            // 
            // btn_Quit
            // 
            btn_Quit.BackgroundImage = (Image)resources.GetObject("btn_Quit.BackgroundImage");
            btn_Quit.FlatAppearance.BorderSize = 0;
            btn_Quit.FlatStyle = FlatStyle.Flat;
            btn_Quit.Location = new Point(88, 440);
            btn_Quit.Name = "btn_Quit";
            btn_Quit.Size = new Size(221, 63);
            btn_Quit.TabIndex = 0;
            btn_Quit.UseVisualStyleBackColor = true;
            btn_Quit.Click += btn_Quit_Click;
            // 
            // btn_Skip
            // 
            btn_Skip.BackgroundImage = (Image)resources.GetObject("btn_Skip.BackgroundImage");
            btn_Skip.FlatAppearance.BorderSize = 0;
            btn_Skip.FlatStyle = FlatStyle.Flat;
            btn_Skip.Location = new Point(88, 588);
            btn_Skip.Name = "btn_Skip";
            btn_Skip.Size = new Size(207, 63);
            btn_Skip.TabIndex = 1;
            btn_Skip.UseVisualStyleBackColor = true;
            btn_Skip.Click += btn_Skip_Click;
            // 
            // btn_End
            // 
            btn_End.BackgroundImage = (Image)resources.GetObject("btn_End.BackgroundImage");
            btn_End.FlatAppearance.BorderSize = 0;
            btn_End.FlatStyle = FlatStyle.Flat;
            btn_End.Location = new Point(88, 755);
            btn_End.Name = "btn_End";
            btn_End.Size = new Size(221, 63);
            btn_End.TabIndex = 2;
            btn_End.UseVisualStyleBackColor = true;
            btn_End.Click += btn_End_Click;
            // 
            // lbl_Time_Left
            // 
            lbl_Time_Left.AutoSize = true;
            lbl_Time_Left.BackColor = Color.FromArgb(255, 202, 184);
            lbl_Time_Left.FlatStyle = FlatStyle.Flat;
            lbl_Time_Left.Font = new Font("Consolas", 20F, FontStyle.Bold);
            lbl_Time_Left.ForeColor = Color.FromArgb(125, 37, 36);
            lbl_Time_Left.Location = new Point(79, 307);
            lbl_Time_Left.Name = "lbl_Time_Left";
            lbl_Time_Left.Size = new Size(196, 47);
            lbl_Time_Left.TabIndex = 3;
            lbl_Time_Left.Text = "00:00:00";
            // 
            // lbl_Time_Right
            // 
            lbl_Time_Right.AutoSize = true;
            lbl_Time_Right.BackColor = Color.FromArgb(252, 214, 166);
            lbl_Time_Right.FlatStyle = FlatStyle.Flat;
            lbl_Time_Right.Font = new Font("Consolas", 20F, FontStyle.Bold);
            lbl_Time_Right.ForeColor = Color.FromArgb(125, 37, 36);
            lbl_Time_Right.Location = new Point(1163, 307);
            lbl_Time_Right.Name = "lbl_Time_Right";
            lbl_Time_Right.Size = new Size(196, 47);
            lbl_Time_Right.TabIndex = 4;
            lbl_Time_Right.Text = "00:00:00";
            // 
            // txt_Name_Left
            // 
            txt_Name_Left.BackColor = Color.FromArgb(241, 173, 148);
            txt_Name_Left.BorderStyle = BorderStyle.None;
            txt_Name_Left.Font = new Font("Arial", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_Name_Left.ForeColor = Color.FromArgb(125, 37, 36);
            txt_Name_Left.Location = new Point(88, 235);
            txt_Name_Left.Name = "txt_Name_Left";
            txt_Name_Left.Size = new Size(169, 23);
            txt_Name_Left.TabIndex = 5;
            txt_Name_Left.TextAlign = HorizontalAlignment.Center;
            // 
            // txt_Name_Right
            // 
            txt_Name_Right.BackColor = Color.FromArgb(243, 174, 101);
            txt_Name_Right.BorderStyle = BorderStyle.None;
            txt_Name_Right.Font = new Font("Arial", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_Name_Right.ForeColor = Color.FromArgb(125, 37, 36);
            txt_Name_Right.Location = new Point(1172, 235);
            txt_Name_Right.Name = "txt_Name_Right";
            txt_Name_Right.Size = new Size(169, 23);
            txt_Name_Right.TabIndex = 7;
            txt_Name_Right.TextAlign = HorizontalAlignment.Center;
            // 
            // pl_Board
            // 
            pl_Board.BackColor = Color.FromArgb(255, 207, 145);
            pl_Board.BackgroundImageLayout = ImageLayout.None;
            pl_Board.Location = new Point(398, 164);
            pl_Board.Name = "pl_Board";
            pl_Board.Size = new Size(662, 663);
            pl_Board.TabIndex = 20;
            pl_Board.Visible = false;
            // 
            // Mode_W_Player
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1438, 1024);
            Controls.Add(btn_Skip);
            Controls.Add(pl_Board);
            Controls.Add(txt_Name_Right);
            Controls.Add(txt_Name_Left);
            Controls.Add(lbl_Time_Right);
            Controls.Add(lbl_Time_Left);
            Controls.Add(btn_End);
            Controls.Add(btn_Quit);
            Name = "Mode_W_Player";
            Text = "Mode_W_Player";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_Quit;
        private Button btn_Skip;
        private Button btn_End;
        private Label lbl_Time_Left;
        private Label lbl_Time_Right;
        private TextBox txt_Name_Left;
        private TextBox txt_Name_Right;
        private Panel pl_Board;
    }
}