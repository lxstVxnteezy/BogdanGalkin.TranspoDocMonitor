using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TranspoDocMonitor.Service.DataContext.DataAccess.Repositories;

namespace TranspoDocMonitor.Service.DataContext.DataAccess
{
    public static class DependenciesExtensions
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            var connStr = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ServiceContext>(options => options.UseNpgsql(connStr!));

            return services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        }
    }
}
