using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TranspoDocMonitor.Service.Core.Notification
{
    public static class DependenciesExtensions
    {
        public static IServiceCollection AddNotification(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<EmailNotification>();
            return services;
        }

    }
}
