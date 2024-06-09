
using MimeKit;
using System.Text;
using TranspoDocMonitor.Service.Core.Notification;
using TranspoDocMonitor.Service.Domain.Library.Entities;

namespace TranspoDocMonitor.Service.Core.Nortification
{
    public interface IEmailBuilderPasses
    {
        Task BuildEmailMessage(Pass document, CancellationToken ctn);
    }
    internal class EmailBuilderPasses : IEmailBuilderPasses
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly IEmailNotification _emailNotification;

        public EmailBuilderPasses(SmtpSettings smtpSettings, IEmailNotification emailNotification)
        {
            _smtpSettings = smtpSettings;
            _emailNotification = emailNotification;
        }

        public async Task BuildEmailMessage(Pass document, CancellationToken ctn)
        {
            MimeMessage message = MessageBuilderVehicalDiagnosticReport(document);
            await _emailNotification.SendEmailAsync(message, ctn);
        }

        private string BuildEmailBodyVehicalDiagnosticReport(Pass document)
        {
            var sb = new StringBuilder();
            sb.AppendLine("VehicalDiagnosticPasses");

            sb.AppendLine($"Dear, {document.Vehicle.User.FirstName},");
            sb.AppendLine("We have important information for you:");
            sb.AppendLine($"Document Number: {document.PassNumber}");
            sb.AppendLine($"Document expiration date: {document.ExDateTime}");
            sb.AppendLine("Sincerely,");
            sb.AppendLine("Your TranspoDocMonitor team");
            return sb.ToString();
        }

        private MimeMessage MessageBuilderVehicalDiagnosticReport(Pass document)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
            message.To.Add(new MailboxAddress(document!.Vehicle!.User!.FirstName, document.Vehicle.User.Email));
            message.Subject = "Information about the document and vehicle";
            var bodyBuilder = new BodyBuilder
            {
                TextBody = BuildEmailBodyVehicalDiagnosticReport(document)
            };
            message.Body = bodyBuilder.ToMessageBody();
            return message;
        }
    }
}
