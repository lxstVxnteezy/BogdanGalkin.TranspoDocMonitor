using ComStar.ElDabaa.Service.Core.Http.HttpAccessor;
using Hangfire.Dashboard;


namespace TranspoDocMonitor.Service.Core.BackgroundJob
{
    public class HangfireDashboardAuthFilter : IDashboardAuthorizationFilter
    {


        public HangfireDashboardAuthFilter()
        {
        }

        public bool Authorize(DashboardContext context)
        {
            var httpCtx = context.GetHttpContext();

            if (!httpCtx.User.Identity.IsAuthenticated)
            {
                return false;
            }

            return true;
        }
    }
}
