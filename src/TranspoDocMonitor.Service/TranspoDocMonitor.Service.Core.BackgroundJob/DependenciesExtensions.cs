using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TranspoDocMonitor.Service.Core.BackgroundJob.RecurringJobs;
using TranspoDocMonitor.Service.Core.Notification;
using TranspoDocMonitor.Service.DataContext;


namespace TranspoDocMonitor.Service.Core.BackgroundJob
{
    public static class DependenciesExtensions
    {
        static CancellationToken ctn = new CancellationToken();
        public static IServiceCollection AddCustomHangFire(this IServiceCollection services,IConfiguration configuration)
        {
            var connStr = configuration.GetConnectionString("DefaultConnection");

            services.AddHangfire(x => x.UsePostgreSqlStorage(connStr));
            services.AddHangfireServer();
            
            return services;
        }
        public static IApplicationBuilder UseHangFire(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard("/dashboard");
            var scope = app.ApplicationServices.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var context = serviceProvider.GetRequiredService<ServiceContext>();

            var emailNotification = app.ApplicationServices.GetRequiredService<EmailNotification>();
            var documentChecker = new DocumentExpirationChecker(emailNotification, context);
            documentChecker.ScheduleDocumentExpirationCheck(ctn);
            return app;

        }
    }
}
