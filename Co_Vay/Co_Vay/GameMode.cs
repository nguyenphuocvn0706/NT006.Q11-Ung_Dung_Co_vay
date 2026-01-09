using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firebase.Auth;

namespace Co_Vay
{
    public partial class GameMode : Form
    {
        private Man_Hinh_Chinh mainForm;
        public GameMode(Man_Hinh_Chinh mainForm)
        {
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.mainForm = mainForm;
        }

        private void btn_GM_Back_Click(object sender, EventArgs e)
        {
            mainForm.Show();  // hiện lại màn hình chính
            this.Close();
        }

        private void btn_W_Comp_Click(object sender, EventArgs e)
        {
            // Player local (máy)
            PlayerInfo player = new PlayerInfo
            {
                Id = mainForm.UserId,
                Name = "Player"
            };

            Match_Setting_Comp comp =
                new Match_Setting_Comp(mainForm, player, mainForm.IdToken);

            comp.StartPosition = FormStartPosition.CenterScreen;
            comp.Show();

            this.Close();
        }

        private void btn_W_Player_Click(object sender, EventArgs e)
        {
            Match_Setting_Player player = new Match_Setting_Player();
            player.Show();
            this.Close();
        }

        private async void btn_Online_Click(object sender, EventArgs e)
        {
            var firebase = new FirebaseService();
            var realtimeDb = new RealtimeDatabaseService();

            // 2. Lấy UID user đang đăng nhập từ FirebaseAuthClient
            var authClient = firebase.AuthClient;
            if (authClient == null || authClient.User == null)
            {
                return;
            }

            string uid = authClient.User.Uid;

            // 3. Lấy thông tin user từ Realtime DB (/users/{uid})
            var user = await realtimeDb.GetUserAsync(uid);
            if (user == null)
            {
                MessageBox.Show("Không lấy được thông tin người chơi từ Firebase.");
                return;
            }

            // 4. Tạo PlayerInfo THẬT (KHÔNG phải Guest)
            var player = new PlayerInfo
            {
                Id = uid,
                Name = user.username   // dùng username trong node /users
            };

            // 5. Mở Game_Mode_Online với PlayerInfo
            var online = new Game_Mode_Online(player);
            online.Show();

            this.Close();
        }
    }
}
