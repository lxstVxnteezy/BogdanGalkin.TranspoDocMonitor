namespace TranspoDocMonitor.Service.Contracts.Shared.Auth
{
    public class AuthRequest
    {
        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
