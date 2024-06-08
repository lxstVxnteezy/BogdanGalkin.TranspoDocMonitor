using Microsoft.EntityFrameworkCore;
using TranspoDocMonitor.Service.Core.Notification;
using TranspoDocMonitor.Service.DataContext.DataAccess.Repositories;
using TranspoDocMonitor.Service.Domain.Identity;
using TranspoDocMonitor.Service.Domain.Library.StagingTables;

namespace TranspoDocMonitor.Service.Core.BackgroundJob.RecurringJobs
{
     public interface IDocumentExpirationChecker
    {
        Task CheckDocumentExpirations(CancellationToken cancellationToken);
    }

    public class DocumentExpirationChecker : IDocumentExpirationChecker
    {
        public Task CheckDocumentExpirations(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}