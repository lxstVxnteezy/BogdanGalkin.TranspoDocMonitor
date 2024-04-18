
using TranspoDocMonitor.Service.Core.Email;
using TranspoDocMonitor.Service.DataContext.DataAccess.Repositories;
using TranspoDocMonitor.Service.Domain.Library.StagingTables;

namespace TranspoDocMonitor.Service.Core.Hangfire
{
    public class ExpiryNotificationService
    {
        private readonly IEmailSender _emailSender;
        private readonly IRepository<VehicleDocument> _vehicleDocumentRepository;

        public ExpiryNotificationService(IEmailSender emailSender, IRepository<VehicleDocument> vehicleDocumentRepository)
        {
            _emailSender = emailSender;
            _vehicleDocumentRepository = vehicleDocumentRepository;
        }

        public void CheckExpiryDates()
        {
            var today = DateTime.Today;
            var documentsExpiringTomorrow = _vehicleDocumentRepository.Query
                .Where(doc => doc.ExpirationDateOfIssue.Date == today.AddDays(1))
                .ToList();

            foreach (var document in documentsExpiringTomorrow)
            {
                // Отправляем уведомление по электронной почте
                string recipientEmail = "адрес_получателя@example.com";
                string subject = "Уведомление о скором истечении срока действия документа";
                string body = $"Документ {document.DocumentNumber} истекает завтра. Пожалуйста, обратите на это внимание.";
                _emailSender.SendEmailAsync(recipientEmail, subject, body);
            }
        }
    }


}
