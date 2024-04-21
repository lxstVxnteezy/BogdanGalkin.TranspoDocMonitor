using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TranspoDocMonitor.Service.Contracts.Shared.Notification.Email;

namespace TranspoDocMonitor.Service.Core.Notification
{
    public static class DependenciesExtensions
    {
        public static IServiceCollection AddNotification(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<EmailNotification>();
            var smtpSettings = new SmtpSettings()
            {
                Host = configuration["SmtpSettings:Host"],
                Port = int.Parse(configuration["SmtpSettings:Port"]),
                Username = configuration["SmtpSettings:Username"],
                Password = configuration["SmtpSettings:Password"],
                SenderName = configuration["SmtpSettings:SenderName"],
                SenderEmail = configuration["SmtpSettings:SenderEmail"]
            };

            services.AddSingleton(smtpSettings);
            return services;
        }

    }
}
