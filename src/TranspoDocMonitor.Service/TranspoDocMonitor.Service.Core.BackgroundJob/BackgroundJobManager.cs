using Hangfire;
using TranspoDocMonitor.Service.Core.BackgroundJob.RecurringJobs;

namespace TranspoDocMonitor.Service.Core.BackgroundJob
{
    static class BackgroundJobManager
    {
        public static void Start()
        {
            DocumentExprationCheckerReccuringJob();
            PassesCheckerReccuringJob();
        }

        private static void DocumentExprationCheckerReccuringJob()
        {
            RecurringJob.AddOrUpdate<DiagnosticDocumentExpirationChecker>("document-expiration-check",
                x => x.CheckDiagnosticDocumentExpirations(CancellationToken.None),
                Cron.Minutely);
        }
        private static void PassesCheckerReccuringJob()
        {
            RecurringJob.AddOrUpdate<PassesDocumentsChecker>("passes-expiration-check",
                x => x.CheckPassesDocumentExpirations(CancellationToken.None),
                Cron.Minutely);
        }
    }
}
