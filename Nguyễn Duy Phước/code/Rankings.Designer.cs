namespace Co_Vay
{
    partial class Rankings
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pnl_Container;
        private FlowLayoutPanel pnl_List;
        private Button btn_Back;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnl_Container = new Panel();
            pnl_List = new FlowLayoutPanel();
            btn_Back = new Button();
            pnl_Container.SuspendLayout();
            SuspendLayout();
            // 
            // pnl_Container
            // 
            pnl_Container.BackColor = Color.Transparent;
            pnl_Container.Controls.Add(pnl_List);
            pnl_Container.Location = new Point(270, 200);
            pnl_Container.Name = "pnl_Container";
            pnl_Container.Size = new Size(900, 600);
            pnl_Container.TabIndex = 0;
            // 
            // pnl_List
            // 
            pnl_List.AutoScroll = true;
            pnl_List.BackColor = Color.FromArgb(240, 255, 235, 215);
            pnl_List.FlowDirection = FlowDirection.TopDown;
            pnl_List.Location = new Point(77, 119);
            pnl_List.Name = "pnl_List";
            pnl_List.Size = new Size(726, 434);
            pnl_List.TabIndex = 0;
            pnl_List.WrapContents = false;
            // 
            // btn_Back
            // 
            btn_Back.BackColor = Color.FromArgb(255, 230, 200);
            btn_Back.BackgroundImage = Properties.Resources.BACK_RANKS;
            btn_Back.FlatAppearance.BorderSize = 0;
            btn_Back.FlatStyle = FlatStyle.Flat;
            btn_Back.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            btn_Back.Location = new Point(476, 792);
            btn_Back.Name = "btn_Back";
            btn_Back.Size = new Size(435, 136);
            btn_Back.TabIndex = 1;
            btn_Back.UseVisualStyleBackColor = false;
            btn_Back.Click += btn_Back_Click;
            // 
            // Rankings
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Ranks1;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1438, 1024);
            Controls.Add(btn_Back);
            Controls.Add(pnl_Container);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Rankings";
            StartPosition = FormStartPosition.CenterScreen;
            Load += Rankings_Load;
            pnl_Container.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
