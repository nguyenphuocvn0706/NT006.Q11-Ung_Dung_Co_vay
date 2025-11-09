using Firebase.Auth;
using Firebase.Auth.Providers;
using System.Threading.Tasks;

namespace Co_Vay
{
    internal class FirebaseService
    {
        private const string ApiKey = "AIzaSyB2hBtJx5MgJ8R4dlImA06yCjIf3l1zilE";
        private readonly FirebaseAuthClient authClient;

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

        public async Task<UserCredential> RegisterAsync(string email, string password)
        {
            return await authClient.CreateUserWithEmailAndPasswordAsync(email, password);
        }

        public async Task<UserCredential> LoginAsync(string email, string password)
        {
            return await authClient.SignInWithEmailAndPasswordAsync(email, password);
        }

        public async Task ResetPasswordAsync(string email)
        {
            await authClient.ResetEmailPasswordAsync(email);
        }
    }
}
