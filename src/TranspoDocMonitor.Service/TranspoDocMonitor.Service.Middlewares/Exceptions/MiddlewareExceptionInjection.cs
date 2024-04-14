using Microsoft.AspNetCore.Builder;
using System.Reflection;

namespace TranspoDocMonitor.Service.Middlewares.Exceptions
{
    public static class MiddlewareExceptionInjection
    {
        public static IApplicationBuilder UseMiddlewaresExceptions(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;

        }
    }
}
