using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TranspoDocMonitor.Service.Core.Hangfire
{
    public static class HangfireExtensions
    {
        public static IServiceCollection AddCustomHangfire(this IServiceCollection services, IConfiguration configuration)
        {
            var connStr = configuration.GetConnectionString("DefaultConnection");
            services.AddHangfire
            (
                x => x.UsePostgreSqlStorage(connStr)
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
            );
            services.AddHangfireServer();
            services.AddTransient<ExpiryNotificationService>();
            services.AddTransient<HangfireJobScheduler>();

            return services;
        }
        public static IApplicationBuilder UseCustomHangfire(this IApplicationBuilder app, HangfireJobScheduler jobScheduler)
        {
            app.UseHangfireDashboard("/dashboard");
            jobScheduler.ScheduleExpiryNotificationCheck();

            return app;
        }


    }
}
