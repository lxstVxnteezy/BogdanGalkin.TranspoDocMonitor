using System.Text;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using TranspoDocMonitor.Service.Contracts.Exceptions;
using TranspoDocMonitor.Service.Core.Exception;
using TranspoDocMonitor.Service.Domain.Identity;
using TranspoDocMonitor.Service.Domain.Library.StagingTables;

namespace TranspoDocMonitor.Service.Core.Notification
{
    public interface IEmailNotification
    {
        Task SendEmailAsync(VehicleDocument document, CancellationToken cancellationToken);
    }

    public class EmailNotification : IEmailNotification
    {

        private readonly IConfiguration _configuration;

        public EmailNotification(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(VehicleDocument? dataDocument, CancellationToken ctn)
        {
            using (var smtpClient = new SmtpClient())
            {
                try
                {
                    var smtpSettings = _configuration.GetSection("SmtpSettings");
                    smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await smtpClient.ConnectAsync(smtpSettings["Host"], int.Parse(smtpSettings["Port"]), false, ctn);
                    await smtpClient.AuthenticateAsync(smtpSettings["Username"], smtpSettings["Password"], ctn);
                    var message = MessageBuilder(dataDocument, smtpSettings);
                    await smtpClient.SendAsync(message, ctn);
                }
                catch (System.Exception)
                {
                    throw OwnError.CanNotSendEmailMessage.ToException("Error sending email");
                }
                finally
                {
                    await smtpClient.DisconnectAsync(true, ctn);
                }
            }
        }

        private MimeMessage MessageBuilder(VehicleDocument? dataDocument, IConfigurationSection smtpSettings)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(smtpSettings["SenderName"], smtpSettings["SenderEmail"]));
            message.To.Add(new MailboxAddress(dataDocument!.UserVehicle!.User!.FirstName, dataDocument.UserVehicle.User.Email));
            message.Subject = "Information about the document and vehicle";
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = BuildEmailBody(dataDocument.UserVehicle.User, dataDocument);
            message.Body = bodyBuilder.ToMessageBody();
            return message;
        }
        private string BuildEmailBody(User user, VehicleDocument vehicleDocument)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Dear, {user.FirstName},");
            sb.AppendLine("We have important information for you:");
            sb.AppendLine($"Document Number: {vehicleDocument.DocumentNumber}");
            sb.AppendLine($"Document creation date: {vehicleDocument.DateOfIssue}");
            sb.AppendLine($"Document expiration date: {vehicleDocument.ExpirationDateOfIssue}");
            sb.AppendLine("Sincerely,");
            sb.AppendLine("Your TranspoDocMonitor team");
            return sb.ToString();
        }
    }
}
