using Hangfire;
using Microsoft.EntityFrameworkCore;
using TranspoDocMonitor.Service.Core.Notification;
using TranspoDocMonitor.Service.DataContext;
using TranspoDocMonitor.Service.DataContext.DataAccess.Repositories;
using TranspoDocMonitor.Service.Domain.Identity;
using TranspoDocMonitor.Service.Domain.Library.StagingTables;


namespace TranspoDocMonitor.Service.Core.BackgroundJob.RecurringJobs
{
    public class DocumentExpirationChecker
    {
        private readonly EmailNotification _emailNotification;
        private readonly ServiceContext _serviceContext;
        public DocumentExpirationChecker(EmailNotification emailNotification, ServiceContext serviceContext)
        {
            _emailNotification = emailNotification;
            _serviceContext = serviceContext;

        }

        public  void CheckDocumentExpirations()
        {
            var documentsToExpireTomorrow = _serviceContext.TransportDocuments
                .Include(td => td.UserVehicle)
                .ThenInclude(uv => uv.User)
                .ToList();

            foreach (var document in documentsToExpireTomorrow)
            {
                var userId = document.UserVehicle?.UserId;
                if (userId != null)
                {
                    var user = _serviceContext.Users.SingleOrDefault(x=>x.Id==userId);
                     _emailNotification.SendEmailAsync(document, user);
                }
            }

        }

        public void ScheduleDocumentExpirationCheck()
        {
            RecurringJob.AddOrUpdate<DocumentExpirationChecker>("document-expiration-check",
                x => x.CheckDocumentExpirations(), Cron.MinuteInterval(1));
        }
    }
}
