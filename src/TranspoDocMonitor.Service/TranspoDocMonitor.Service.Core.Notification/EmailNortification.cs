using MailKit.Net.Smtp;
using MimeKit;
using TranspoDocMonitor.Service.Contracts.Exceptions;
using TranspoDocMonitor.Service.Core.Exception;
using TranspoDocMonitor.Service.Core.Nortification;

namespace TranspoDocMonitor.Service.Core.Notification
{
    public interface IEmailNotification
    {
        Task SendEmailAsync(MimeMessage? message, CancellationToken cancellationToken);
    }

    public class EmailNotification : IEmailNotification
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailNotification(SmtpSettings smtpSettings)
        {
            _smtpSettings = smtpSettings;
        }
        public async Task SendEmailAsync(MimeMessage message, CancellationToken ctn)
        {
            using (var smtpClient = new SmtpClient())
            {
                try
                {
                    smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await smtpClient.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port, false, ctn);
                    await smtpClient.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password, ctn);
                    await smtpClient.SendAsync(message, ctn);
                }
                catch (System.Exception ex)
                {
                    throw OwnError.CanNotSendEmailMessage.ToException($"Error sending email: {ex}");
                }
                finally
                {
                    await smtpClient.DisconnectAsync(true, ctn);
                }
            }
        }
    }
}
