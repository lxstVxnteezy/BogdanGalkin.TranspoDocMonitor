using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TranspoDocMonitor.Service.Domain.Identity;

namespace TranspoDocMonitor.Service.DataContext.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.Property(x => x.Login).HasColumnName("login");
            builder.Property(x => x.Hash).HasColumnName("hash");
            builder.Property(x => x.FirstName).HasColumnName("first_name");
            builder.Property(x => x.Surname).HasColumnName("sur_name");
            builder.Property(x => x.LastName).HasColumnName("last_name");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.RoleId).HasColumnName("role_id");
            builder.HasOne(p => p.Role)
                .WithMany(p => p.Users)
                .HasForeignKey(p => p.RoleId);

        

        }
    }
}
