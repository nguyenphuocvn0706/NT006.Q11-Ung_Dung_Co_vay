using System;
using System.Windows.Forms;

namespace Co_Vay
{
    public partial class Form_Result_Player : Form
    {
        private System.Windows.Forms.Timer? _autoCloseTimer;

        public Form_Result_Player()
        {
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            // Chỉ hiển thị kết quả; 2 ô điểm không dùng theo yêu cầu => để trống (hoặc bạn muốn "N/A" thì đổi ở đây)
            txt_Black_Score.ReadOnly = true;
            txt_White_Score.ReadOnly = true;
            txt_Result.ReadOnly = true;

            txt_Black_Score.TabStop = false;
            txt_White_Score.TabStop = false;
            txt_Result.TabStop = false;

            txt_Black_Score.Text = "";
            txt_White_Score.Text = "";

            _autoCloseTimer = new System.Windows.Forms.Timer();
            _autoCloseTimer.Interval = 4000;
            _autoCloseTimer.Tick += (s, e) =>
            {
                _autoCloseTimer?.Stop();
                if (!IsDisposed) Close();
            };

            Shown += (s, e) => _autoCloseTimer?.Start();

            FormClosed += (s, e) =>
            {
                if (_autoCloseTimer != null)
                {
                    _autoCloseTimer.Stop();
                    _autoCloseTimer.Dispose();
                    _autoCloseTimer = null;
                }
            };
        }

        // Theo yêu cầu: chỉ set txt_Result = "BLACK WON" hoặc "WHITE WON"
        public void SetResult(string resultText)
        {
            txt_Result.Text = resultText ?? "";
        }
        public void SetScores(string blackText, string whiteText)
        {
            txt_Black_Score.Text = blackText ?? "";
            txt_White_Score.Text = whiteText ?? "";
        }

    }
}
