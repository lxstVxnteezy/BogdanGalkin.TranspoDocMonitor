
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TranspoDocMonitor.Service.Domain.Library.StagingTables;

namespace TranspoDocMonitor.Service.DataContext.Configuration
{
    internal class UserVehicleConfiguration:IEntityTypeConfiguration<UserVehicle>
    {
        public void Configure(EntityTypeBuilder<UserVehicle> builder)
        {
            builder.ToTable("user_vehicle");

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.UserId).HasColumnName("user_id");
            builder.Property(x => x.VehicleId).HasColumnName("vehicle_id");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User)
                .WithMany(x => x.UserVehicles)
                .HasForeignKey(x => x.UserId);
            builder.
                HasOne(x=>x.Vehicle).
                WithMany(x=>x.UserVehicles).
                HasForeignKey(x => x.VehicleId);
        }
    }
}
