using Microsoft.Extensions.Configuration;

namespace TranspoDocMonitor.Service.Core.Nortification
{
    public class SmtpSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }

        public SmtpSettings(IConfiguration configuration)
        {
            Host = configuration["SmtpSettings:Host"];
            Port = int.Parse(configuration["SmtpSettings:Port"]);
            Username = configuration["SmtpSettings:Username"];
            Password = configuration["SmtpSettings:Password"];
            SenderName = configuration["SmtpSettings:SenderName"];
            SenderEmail = configuration["SmtpSettings:SenderEmail"];
        }
    }
}
