using MailKit.Net.Smtp;
using MimeKit;

namespace TranspoDocMonitor.Service.Core.Email
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string recipientEmail, string subject, string body);
    }

    internal class SendEmailService : IEmailSender
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;

        public SendEmailService(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUsername = smtpUsername;
            _smtpPassword = smtpPassword;
        }


        public async Task SendEmailAsync(string recipientEmail, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Your Name", _smtpUsername));
                message.To.Add(new MailboxAddress("", recipientEmail));
                message.Subject = subject;

                // Тело письма
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = body;

                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_smtpServer, _smtpPort, useSsl: true);
                    await client.AuthenticateAsync(_smtpUsername, _smtpPassword);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true); // 
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

    

