using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TranspoDocMonitor.Service.Domain.Identity;

namespace TranspoDocMonitor.Service.DataContext.Configuration
{
    internal class RoleConfiguration:IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("roles");
            builder.Property(x => x.Name).HasColumnName("name");
            builder.Property(x => x.Id).HasColumnName("id");

          
        }
    }
}
