using Hangfire.Dashboard;


namespace TranspoDocMonitor.Service.Core.BackgroundJob.Infrastructure
{
    public class HangfireDashboardAuthFilter : IDashboardAuthorizationFilter
    {

        public bool Authorize(DashboardContext context)
        {
            if (!context.GetHttpContext().Response.HttpContext.User.IsInRole("administrator"))
            {
                return false;
            }
            return true;
        }
    }
}
