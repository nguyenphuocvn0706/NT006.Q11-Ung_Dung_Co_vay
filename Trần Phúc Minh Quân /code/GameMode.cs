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
    public partial class GameMode : Form
    {
        private Man_Hinh_Chinh mainForm;
        public GameMode(Man_Hinh_Chinh mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btn_GM_Back_Click(object sender, EventArgs e)
        {
            mainForm.Show();  // hiện lại màn hình chính
            this.Close();
        }
    }
}
