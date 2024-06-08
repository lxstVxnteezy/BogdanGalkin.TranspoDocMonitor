using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using TranspoDocMonitor.Service.Core.Authorization;
//using TranspoDocMonitor.Service.Core.BackgroundJob;
using TranspoDocMonitor.Service.Core.Cors;
using TranspoDocMonitor.Service.Core.HTTP.HttpAccessor;
//using TranspoDocMonitor.Service.Core.Notification;
using TranspoDocMonitor.Service.Core.Swagger;
using TranspoDocMonitor.Service.Core.Validation;
using TranspoDocMonitor.Service.DataContext.DataAccess;
using TranspoDocMonitor.Service.HTTP.Handlers;
using TranspoDocMonitor.Service.Middlewares.Exceptions;

namespace TranspoDocMonitor.Service.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHealthChecks();
            services.AddSwagger();
            services.AddDataAccess(Configuration);
            services.AddHttpHandlers();
            services.AddJwtAuthorization();
            services.AddHttpAccessor();
           // services.AddCustomHangFire(Configuration);
           // services.AddNotification(Configuration);
            services.AddValidation();
            services.AddSupportCors();

        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseSwaggerCustom();
            app.UseRouting();
            app.UseMiddlewareExceptions();
            app.UseAuthentication();
            app.UseAuthorization();
          //  app.UseHangFire();
            app.UseCorsCustom();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });

        }
    }
}
