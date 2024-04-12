using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using TranspoDocMonitor.Service.Core.Swagger;
using TranspoDocMonitor.Service.DataContext.DataAccess;
using TranspoDocMonitor.Service.HTTP.Handlers;

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
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwaggerCustom();
            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

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
