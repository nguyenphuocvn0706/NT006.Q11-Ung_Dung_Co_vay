namespace Co_Vay
{
    public partial class Trang_Chu : Form
    {
        public Trang_Chu()
        {
            InitializeComponent();
        }

        private void btn_DangNhap1_Click(object sender, EventArgs e)
        {
            Dang_Nhap formDangNhap = new Dang_Nhap(this);
            formDangNhap.Show();
            this.Hide();
        }

        private void btn_DangKy1_Click(object sender, EventArgs e)
        {
            Dang_Ky formDangKy = new Dang_Ky(this);
            formDangKy.Show();
            this.Hide(); ;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
