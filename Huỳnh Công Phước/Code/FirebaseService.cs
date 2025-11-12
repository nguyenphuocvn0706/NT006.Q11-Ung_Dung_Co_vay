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

        public FirebaseAuthClient AuthClient => authClient;

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
        }

        // ✅ Đăng ký tài khoản mới
        public async Task<UserCredential> RegisterAsync(string email, string password)
        {
            return await authClient.CreateUserWithEmailAndPasswordAsync(email, password);
        }

        // ✅ Đăng nhập
        public async Task<UserCredential> LoginAsync(string email, string password)
        {
            return await authClient.SignInWithEmailAndPasswordAsync(email, password);
        }

        // ✅ Đặt lại mật khẩu (dùng REST API vì SDK mới không có ResetPasswordAsync)
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

        // ✅ Gửi email xác minh (REST API)
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

        // ✅ Làm mới token (thay thế cho RefreshUserAsync)
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
    }
}
