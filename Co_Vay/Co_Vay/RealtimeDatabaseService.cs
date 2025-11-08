using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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

        public async Task SetUserAsync(string uid, UserModel user)
        {
            string json = JsonSerializer.Serialize(user);
            await client.PutAsync(AuthUrl($"users/{uid}"), new StringContent(json, Encoding.UTF8, "application/json"));
        }

        public async Task UpdateUsernameMappingAsync(string username, string email)
        {
            var json = JsonSerializer.Serialize(email);
            await client.PutAsync(AuthUrl($"usernames/{username}"), new StringContent(json, Encoding.UTF8, "application/json"));
        }

        public async Task<string> GetEmailByUsernameAsync(string username)
        {
            var res = await client.GetAsync(AuthUrl($"usernames/{username}"));
            var text = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode && text != "null")
                return JsonSerializer.Deserialize<string>(text);
            return null;
        }
    }

    internal class UserModel
    {
        public string username { get; set; }
        public string email { get; set; }
        public int rank { get; set; } = 1;
        public int wins { get; set; } = 0;
        public int losses { get; set; } = 0;
    }
}
