using Microsoft.EntityFrameworkCore;
using TranspoDocMonitor.Service.Contracts.Exceptions;
using TranspoDocMonitor.Service.Core.Exception;
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
        private readonly IRepository<VehicleDocument> _vehicleDocumentRepository;
        private readonly EmailNotification _emailNotification;

        public DocumentExpirationChecker(
            EmailNotification emailNotification,
            IRepository<VehicleDocument> vehicleDocumentRepository,
            IRepository<User> userRepository)
        {
            _emailNotification = emailNotification;
            _vehicleDocumentRepository = vehicleDocumentRepository;
        }

        public async Task CheckDocumentExpirations(CancellationToken ctn)
        {
            var documentsToExpireTomorrow = _vehicleDocumentRepository.Query
                .Include(td => td.UserVehicle)
                .ThenInclude(uv => uv!.User)
                .ToList();

            if (documentsToExpireTomorrow == null)
                return;

            foreach (var document in documentsToExpireTomorrow)
            {
                await _emailNotification.SendEmailAsync(document, ctn);
            }
        }

    }
}