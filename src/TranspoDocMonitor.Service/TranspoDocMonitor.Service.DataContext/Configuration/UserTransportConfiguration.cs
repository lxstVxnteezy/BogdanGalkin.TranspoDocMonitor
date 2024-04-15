using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TranspoDocMonitor.Service.Domain.Library.StagingTables;

namespace TranspoDocMonitor.Service.DataContext.Configuration
{
    internal class UserTransportConfiguration
    {
        public void Configure(EntityTypeBuilder<UserTransport> builder)
        {
            builder.ToTable("userTransport");

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.VehicleId).HasColumnName("vehicleId");
            builder.Property(x => x.UserId).HasColumnName("userId");


            builder.HasKey(ut => ut.Id);

            builder.HasOne(ut => ut.User)
                .WithMany(u => u.UserTransports)
                .HasForeignKey(ut => ut.UserId);

            builder.HasOne(ut => ut.Vehicle)
                .WithMany(t => t.UserTransports)
                .HasForeignKey(ut => ut.VehicleId);
        }
    }
}
