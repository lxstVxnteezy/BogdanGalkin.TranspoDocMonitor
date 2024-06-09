using Microsoft.EntityFrameworkCore;
using TranspoDocMonitor.Service.Core.Nortification;
using TranspoDocMonitor.Service.DataContext.DataAccess.Repositories;
using TranspoDocMonitor.Service.Domain.Library.Entities;


namespace TranspoDocMonitor.Service.Core.BackgroundJob.RecurringJobs
{
     public interface IDiagnosticDocumentExpirationChecker
    {
        Task CheckDiagnosticDocumentExpirations(CancellationToken ctn);
    }

    public class DiagnosticDocumentExpirationChecker : IDiagnosticDocumentExpirationChecker
    {
        private readonly IRepository<VehicleDiagnosticReport> _vehicleDiagnosticRepository;
        private readonly IEmailBuilderVehicalDiagnosticReport _emailBuilderVehicalDiagnosticReport;
        public DiagnosticDocumentExpirationChecker(
            
            IRepository<VehicleDiagnosticReport> vehicleDiagnosticRepository,
            IEmailBuilderVehicalDiagnosticReport emailBuilderVehicalDiagnosticReport)
        {
            _vehicleDiagnosticRepository = vehicleDiagnosticRepository;
            _emailBuilderVehicalDiagnosticReport = emailBuilderVehicalDiagnosticReport;
        }
        public async Task CheckDiagnosticDocumentExpirations(CancellationToken ctn)
        {
            var documentsToExpireTomorrow = await _vehicleDiagnosticRepository.Query
                .Include(x => x.Vehicle)
                .ThenInclude(x => x.User).ToListAsync(ctn);
           
            if (documentsToExpireTomorrow == null)
                return;

            foreach (var document in documentsToExpireTomorrow)
            {
                await _emailBuilderVehicalDiagnosticReport.BuildEmailMessage(document,ctn);
            }
        }



    }
}