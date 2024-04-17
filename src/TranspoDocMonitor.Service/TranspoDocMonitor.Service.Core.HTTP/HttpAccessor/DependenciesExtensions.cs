using ComStar.ElDabaa.Service.Core.Http.HttpAccessor;
using Microsoft.Extensions.DependencyInjection;

namespace TranspoDocMonitor.Service.Core.HTTP.HttpAccessor
{
    public static class DependenciesExtensions
    {
        public static IServiceCollection AddHttpAccessor(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IUserIdentityProvider, UserIdentityProvider>();

            return services;
        }
    }
}
