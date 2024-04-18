namespace TranspoDocMonitor.Service.Contracts.Shared.Auth
{
    public class AuthResponse
    {
        public string Token { get; }

        public AuthResponse(string token)
        {
            Token = token;
        }
    }
}
