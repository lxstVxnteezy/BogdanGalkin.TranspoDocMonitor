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
        private readonly IRepository<VehicleDocument> _vehicleDocumentRepository;
        private readonly IRepository<User> _userRepository;
        private readonly EmailNotification _emailNotification;

        public DocumentExpirationChecker(
            EmailNotification emailNotification,
            IRepository<VehicleDocument> vehicleDocumentRepository,
            IRepository<User> userRepository)
        {
            _emailNotification = emailNotification;
            _vehicleDocumentRepository = vehicleDocumentRepository;
            _userRepository = userRepository;
        }

        public async Task CheckDocumentExpirations(CancellationToken ctn)
        {
            var documentsToExpireTomorrow = _vehicleDocumentRepository.Query
                .Include(td => td.UserVehicle)
                .ThenInclude(uv => uv.User)
                .Where(x => x.ExpirationDateOfIssue == DateTime.Today.AddDays(1))
                .ToList();

            foreach (var document in documentsToExpireTomorrow)
            {
                var userId = document.UserVehicle.UserId;
                if (userId != null)
                {
                    var user = await _userRepository.FoundByIdAsync(userId, ctn);
                    await _emailNotification.SendEmailAsync(document, user, ctn);
                }
            }
        }

    }
}