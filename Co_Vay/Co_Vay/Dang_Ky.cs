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
    public partial class Dang_Ky : Form
    {
        private Trang_Chu trangChuForm;
        public Dang_Ky(Trang_Chu formTrangChu)
        {
            InitializeComponent();
            trangChuForm = formTrangChu;
        }

        private async void btn_DangKy_Click(object sender, EventArgs e)
        {
            string username = txb_Username.Text.Trim();
            string email = txt_Email.Text.Trim();
            string password = txb_Password1.Text.Trim();
            string confirm = txb_Password2.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            if (password != confirm)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!");
                return;
            }

            try
            {
                var firebase = new FirebaseService();
                var auth = await firebase.RegisterAsync(email, password);
                var user = auth.User;

                string uid = user.Uid;
                string token = await user.GetIdTokenAsync();

                var db = new RealtimeDatabaseService(token);
                await db.SetUserAsync(uid, new UserModel
                {
                    username = username,
                    email = email
                });

                await db.UpdateUsernameMappingAsync(username, email);

                MessageBox.Show("Đăng ký thành công!");
                this.Close();
                trangChuForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng ký: " + ex.Message);
            }
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            this.Close();
            trangChuForm.Show();
        }
    }
}
