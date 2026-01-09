using Firebase.Auth;
using Firebase.Auth.Providers;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Co_Vay
{
    internal class FirebaseService
    {
        private const string ApiKey = "AIzaSyB2hBtJx5MgJ8R4dlImA06yCjIf3l1zilE";
        private readonly FirebaseAuthClient authClient;

        // dịch vụ Realtime Database dùng chung cho game/match 
        private readonly RealtimeDatabaseService realtimeDb;

        public FirebaseAuthClient AuthClient => authClient;

        // Cho Form1 dùng nếu không quan tâm databaseUrl (giữ nguyên)
        public FirebaseService()
        {
            var config = new FirebaseAuthConfig
            {
                ApiKey = ApiKey,
                AuthDomain = "covay-54c65.firebaseapp.com",
                Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider()
                }
            };
            authClient = new FirebaseAuthClient(config);

            //  khởi tạo RealtimeDatabaseService không cần token 
            realtimeDb = new RealtimeDatabaseService();
        }
                
        public FirebaseService(string databaseUrl) : this()
        {
            
        }

        //  hàm tạo RealtimeDatabaseService có token sau khi login (tuỳ chọn dùng sau này)
        public RealtimeDatabaseService CreateRealtimeDbWithToken(string idToken)
        {
            return new RealtimeDatabaseService(idToken);
        }

        //  Đăng ký tài khoản mới
        public async Task<UserCredential> RegisterAsync(string email, string password)
        {
            return await authClient.CreateUserWithEmailAndPasswordAsync(email, password);
        }

        //  Đăng nhập
        public async Task<UserCredential> LoginAsync(string email, string password)
        {
            return await authClient.SignInWithEmailAndPasswordAsync(email, password);
        }

        //  Đặt lại mật khẩu (dùng REST API vì SDK mới không có ResetPasswordAsync)
        public async Task ResetPasswordAsync(string email)
        {
            using (var client = new HttpClient())
            {
                var body = new
                {
                    requestType = "PASSWORD_RESET",
                    email = email
                };

                string json = JsonSerializer.Serialize(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(
                    $"https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key={ApiKey}", content);

                if (!response.IsSuccessStatusCode)
                {
                    string error = await response.Content.ReadAsStringAsync();
                    throw new Exception("Không thể gửi email đặt lại mật khẩu: " + error);
                }
            }
        }

        //  Gửi email xác minh (REST API)
        public async Task SendEmailVerificationAsync(string idToken)
        {
            using (var client = new HttpClient())
            {
                var body = new
                {
                    requestType = "VERIFY_EMAIL",
                    idToken = idToken
                };

                string json = JsonSerializer.Serialize(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(
                    $"https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key={ApiKey}", content);

                if (!response.IsSuccessStatusCode)
                {
                    string error = await response.Content.ReadAsStringAsync();
                    throw new Exception("Không thể gửi email xác minh: " + error);
                }
            }
        }

        //  Làm mới token (thay thế cho RefreshUserAsync)
        public async Task<string> RefreshIdTokenAsync(string refreshToken)
        {
            using (var client = new HttpClient())
            {
                var body = new
                {
                    grant_type = "refresh_token",
                    refresh_token = refreshToken
                };

                string json = JsonSerializer.Serialize(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(
                    $"https://securetoken.googleapis.com/v1/token?key={ApiKey}", content);

                string result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    throw new Exception("Không thể làm mới token: " + result);

                // Lấy access_token mới từ response
                using (var doc = JsonDocument.Parse(result))
                {
                    return doc.RootElement.GetProperty("id_token").GetString();
                }
            }
        }

        // =====================================================================
        // ===============  CÁC HÀM BỔ SUNG CHO GAME/MATCH  ====================
        // =====================================================================

        //===lưu trạng thái bàn cờ GoGame theo gameId (MatchId) ===
        public async Task SaveGameAsync(string gameId, Game_Engine game)
        {
            if (string.IsNullOrWhiteSpace(gameId))
                throw new ArgumentException("gameId không hợp lệ", nameof(gameId));

            await realtimeDb.SetAsync($"games/{gameId}", game);
        }

        // ===đọc trạng thái bàn cờ GoGame theo gameId ===
        public async Task<Game_Engine?> LoadGameAsync(string gameId)
        {
            if (string.IsNullOrWhiteSpace(gameId))
                return null;

            return await realtimeDb.GetAsync<Game_Engine>($"games/{gameId}");
        }

        // ===xoá dữ liệu game sau khi trận kết thúc ===
        public async Task DeleteGameAsync(string gameId)
        {
            if (string.IsNullOrWhiteSpace(gameId))
                return;

            await realtimeDb.DeleteAsync($"games/{gameId}");
        }

        // === xoá thông tin trận đấu dưới /matches/{matchId} ===
        public async Task DeleteMatchAsync(string matchId)
        {
            if (string.IsNullOrWhiteSpace(matchId))
                return;

            await realtimeDb.DeleteAsync($"matches/{matchId}");
        }


        // ===  sinh mã phòng 4 chữ số, đảm bảo chưa tồn tại dưới /matches ===
        public async Task<string> GenerateMatchIdAsync()
        {
            var rnd = new Random();
            string matchId;
            int maxTry = 20;

            for (int i = 0; i < maxTry; i++)
            {
                matchId = rnd.Next(0, 10000).ToString("D4"); // 0000 - 9999

                var existed = await realtimeDb.GetAsync<MatchInfo>($"matches/{matchId}");
                if (existed == null)
                {
                    return matchId;
                }
            }

            throw new Exception("Không thể tạo MatchId mới (thử quá nhiều lần).");
        }

        // ===  lưu thông tin trận đấu dưới /matches/{MatchId} ===
        public async Task SaveMatchAsync(MatchInfo match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            if (string.IsNullOrWhiteSpace(match.MatchId))
                throw new ArgumentException("Match.MatchId không được trống");

            await realtimeDb.SetAsync($"matches/{match.MatchId}", match);
        }

        // ===  đọc thông tin trận đấu từ /matches/{matchId} ===
        public async Task<MatchInfo?> LoadMatchAsync(string matchId)
        {
            if (string.IsNullOrWhiteSpace(matchId))
                return null;

            return await realtimeDb.GetAsync<MatchInfo>($"matches/{matchId}");
        }
    }
}
