using System.Text;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Co_Vay
{
    internal class RealtimeDatabaseService
    {
        private readonly string baseUrl = "https://covay-54c65-default-rtdb.firebaseio.com/";
        private readonly string idToken;
        private readonly HttpClient client = new HttpClient();

        public RealtimeDatabaseService(string idToken = null)
        {
            this.idToken = idToken;
        }

        private string AuthUrl(string path)
        {
            return idToken == null ? $"{baseUrl}{path}.json" : $"{baseUrl}{path}.json?auth={idToken}";
        }

        // =====================================================================
        // ==============  CÁC HÀM DÙNG CHUNG CHO NHIỀU LOẠI DATA  =============
        // =====================================================================

        // ===  ghi (PUT) một object bất kỳ lên path chỉ định ===
        public async Task SetAsync<T>(string path, T data)
        {
            string json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await client.PutAsync(AuthUrl(path), content);
            res.EnsureSuccessStatusCode();
        }

        // ===  lấy (GET) object ở path chỉ định ===
        public async Task<T?> GetAsync<T>(string path)
        {
            var res = await client.GetAsync(AuthUrl(path));
            var text = await res.Content.ReadAsStringAsync();

            if (!res.IsSuccessStatusCode || text == "null")
                return default;

            return JsonSerializer.Deserialize<T>(text);
        }

        // ===  xoá node ở path chỉ định ===
        public async Task DeleteAsync(string path)
        {
            var res = await client.DeleteAsync(AuthUrl(path));
            res.EnsureSuccessStatusCode();
        }

        // GHI ĐÈ TOÀN BỘ USER 
        public async Task SetUserAsync(string uid, UserModel user)
        {
            string json = JsonSerializer.Serialize(user);
            await client.PutAsync(AuthUrl($"users/{uid}"),
                new StringContent(json, Encoding.UTF8, "application/json"));
        }

        //  UPDATE TRƯỜNG ĐƯỢC CHỈ ĐỊNH 
        public async Task UpdateUserFieldsAsync(string uid, object fields)
        {
            string json = JsonSerializer.Serialize(fields);
            await client.PatchAsync(AuthUrl($"users/{uid}"),
                new StringContent(json, Encoding.UTF8, "application/json"));
        }

        // MAPPING USERNAME -> EMAIL
        public async Task UpdateUsernameMappingAsync(string username, string email)
        {
            string json = JsonSerializer.Serialize(email);
            await client.PutAsync(AuthUrl($"usernames/{username}"),
                new StringContent(json, Encoding.UTF8, "application/json"));
        }

        public async Task<string> GetEmailByUsernameAsync(string username)
        {
            var res = await client.GetAsync(AuthUrl($"usernames/{username}"));
            var text = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode && text != "null")
                return JsonSerializer.Deserialize<string>(text);
            return null;
        }

        // LẤY USER TỪ DB
        public async Task<UserModel> GetUserAsync(string uid)
        {
            var res = await client.GetAsync(AuthUrl($"users/{uid}"));
            var text = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode && text != "null")
                return JsonSerializer.Deserialize<UserModel>(text);
            return null;
        }

        // XÓA USER DƯỚI NODE /users
        public async Task DeleteUserAsync(string uid)
        {
            using (var c = new HttpClient())
            {
                string url = $"{baseUrl}/users/{uid}.json?auth={idToken}";
                var response = await c.DeleteAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    string error = await response.Content.ReadAsStringAsync();
                    throw new Exception("Unable to delete user data: " + error);
                }
            }
        }

        // XÓA MAPPING USERNAME
        public async Task DeleteUsernameMappingAsync(string username)
        {
            using (var c = new HttpClient())
            {
                string url = $"{baseUrl}/usernames/{username}.json?auth={idToken}";
                await c.DeleteAsync(url);
            }
        }

        public async Task UpdateEloAsync(string uid, int delta)
        {
            var user = await GetUserAsync(uid);
            int currentElo = user?.elo ?? 0;

            int newElo = Math.Max(0, currentElo + delta);

            await UpdateUserFieldsAsync(uid, new
            {
                elo = newElo
            });
        }

        // ============================================================
        // ================== CHỈ THÊM – KHÔNG SỬA ===================
        // ============================================================

        // LẤY TOÀN BỘ MATCHES (PHỤC VỤ MATCH HISTORY)
        public async Task<Dictionary<string, MatchModel>> GetMatchesAsync()
        {
            return await GetAsync<Dictionary<string, MatchModel>>("matches");
        }

    }

    internal class UserModel
    {
        public string username { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string sex { get; set; }
        public string dob { get; set; }
        public int elo { get; set; } = 0;
    }

    // MODEL MATCH 
    internal class MatchModel
    {
        public string MatchId { get; set; }
        public int ResultType { get; set; }
        public int RoomType { get; set; }
        public bool IsRandom { get; set; }
        public int Status { get; set; }

        public string StartedAt { get; set; }
        public string EndedAt { get; set; }

        public string WinnerPlayerId { get; set; }
        public PlayerModel BlackPlayer { get; set; }
        public PlayerModel WhitePlayer { get; set; }
    }
    internal class PlayerModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
