namespace Co_Vay
{
    partial class Mode_W_Comp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mode_W_Comp));
            lbl_Time_Right = new Label();
            lbl_Time_Left = new Label();
            btn_Quit = new Button();
            btn_Undo = new Button();
            btn_End = new Button();
            txt_Name_Right = new TextBox();
            txt_Name_Left = new TextBox();
            pl_Board = new Panel();
            SuspendLayout();
            // 
            // lbl_Time_Right
            // 
            lbl_Time_Right.AutoSize = true;
            lbl_Time_Right.BackColor = Color.FromArgb(252, 214, 166);
            lbl_Time_Right.FlatStyle = FlatStyle.Flat;
            lbl_Time_Right.Font = new Font("Consolas", 20F, FontStyle.Bold);
            lbl_Time_Right.ForeColor = Color.FromArgb(125, 37, 36);
            lbl_Time_Right.Location = new Point(1163, 299);
            lbl_Time_Right.Name = "lbl_Time_Right";
            lbl_Time_Right.Size = new Size(196, 47);
            lbl_Time_Right.TabIndex = 16;
            lbl_Time_Right.Text = "00:00:00";
            // 
            // lbl_Time_Left
            // 
            lbl_Time_Left.AutoSize = true;
            lbl_Time_Left.BackColor = Color.FromArgb(255, 202, 184);
            lbl_Time_Left.FlatStyle = FlatStyle.Flat;
            lbl_Time_Left.Font = new Font("Consolas", 20F, FontStyle.Bold);
            lbl_Time_Left.ForeColor = Color.FromArgb(125, 37, 36);
            lbl_Time_Left.Location = new Point(79, 299);
            lbl_Time_Left.Name = "lbl_Time_Left";
            lbl_Time_Left.Size = new Size(196, 47);
            lbl_Time_Left.TabIndex = 15;
            lbl_Time_Left.Text = "00:00:00";
            // 
            // btn_Quit
            // 
            btn_Quit.BackgroundImage = (Image)resources.GetObject("btn_Quit.BackgroundImage");
            btn_Quit.FlatAppearance.BorderSize = 0;
            btn_Quit.FlatStyle = FlatStyle.Flat;
            btn_Quit.Location = new Point(84, 442);
            btn_Quit.Name = "btn_Quit";
            btn_Quit.Size = new Size(221, 63);
            btn_Quit.TabIndex = 14;
            btn_Quit.UseVisualStyleBackColor = true;
            btn_Quit.Click += btn_Quit_Click;
            // 
            // btn_Undo
            // 
            btn_Undo.BackgroundImage = (Image)resources.GetObject("btn_Undo.BackgroundImage");
            btn_Undo.FlatAppearance.BorderSize = 0;
            btn_Undo.FlatStyle = FlatStyle.Flat;
            btn_Undo.Location = new Point(84, 590);
            btn_Undo.Name = "btn_Undo";
            btn_Undo.Size = new Size(221, 63);
            btn_Undo.TabIndex = 17;
            btn_Undo.UseVisualStyleBackColor = true;
            btn_Undo.Click += btn_Undo_Click;
            // 
            // btn_End
            // 
            btn_End.BackgroundImage = (Image)resources.GetObject("btn_End.BackgroundImage");
            btn_End.FlatAppearance.BorderSize = 0;
            btn_End.FlatStyle = FlatStyle.Flat;
            btn_End.Location = new Point(84, 756);
            btn_End.Name = "btn_End";
            btn_End.Size = new Size(221, 63);
            btn_End.TabIndex = 18;
            btn_End.UseVisualStyleBackColor = true;
            btn_End.Click += btn_End_Click;
            // 
            // txt_Name_Right
            // 
            txt_Name_Right.BackColor = Color.FromArgb(243, 174, 101);
            txt_Name_Right.BorderStyle = BorderStyle.None;
            txt_Name_Right.Font = new Font("Arial", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_Name_Right.ForeColor = Color.FromArgb(125, 37, 36);
            txt_Name_Right.Location = new Point(1175, 236);
            txt_Name_Right.Name = "txt_Name_Right";
            txt_Name_Right.ReadOnly = true;
            txt_Name_Right.Size = new Size(169, 23);
            txt_Name_Right.TabIndex = 20;
            txt_Name_Right.Text = "Computer";
            txt_Name_Right.TextAlign = HorizontalAlignment.Center;
            // 
            // txt_Name_Left
            // 
            txt_Name_Left.BackColor = Color.FromArgb(241, 173, 148);
            txt_Name_Left.BorderStyle = BorderStyle.None;
            txt_Name_Left.Font = new Font("Arial", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_Name_Left.ForeColor = Color.FromArgb(125, 37, 36);
            txt_Name_Left.Location = new Point(93, 236);
            txt_Name_Left.Name = "txt_Name_Left";
            txt_Name_Left.ReadOnly = true;
            txt_Name_Left.Size = new Size(169, 23);
            txt_Name_Left.TabIndex = 19;
            txt_Name_Left.Text = "Player";
            txt_Name_Left.TextAlign = HorizontalAlignment.Center;
            // 
            // pl_Board
            // 
            pl_Board.BackColor = Color.FromArgb(255, 207, 145);
            pl_Board.BackgroundImageLayout = ImageLayout.None;
            pl_Board.Location = new Point(397, 164);
            pl_Board.Name = "pl_Board";
            pl_Board.Size = new Size(662, 663);
            pl_Board.TabIndex = 21;
            pl_Board.Visible = false;
            pl_Board.Paint += pl_Board_Paint;
            // 
            // Mode_W_Comp
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1438, 1024);
            Controls.Add(pl_Board);
            Controls.Add(txt_Name_Right);
            Controls.Add(txt_Name_Left);
            Controls.Add(btn_End);
            Controls.Add(btn_Undo);
            Controls.Add(lbl_Time_Right);
            Controls.Add(lbl_Time_Left);
            Controls.Add(btn_Quit);
            Name = "Mode_W_Comp";
            Text = "Mode_W_Comp";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbl_Time_Right;
        private Label lbl_Time_Left;
        private Button btn_Quit;
        private Button btn_Undo;
        private Button btn_End;
        private TextBox txt_Name_Right;
        private TextBox txt_Name_Left;
        private Panel pl_Board;
    }
}