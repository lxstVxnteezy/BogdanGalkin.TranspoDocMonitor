using System.Text;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using TranspoDocMonitor.Service.Domain.Identity;
using TranspoDocMonitor.Service.Domain.Library.StagingTables;

namespace TranspoDocMonitor.Service.Core.Notification
{
    public class EmailNotification 
    {
        private readonly SmtpClient _smtpClient;
        private readonly string _senderName;
        private readonly string _senderEmail;

        public EmailNotification(IConfiguration configuration)
        {
            var smtpSettings = configuration.GetSection("SmtpSettings");

            _smtpClient = new SmtpClient();
            _smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
            _smtpClient.Connect(smtpSettings["Host"], int.Parse(smtpSettings["Port"]), false);
            _smtpClient.Authenticate(smtpSettings["Username"], smtpSettings["Password"]);
            _senderName = smtpSettings["SenderName"];
            _senderEmail = smtpSettings["SenderEmail"];
        }

        public  async Task SendEmailAsync(VehicleDocument? dataDocument,User user,CancellationToken ctn)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_senderName, _senderEmail));
            message.To.Add(new MailboxAddress(user.FirstName, user.Email));
            message.Subject = "Информация о документе и транспортном средстве";
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = BuildEmailBody(user, dataDocument);
            message.Body = bodyBuilder.ToMessageBody();

            try
            {
                await _smtpClient.SendAsync(message, ctn);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при отправке электронного сообщения", ex);
            }
            finally
            {
                _smtpClient.Disconnect(true);
                _smtpClient.Dispose();
            }
        }

        private string BuildEmailBody(User user, VehicleDocument vehicleDocument)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Уважаемый {user.FirstName},");
            sb.AppendLine("У нас есть важная информация для вас:");
            sb.AppendLine($"Номер документа: {vehicleDocument.DocumentNumber}");
            sb.AppendLine($"Дата создания документа: {vehicleDocument.DateOfIssue}");
            sb.AppendLine($"Срок окончания действия документа: {vehicleDocument.ExpirationDateOfIssue}");
            sb.AppendLine("С уважением,");
            sb.AppendLine("Ваша команда TranspoDocMonitor");
            return sb.ToString();
        }
    }
}
