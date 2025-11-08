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
    public partial class Dang_Nhap : Form
    {
        private Trang_Chu trangChuForm;
        public Dang_Nhap(Trang_Chu formTrangChu)
        {
            InitializeComponent();
            trangChuForm = formTrangChu;
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            this.Close();
            trangChuForm.Show();
        }

        private async void btn_DangNhap2_Click(object sender, EventArgs e)
        {
            string username = txb_Username.Text.Trim();
            string password = txb_Password.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu!");
                return;
            }

            try
            {
                // B1: Lấy email theo username từ Realtime Database
                var db = new RealtimeDatabaseService();
                string email = await db.GetEmailByUsernameAsync(username);

                if (string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Tên đăng nhập không tồn tại!");
                    return;
                }

                // B2: Đăng nhập Firebase
                var firebase = new FirebaseService();
                var auth = await firebase.LoginAsync(email, password);

                MessageBox.Show("Đăng nhập thành công!");

                // B3: Mở form chính
                Man_Hinh_Chinh mainForm = new Man_Hinh_Chinh();
                mainForm.Show();

                // Đóng form đăng nhập
                this.Hide();
                this.Dispose();
            }
            catch (Firebase.Auth.FirebaseAuthException)
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng nhập: " + ex.Message);
            }
        }

        private void btn_Quen_mat_khau_Click(object sender, EventArgs e)
        {
            Quen_Mat_Khau f= new Quen_Mat_Khau(this);
            f.Show();
        }
    }
}
