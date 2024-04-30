﻿using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TranspoDocMonitor.Service.Core.BackgroundJob.RecurringJobs;

namespace TranspoDocMonitor.Service.Core.BackgroundJob
{
    public static class DependenciesExtensions
    {
        public static IServiceCollection AddCustomHangFire(this IServiceCollection services, IConfiguration configuration)
        {
            var connStr = configuration.GetConnectionString("DefaultConnection");
            services.AddHangfire(x => x.UsePostgreSqlStorage(connStr));
            services.AddHangfireServer();
            services.AddSingleton<HangfireDashboardAuthFilter>();
            services.AddScoped<DocumentExpirationChecker>();
            return services;
        }

        public static IApplicationBuilder UseHangFire(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard("/dashboard", new DashboardOptions
            {
                Authorization = new[] { new HangfireDashboardAuthFilter() }
            });

            RecurringJob.
                AddOrUpdate<DocumentExpirationChecker>("document-expiration-check",
                    x => x.CheckDocumentExpirations(CancellationToken.None),
                Cron.Minutely);


            return app;
        }
    }
}