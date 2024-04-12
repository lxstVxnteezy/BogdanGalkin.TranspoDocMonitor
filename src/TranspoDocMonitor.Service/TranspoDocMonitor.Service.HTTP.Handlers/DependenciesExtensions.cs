using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace TranspoDocMonitor.Service.HTTP.Handlers
{
    public static class DependenciesExtensions
    {
        public static IServiceCollection AddHttpHandlers(this IServiceCollection services)
        {
            var serviceType = typeof(IHandler);

            var assembly = Assembly.GetExecutingAssembly();
            foreach (var implementationType in assembly.GetTypes()
                         .Where(type => serviceType.IsAssignableFrom(type) && !type.GetTypeInfo().IsAbstract))
            {
                var handlerInterface = implementationType.GetInterfaces().Single(x => x != serviceType);
                services.AddTransient(handlerInterface, implementationType);
            }

            return services;
        }
    }
}
