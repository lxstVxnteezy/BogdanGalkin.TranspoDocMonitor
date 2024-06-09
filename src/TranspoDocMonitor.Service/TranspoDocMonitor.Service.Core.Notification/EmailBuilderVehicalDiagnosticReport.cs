using System.Text;
using MimeKit;
using TranspoDocMonitor.Service.Core.Notification;
using TranspoDocMonitor.Service.Domain.Library.Entities;

namespace TranspoDocMonitor.Service.Core.Nortification
{
    public interface IEmailBuilderVehicalDiagnosticReport
    {
        Task BuildEmailMessage(VehicleDiagnosticReport document, CancellationToken ctn);
    }

    public class EmailBuilderVehicalDiagnosticReport : IEmailBuilderVehicalDiagnosticReport
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly IEmailNotification _emailNotification;

        public EmailBuilderVehicalDiagnosticReport(SmtpSettings smtpSettings, IEmailNotification emailNotification)
        {
            _smtpSettings = smtpSettings;
            _emailNotification = emailNotification;
        }

        public async Task BuildEmailMessage(VehicleDiagnosticReport document, CancellationToken ctn)
        {
            MimeMessage message = MessageBuilderVehicalDiagnosticReport(document);
            await _emailNotification.SendEmailAsync(message, ctn);
        }

        private string BuildEmailBodyVehicalDiagnosticReport(VehicleDiagnosticReport report)
        {
            var sb = new StringBuilder();
            sb.AppendLine("VehicalDiagnosticReport");
            sb.AppendLine($"Dear, {report.Vehicle.User.FirstName},");
            sb.AppendLine("We have important information for you:");
            sb.AppendLine($"Document Number: {report.DiagnosticCardNumber}");
            sb.AppendLine($"Document expiration date: {report.ExpirationDateOfIssue}");
            sb.AppendLine("Sincerely,");
            sb.AppendLine("Your TranspoDocMonitor team");
            return sb.ToString();
        }

        private MimeMessage MessageBuilderVehicalDiagnosticReport(VehicleDiagnosticReport report)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
            message.To.Add(new MailboxAddress(report!.Vehicle!.User!.FirstName, report.Vehicle.User.Email));
            message.Subject = "Information about the document and vehicle";
            var bodyBuilder = new BodyBuilder
            {
                TextBody = BuildEmailBodyVehicalDiagnosticReport(report)
            };
            message.Body = bodyBuilder.ToMessageBody();
            return message;
        }
    }
}
