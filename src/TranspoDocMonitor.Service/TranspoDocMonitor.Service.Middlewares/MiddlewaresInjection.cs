using Microsoft.AspNetCore.Builder;
using System.Reflection;

namespace TranspoDocMonitor.Service.Middlewares
{
    public static class MiddlewaresInjection
    {
        public static IApplicationBuilder UseMiddlewaresCustom(this IApplicationBuilder app)
        {
            var applicationType = typeof(IMiddleware);
            var assembly = Assembly.GetExecutingAssembly();
            foreach (var implementationType in assembly.GetTypes()
                         .Where(type => applicationType.IsAssignableFrom(type) && !type.GetTypeInfo().IsAbstract))
            {
                var handlerInterface = implementationType.GetInterfaces().SingleOrDefault(x => x != applicationType);
                if (handlerInterface != null)
                {
                    var middlewareInstance = Activator.CreateInstance(implementationType);
                    var middlewareMethod = typeof(IApplicationBuilder).GetMethod("UseMiddleware", new Type[] { });
                    var genericMiddlewareMethod = middlewareMethod.MakeGenericMethod(handlerInterface);
                    genericMiddlewareMethod.Invoke(app, new[] { app });
                }
            }

            return app;

        }
    }
}
 