using Microsoft.AspNetCore.Builder;

namespace TranspoDocMonitor.Service.Middlewares.Exceptions
{
    public static class DependenciesExtensions
    {
        public static IApplicationBuilder UseMiddlewareExceptions(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;

        }
    }
}
