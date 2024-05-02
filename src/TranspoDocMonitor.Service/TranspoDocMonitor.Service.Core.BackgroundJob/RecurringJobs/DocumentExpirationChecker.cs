using Microsoft.EntityFrameworkCore;
using TranspoDocMonitor.Service.Core.Notification;
using TranspoDocMonitor.Service.DataContext;

namespace TranspoDocMonitor.Service.Core.BackgroundJob.RecurringJobs
{

    public interface IDocumentExpirationChecker
    {
        Task CheckDocumentExpirations(CancellationToken cancellationToken);
    }

    public class DocumentExpirationChecker : IDocumentExpirationChecker
    {
        private readonly EmailNotification _emailNotification;
        private readonly ServiceContext _serviceContext;

        public DocumentExpirationChecker(EmailNotification emailNotification, ServiceContext serviceContext)
        {
            _emailNotification = emailNotification;
            _serviceContext = serviceContext;
        }

        public async Task CheckDocumentExpirations(CancellationToken ctn)
        {
            var documentsToExpireTomorrow = _serviceContext.TransportDocuments
                .Include(td => td.UserVehicle)
                .ThenInclude(uv => uv.User)
                .Where(x=>x.ExpirationDateOfIssue == DateTime.Today.AddDays(1))
                .ToList();

            foreach (var document in documentsToExpireTomorrow)
            {
                var userId = document.UserVehicle?.UserId;
                if (userId != null)
                {
                    var user = _serviceContext.Users.SingleOrDefault(x => x.Id == userId);
                    await _emailNotification.SendEmailAsync(document, user, ctn);
                }
            }
        }

    }
}