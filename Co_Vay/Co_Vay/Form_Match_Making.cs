using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Co_Vay
{
    public partial class Form_Match_Making : Form
    {
        // Event báo về Game_Mode_Online
        public event Action CancelMatchMaking;

        public Form_Match_Making()
        {
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            // Bắt sự kiện bấm nút X
            this.FormClosing += Form_Match_Making_FormClosing;
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            CancelMatchMaking?.Invoke();
            this.Close();
        }

        private void Form_Match_Making_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Nếu đóng bằng nút X thì cũng coi như Cancel
            CancelMatchMaking?.Invoke();
        }
    }
}

