using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TranspoDocMonitor.Service.Domain.Base;
using TranspoDocMonitor.Service.Domain.Identity;

namespace TranspoDocMonitor.Service.DataContext
{
    public class ServiceContext : DbContext
    {
        public ServiceContext(DbContextOptions options) : base(options)
        {
        }

        public ServiceContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly(),
                t => t.GetInterfaces().Any(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>) &&
                    typeof(BaseEntity).IsAssignableFrom(i.GenericTypeArguments[0]))
            );
        }
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
    }
}
