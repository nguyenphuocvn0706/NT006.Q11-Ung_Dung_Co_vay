using System;
using System.Windows.Forms;

namespace Co_Vay
{
    public partial class Form_Draw : Form
    {
        private readonly System.Windows.Forms.Timer _autoCloseTimer = new System.Windows.Forms.Timer();

        public Form_Draw()
        {
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            // UI behavior
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            MinimizeBox = false;

            // Auto close after 4 seconds
            _autoCloseTimer.Interval = 4000;
            _autoCloseTimer.Tick += (s, e) =>
            {
                _autoCloseTimer.Stop();
                Close();
            };
            Shown += (s, e) => _autoCloseTimer.Start();
            FormClosed += (s, e) => _autoCloseTimer.Stop();
        }
    }
}
