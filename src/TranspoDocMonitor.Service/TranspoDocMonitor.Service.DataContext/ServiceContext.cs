using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TranspoDocMonitor.Service.Domain.Base;
using TranspoDocMonitor.Service.Domain.Identity;
using TranspoDocMonitor.Service.Domain.Library.Dictionaries;
using TranspoDocMonitor.Service.Domain.Library.Entities;
using TranspoDocMonitor.Service.Domain.Library.StagingTables;

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
        public DbSet<Vehicle> Vehicles { get; set; } = null!;
        public DbSet<UserTransport> UsersTransports { get; set; } = null!;
        public DbSet<DictionaryDocumentType> DocumentTypes { get; set; } = null!;
        public DbSet<TransportDocuments> TransportDocuments { get; set; } = null!;
    }
}
