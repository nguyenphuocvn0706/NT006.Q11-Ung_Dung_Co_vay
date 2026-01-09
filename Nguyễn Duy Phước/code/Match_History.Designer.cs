namespace Co_Vay
{
    partial class Match_History
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvMatches;
        private System.Windows.Forms.Button btnBack;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            dgvMatches = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            btnBack = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvMatches).BeginInit();
            SuspendLayout();
            // 
            // dgvMatches
            // 
            dgvMatches.AllowUserToAddRows = false;
            dgvMatches.AllowUserToResizeRows = false;
            dgvMatches.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMatches.BackgroundColor = Color.FromArgb(255, 245, 230);
            dgvMatches.BorderStyle = BorderStyle.None;
            dgvMatches.ColumnHeadersHeight = 34;
            dgvMatches.ColumnHeadersVisible = false;
            dgvMatches.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3 });
            dgvMatches.Location = new Point(296, 328);
            dgvMatches.Name = "dgvMatches";
            dgvMatches.ReadOnly = true;
            dgvMatches.RowHeadersVisible = false;
            dgvMatches.RowHeadersWidth = 62;
            dgvMatches.RowTemplate.Height = 40;
            dgvMatches.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMatches.Size = new Size(832, 420);
            dgvMatches.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewTextBoxColumn1.FillWeight = 20F;
            dataGridViewTextBoxColumn1.MinimumWidth = 8;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewTextBoxColumn2.FillWeight = 55F;
            dataGridViewTextBoxColumn2.MinimumWidth = 8;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewTextBoxColumn3.FillWeight = 25F;
            dataGridViewTextBoxColumn3.MinimumWidth = 8;
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // btnBack
            // 
            btnBack.BackgroundImage = Properties.Resources.BACK_RANKS;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnBack.Location = new Point(479, 791);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(435, 143);
            btnBack.TabIndex = 0;
            btnBack.Click += btnBack_Click;
            // 
            // Match_History
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Matchhis;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1438, 1024);
            Controls.Add(btnBack);
            Controls.Add(dgvMatches);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Match_History";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Match History";
            Load += Match_History_Load;
            ((System.ComponentModel.ISupportInitialize)dgvMatches).EndInit();
            ResumeLayout(false);
        }

        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    }
}
