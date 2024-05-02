using Hangfire;
using TranspoDocMonitor.Service.Core.BackgroundJob.RecurringJobs;

namespace TranspoDocMonitor.Service.Core.BackgroundJob
{
    static class BackgroundJobManager
    {
        public static void Start()
        {
            DocumentExprationCheckerReccuringJob();

        }

        private static void DocumentExprationCheckerReccuringJob()
        {
            RecurringJob.AddOrUpdate<DocumentExpirationChecker>("document-expiration-check",
                x => x.CheckDocumentExpirations(CancellationToken.None),
                Cron.Minutely);
        }
    }
}
