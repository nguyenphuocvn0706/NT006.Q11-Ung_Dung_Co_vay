namespace Co_Vay
{
    partial class Profile
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            txt_Name = new TextBox();
            txt_Sex = new ComboBox();
            txt_Date = new DateTimePicker();
            btn_Chang_Password = new Button();
            btn_Chang_Email = new Button();
            btn_Change_Info = new Button();
            btn_Delete_Info = new Button();
            btn_Back = new Button();
            btn_Log_Out = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Tran_Ca_Nhan;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1521, 1004);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // txt_Name
            // 
            txt_Name.Font = new Font("Times New Roman", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_Name.Location = new Point(786, 128);
            txt_Name.Multiline = true;
            txt_Name.Name = "txt_Name";
            txt_Name.Size = new Size(614, 56);
            txt_Name.TabIndex = 1;
            // 
            // txt_Sex
            // 
            txt_Sex.DropDownStyle = ComboBoxStyle.DropDownList;
            txt_Sex.Font = new Font("Times New Roman", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_Sex.Items.AddRange(new object[] { "Male", "Female", "Other" });
            txt_Sex.Location = new Point(786, 271);
            txt_Sex.Name = "txt_Sex";
            txt_Sex.Size = new Size(614, 41);
            txt_Sex.TabIndex = 2;
            txt_Sex.SelectedIndexChanged += txt_Sex_SelectedIndexChanged;
            // 
            // txt_Date
            // 
            txt_Date.CustomFormat = "dd/MM/yyyy";
            txt_Date.Font = new Font("Times New Roman", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_Date.Format = DateTimePickerFormat.Custom;
            txt_Date.Location = new Point(786, 404);
            txt_Date.Name = "txt_Date";
            txt_Date.Size = new Size(614, 40);
            txt_Date.TabIndex = 3;
            // 
            // btn_Chang_Password
            // 
            btn_Chang_Password.FlatAppearance.BorderSize = 0;
            btn_Chang_Password.FlatStyle = FlatStyle.Flat;
            btn_Chang_Password.Image = Properties.Resources.Screenshot_2025_11_12_184112;
            btn_Chang_Password.Location = new Point(359, 539);
            btn_Chang_Password.Name = "btn_Chang_Password";
            btn_Chang_Password.Size = new Size(576, 196);
            btn_Chang_Password.TabIndex = 4;
            btn_Chang_Password.UseVisualStyleBackColor = true;
            btn_Chang_Password.Click += btn_Chang_Password_Click;
            // 
            // btn_Chang_Email
            // 
            btn_Chang_Email.FlatAppearance.BorderSize = 0;
            btn_Chang_Email.FlatStyle = FlatStyle.Flat;
            btn_Chang_Email.Image = Properties.Resources.Screenshot_2025_11_12_184249;
            btn_Chang_Email.Location = new Point(367, 746);
            btn_Chang_Email.Name = "btn_Chang_Email";
            btn_Chang_Email.Size = new Size(568, 194);
            btn_Chang_Email.TabIndex = 5;
            btn_Chang_Email.UseVisualStyleBackColor = true;
            btn_Chang_Email.Click += btn_Chang_Email_Click;
            // 
            // btn_Change_Info
            // 
            btn_Change_Info.FlatAppearance.BorderSize = 0;
            btn_Change_Info.FlatStyle = FlatStyle.Flat;
            btn_Change_Info.Image = Properties.Resources.Screenshot_2025_11_12_184333;
            btn_Change_Info.Location = new Point(940, 542);
            btn_Change_Info.Name = "btn_Change_Info";
            btn_Change_Info.Size = new Size(568, 194);
            btn_Change_Info.TabIndex = 6;
            btn_Change_Info.UseVisualStyleBackColor = true;
            btn_Change_Info.Click += btn_Change_Info_Click;
            // 
            // btn_Delete_Info
            // 
            btn_Delete_Info.FlatAppearance.BorderSize = 0;
            btn_Delete_Info.FlatStyle = FlatStyle.Flat;
            btn_Delete_Info.Image = Properties.Resources.Screenshot_2025_11_12_184357;
            btn_Delete_Info.Location = new Point(941, 743);
            btn_Delete_Info.Name = "btn_Delete_Info";
            btn_Delete_Info.Size = new Size(568, 194);
            btn_Delete_Info.TabIndex = 7;
            btn_Delete_Info.UseVisualStyleBackColor = true;
            btn_Delete_Info.Click += btn_Delete_Info_Click;
            // 
            // btn_Back
            // 
            btn_Back.FlatAppearance.BorderSize = 0;
            btn_Back.FlatStyle = FlatStyle.Flat;
            btn_Back.Image = Properties.Resources.Screenshot_2025_11_12_184900;
            btn_Back.Location = new Point(30, 805);
            btn_Back.Name = "btn_Back";
            btn_Back.Size = new Size(254, 183);
            btn_Back.TabIndex = 8;
            btn_Back.UseVisualStyleBackColor = true;
            btn_Back.Click += btn_Back_Click;
            // 
            // btn_Log_Out
            // 
            btn_Log_Out.FlatAppearance.BorderSize = 0;
            btn_Log_Out.FlatStyle = FlatStyle.Flat;
            btn_Log_Out.Font = new Font("Times New Roman", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_Log_Out.ForeColor = SystemColors.ControlLightLight;
            btn_Log_Out.Image = Properties.Resources.Desktop___2__4_;
            btn_Log_Out.Location = new Point(107, 425);
            btn_Log_Out.Name = "btn_Log_Out";
            btn_Log_Out.Size = new Size(253, 86);
            btn_Log_Out.TabIndex = 9;
            btn_Log_Out.Text = "LOG OUT";
            btn_Log_Out.UseVisualStyleBackColor = true;
            btn_Log_Out.Click += btn_Log_Out_Click;
            // 
            // Profile
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1521, 1000);
            Controls.Add(btn_Log_Out);
            Controls.Add(btn_Back);
            Controls.Add(btn_Delete_Info);
            Controls.Add(btn_Change_Info);
            Controls.Add(btn_Chang_Email);
            Controls.Add(btn_Chang_Password);
            Controls.Add(txt_Date);
            Controls.Add(txt_Sex);
            Controls.Add(txt_Name);
            Controls.Add(pictureBox1);
            Name = "Profile";
            Text = "Profile";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private TextBox txt_Name;
        private ComboBox txt_Sex;
        private DateTimePicker txt_Date;
        private Button btn_Chang_Password;
        private Button btn_Chang_Email;
        private Button btn_Change_Info;
        private Button btn_Delete_Info;
        private Button btn_Back;
        private Button btn_Log_Out;
    }
}
