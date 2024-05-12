using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TranspoDocMonitor.Service.Core.Nortification;

namespace TranspoDocMonitor.Service.Core.Notification
{
    public static class DependenciesExtensions
    {
        public static IServiceCollection AddNotification(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmailNotification, EmailNotification>(); 
            services.AddSingleton<SmtpSettings>();
            return services;
        }

    }
}
