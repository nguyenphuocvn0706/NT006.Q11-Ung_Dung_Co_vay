using System;
using System.Windows.Forms;

namespace Co_Vay
{
    public partial class Form_Win : Form
    {
        private readonly System.Windows.Forms.Timer _autoCloseTimer = new System.Windows.Forms.Timer();

        public Form_Win()
        {
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            MinimizeBox = false;

            // Make textboxes read-only (display only)
            txt_Black_Score.ReadOnly = true;
            txt_White_Score.ReadOnly = true;
            txt_Black_Score.TabStop = false;
            txt_White_Score.TabStop = false;

            _autoCloseTimer.Interval = 4000;
            _autoCloseTimer.Tick += (s, e) =>
            {
                _autoCloseTimer.Stop();
                Close();
            };
            Shown += (s, e) => _autoCloseTimer.Start();
            FormClosed += (s, e) => _autoCloseTimer.Stop();
        }

        public void SetScores(string blackText, string whiteText)
        {
            txt_Black_Score.Text = blackText ?? "";
            txt_White_Score.Text = whiteText ?? "";
        }
    }
}
