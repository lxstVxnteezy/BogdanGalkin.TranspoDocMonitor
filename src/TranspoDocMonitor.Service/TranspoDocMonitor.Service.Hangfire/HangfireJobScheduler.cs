using Hangfire;

namespace TranspoDocMonitor.Service.Core.Hangfire
{
    public class HangfireJobScheduler
    {
        private readonly ExpiryNotificationService _expiryNotificationService;

        public HangfireJobScheduler(ExpiryNotificationService expiryNotificationService)
        {
            _expiryNotificationService = expiryNotificationService;
        }

        public void ScheduleExpiryNotificationCheck()
        {
            RecurringJob.AddOrUpdate("ExpiryNotificationCheck", () => _expiryNotificationService.CheckExpiryDates(), Cron.Daily);
        }
    }
}
