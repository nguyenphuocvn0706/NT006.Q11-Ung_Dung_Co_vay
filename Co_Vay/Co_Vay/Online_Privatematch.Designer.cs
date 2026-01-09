using System.Drawing;
using System.Windows.Forms;

namespace Co_Vay
{
    partial class Online_Privatematch
    {
        private System.ComponentModel.IContainer components = null;
        private ComboBox cbo_Time;
        private ComboBox cbo_Rule;
        private Button btn_CreateRoom;
        private TextBox txt_YourRoomCode;
        private TextBox txt_JoinCode;
        private Button btn_JoinRoom;

        private Button btn_Back;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Online_Privatematch));
            cbo_Time = new ComboBox();
            cbo_Rule = new ComboBox();
            btn_CreateRoom = new Button();
            txt_YourRoomCode = new TextBox();
            txt_JoinCode = new TextBox();
            btn_JoinRoom = new Button();
            btn_Back = new Button();
            SuspendLayout();
            // 
            // cbo_Time
            // 
            cbo_Time.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo_Time.FlatStyle = FlatStyle.Flat;
            cbo_Time.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbo_Time.Items.AddRange(new object[] { "15 minutes", "30 minutes", "1 hour", "2 hours", "3 hours" });
            cbo_Time.Location = new Point(812, 221);
            cbo_Time.Name = "cbo_Time";
            cbo_Time.Size = new Size(200, 40);
            cbo_Time.TabIndex = 1;
            // 
            // cbo_Rule
            // 
            cbo_Rule.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo_Rule.FlatStyle = FlatStyle.Flat;
            cbo_Rule.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbo_Rule.Items.AddRange(new object[] { "Japanese", "Chinese" });
            cbo_Rule.Location = new Point(812, 323);
            cbo_Rule.Name = "cbo_Rule";
            cbo_Rule.Size = new Size(200, 40);
            cbo_Rule.TabIndex = 3;
            // 
            // btn_CreateRoom
            // 
            btn_CreateRoom.BackgroundImage = (Image)resources.GetObject("btn_CreateRoom.BackgroundImage");
            btn_CreateRoom.FlatAppearance.BorderSize = 0;
            btn_CreateRoom.FlatStyle = FlatStyle.Flat;
            btn_CreateRoom.Location = new Point(630, 412);
            btn_CreateRoom.Name = "btn_CreateRoom";
            btn_CreateRoom.Size = new Size(180, 64);
            btn_CreateRoom.TabIndex = 4;
            btn_CreateRoom.Click += btn_CreateRoom_Click;
            // 
            // txt_YourRoomCode
            // 
            txt_YourRoomCode.BackColor = SystemColors.Window;
            txt_YourRoomCode.BorderStyle = BorderStyle.None;
            txt_YourRoomCode.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_YourRoomCode.Location = new Point(812, 529);
            txt_YourRoomCode.Name = "txt_YourRoomCode";
            txt_YourRoomCode.ReadOnly = true;
            txt_YourRoomCode.Size = new Size(200, 32);
            txt_YourRoomCode.TabIndex = 6;
            txt_YourRoomCode.TextAlign = HorizontalAlignment.Center;
            // 
            // txt_JoinCode
            // 
            txt_JoinCode.BorderStyle = BorderStyle.None;
            txt_JoinCode.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_JoinCode.Location = new Point(812, 804);
            txt_JoinCode.Name = "txt_JoinCode";
            txt_JoinCode.Size = new Size(200, 32);
            txt_JoinCode.TabIndex = 1;
            txt_JoinCode.TextAlign = HorizontalAlignment.Center;
            // 
            // btn_JoinRoom
            // 
            btn_JoinRoom.BackgroundImage = (Image)resources.GetObject("btn_JoinRoom.BackgroundImage");
            btn_JoinRoom.FlatAppearance.BorderSize = 0;
            btn_JoinRoom.FlatStyle = FlatStyle.Flat;
            btn_JoinRoom.Location = new Point(633, 887);
            btn_JoinRoom.Name = "btn_JoinRoom";
            btn_JoinRoom.Size = new Size(180, 64);
            btn_JoinRoom.TabIndex = 2;
            btn_JoinRoom.Click += btn_JoinRoom_Click;
            // 
            // btn_Back
            // 
            btn_Back.BackgroundImage = (Image)resources.GetObject("btn_Back.BackgroundImage");
            btn_Back.FlatAppearance.BorderSize = 0;
            btn_Back.FlatStyle = FlatStyle.Flat;
            btn_Back.Location = new Point(59, 933);
            btn_Back.Name = "btn_Back";
            btn_Back.Size = new Size(170, 65);
            btn_Back.TabIndex = 2;
            btn_Back.Click += btn_Back_Click;
            // 
            // Online_Privatematch
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1438, 1024);
            Controls.Add(cbo_Time);
            Controls.Add(txt_JoinCode);
            Controls.Add(btn_JoinRoom);
            Controls.Add(cbo_Rule);
            Controls.Add(btn_Back);
            Controls.Add(btn_CreateRoom);
            Controls.Add(txt_YourRoomCode);
            Name = "Online_Privatematch";
            Text = "Private Match Online";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
