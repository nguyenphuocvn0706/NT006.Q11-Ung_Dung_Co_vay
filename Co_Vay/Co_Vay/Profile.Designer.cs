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
            txt_Name = new TextBox();
            txt_Sex = new ComboBox();
            txt_Date = new DateTimePicker();
            btn_Chang_Password = new Button();
            btn_Chang_Email = new Button();
            btn_Change_Info = new Button();
            btn_Delete_Info = new Button();
            btn_Back = new Button();
            SuspendLayout();
            // 
            // txt_Name
            // 
            txt_Name.Font = new Font("Arial", 14F);
            txt_Name.Location = new Point(311, 510);
            txt_Name.Multiline = true;
            txt_Name.Name = "txt_Name";
            txt_Name.Size = new Size(297, 44);
            txt_Name.TabIndex = 1;
            // 
            // txt_Sex
            // 
            txt_Sex.DropDownStyle = ComboBoxStyle.DropDownList;
            txt_Sex.Font = new Font("Arial", 14F);
            txt_Sex.Items.AddRange(new object[] { "Male", "Female", "Other" });
            txt_Sex.Location = new Point(311, 608);
            txt_Sex.Name = "txt_Sex";
            txt_Sex.Size = new Size(297, 40);
            txt_Sex.TabIndex = 2;
            // 
            // txt_Date
            // 
            txt_Date.CustomFormat = "dd/MM/yyyy";
            txt_Date.Font = new Font("Arial", 14F);
            txt_Date.Format = DateTimePickerFormat.Custom;
            txt_Date.Location = new Point(311, 711);
            txt_Date.Name = "txt_Date";
            txt_Date.Size = new Size(297, 40);
            txt_Date.TabIndex = 3;
            // 
            // btn_Chang_Password
            // 
            btn_Chang_Password.BackColor = Color.FromArgb(255, 156, 124);
            btn_Chang_Password.FlatAppearance.BorderSize = 0;
            btn_Chang_Password.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 156, 124);
            btn_Chang_Password.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 156, 124);
            btn_Chang_Password.FlatStyle = FlatStyle.Flat;
            btn_Chang_Password.Font = new Font("Arial", 35F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_Chang_Password.ForeColor = Color.FromArgb(255, 255, 255);
            btn_Chang_Password.Location = new Point(656, 192);
            btn_Chang_Password.Name = "btn_Chang_Password";
            btn_Chang_Password.Size = new Size(480, 170);
            btn_Chang_Password.TabIndex = 4;
            btn_Chang_Password.Text = "CHANG PASSWORD";
            btn_Chang_Password.UseVisualStyleBackColor = false;
            btn_Chang_Password.Click += btn_Chang_Password_Click;
            // 
            // btn_Chang_Email
            // 
            btn_Chang_Email.BackColor = Color.FromArgb(255, 156, 124);
            btn_Chang_Email.FlatAppearance.BorderSize = 0;
            btn_Chang_Email.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 156, 124);
            btn_Chang_Email.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 156, 124);
            btn_Chang_Email.FlatStyle = FlatStyle.Flat;
            btn_Chang_Email.Font = new Font("Arial", 25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_Chang_Email.ForeColor = Color.FromArgb(255, 255, 255);
            btn_Chang_Email.Location = new Point(656, 417);
            btn_Chang_Email.Name = "btn_Chang_Email";
            btn_Chang_Email.Size = new Size(476, 107);
            btn_Chang_Email.TabIndex = 5;
            btn_Chang_Email.Text = "CHANGE EMAIL";
            btn_Chang_Email.UseVisualStyleBackColor = true;
            btn_Chang_Email.Click += btn_Chang_Email_Click;
            // 
            // btn_Change_Info
            // 
            btn_Change_Info.BackColor = Color.FromArgb(255, 198, 162);
            btn_Change_Info.FlatAppearance.BorderSize = 0;
            btn_Change_Info.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 198, 162);
            btn_Change_Info.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 198, 162);
            btn_Change_Info.FlatStyle = FlatStyle.Flat;
            btn_Change_Info.Font = new Font("Arial", 25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_Change_Info.ForeColor = Color.FromArgb(255, 255, 255);
            btn_Change_Info.Location = new Point(656, 581);
            btn_Change_Info.Name = "btn_Change_Info";
            btn_Change_Info.Size = new Size(476, 105);
            btn_Change_Info.TabIndex = 6;
            btn_Change_Info.Text = "CHANGE INFO";
            btn_Change_Info.UseVisualStyleBackColor = false;
            btn_Change_Info.Click += btn_Change_Info_Click;
            // 
            // btn_Delete_Info
            // 
            btn_Delete_Info.BackColor = Color.FromArgb(254, 238, 238);
            btn_Delete_Info.FlatAppearance.BorderSize = 0;
            btn_Delete_Info.FlatAppearance.MouseDownBackColor = Color.FromArgb(254, 238, 238);
            btn_Delete_Info.FlatAppearance.MouseOverBackColor = Color.FromArgb(254, 238, 238);
            btn_Delete_Info.FlatStyle = FlatStyle.Flat;
            btn_Delete_Info.Font = new Font("Arial", 25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_Delete_Info.ForeColor = Color.FromArgb(244, 134, 105);
            btn_Delete_Info.Location = new Point(648, 728);
            btn_Delete_Info.Name = "btn_Delete_Info";
            btn_Delete_Info.Size = new Size(488, 111);
            btn_Delete_Info.TabIndex = 7;
            btn_Delete_Info.Text = "DELETE INFO";
            btn_Delete_Info.UseVisualStyleBackColor = false;
            btn_Delete_Info.Click += btn_Delete_Info_Click;
            // 
            // btn_Back
            // 
            btn_Back.FlatAppearance.BorderSize = 0;
            btn_Back.Font = new Font("Arial", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_Back.Location = new Point(24, 934);
            btn_Back.Name = "btn_Back";
            btn_Back.Size = new Size(180, 56);
            btn_Back.TabIndex = 8;
            btn_Back.Text = "BACK";
            btn_Back.UseVisualStyleBackColor = true;
            btn_Back.Click += btn_Back_Click;
            // 
            // Profile
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Profile_Main;
            ClientSize = new Size(1438, 1024);
            Controls.Add(btn_Back);
            Controls.Add(btn_Delete_Info);
            Controls.Add(btn_Change_Info);
            Controls.Add(btn_Chang_Email);
            Controls.Add(btn_Chang_Password);
            Controls.Add(txt_Date);
            Controls.Add(txt_Sex);
            Controls.Add(txt_Name);
            Name = "Profile";
            Text = "Profile";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txt_Name;
        private ComboBox txt_Sex;
        private DateTimePicker txt_Date;
        private Button btn_Chang_Password;
        private Button btn_Chang_Email;
        private Button btn_Change_Info;
        private Button btn_Delete_Info;
        private Button btn_Back;
    }
}
