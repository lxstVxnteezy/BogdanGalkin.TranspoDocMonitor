using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace TranspoDocMonitor.Service.Core.Cors
{
    public static class CorsExtensions
    {
        public static IApplicationBuilder UseCorsCustom(this IApplicationBuilder app)
        {
            app.UseCors("AllowAll");
            return app;
        }


        public static IServiceCollection AddSupportCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            return services;

        }
    }
}
