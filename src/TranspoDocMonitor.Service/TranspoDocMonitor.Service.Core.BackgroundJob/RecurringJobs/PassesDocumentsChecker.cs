using Microsoft.EntityFrameworkCore;
using TranspoDocMonitor.Service.Core.Nortification;
using TranspoDocMonitor.Service.DataContext.DataAccess.Repositories;
using TranspoDocMonitor.Service.Domain.Library.Entities;

namespace TranspoDocMonitor.Service.Core.BackgroundJob.RecurringJobs
{
    public interface IPassesDocumentsChecker
    {
        Task CheckPassesDocumentExpirations(CancellationToken ctn);
    }

    internal class PassesDocumentsChecker : IPassesDocumentsChecker
    {
        private readonly IRepository<Pass> _passesRepository;
        private readonly IEmailBuilderPasses _emailBuilderPass;
        public PassesDocumentsChecker(
            IRepository<Pass> passesRepository,
            IEmailBuilderPasses emailBuilderPass)
        {
            _passesRepository = passesRepository;
            _emailBuilderPass = emailBuilderPass;
        }
        public async Task CheckPassesDocumentExpirations(CancellationToken ctn)
        {
            var documentsToExpireTomorrow = await _passesRepository .Query
                .Include(x => x.Vehicle)
                .ThenInclude(x => x.User).ToListAsync(ctn);

            if (documentsToExpireTomorrow == null)
                return;

            foreach (var document in documentsToExpireTomorrow)
            {
                await _emailBuilderPass.BuildEmailMessage(document, ctn);
            }
        }
    }
}
