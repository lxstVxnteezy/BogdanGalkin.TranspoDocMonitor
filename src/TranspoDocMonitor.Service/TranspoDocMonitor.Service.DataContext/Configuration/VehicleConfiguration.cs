using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TranspoDocMonitor.Service.Domain.Library.Entities;

namespace TranspoDocMonitor.Service.DataContext.Configuration
{
    internal class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("vehicles");

            builder.Property(x => x.Id).HasColumnName("id").IsRequired();
            builder.Property(x => x.Make).HasColumnName("make").IsRequired();
            builder.Property(x => x.Model).HasColumnName("model").IsRequired();
            builder.Property(x => x.AutoColor).HasConversion<string>().HasColumnName("auto_color").IsRequired();
            builder.Property(x => x.RegistrationNumber).HasColumnName("registration_number").IsRequired();
            builder.Property(x => x.Year).HasColumnName("year_of_issue").IsRequired();
            builder.Property(x => x.VehicleIdentificationNumber).HasColumnName("vehicle_identification_number").IsRequired();
            builder.Property(x => x.EngineCapacity).HasColumnName("engine_capacity").IsRequired();
            builder.Property(x => x.Price).HasColumnName("price").IsRequired();
            builder.Property(x => x.UserId).HasColumnName("user_id").IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Vehicles)
                .HasForeignKey(x => x.UserId);

            builder
                .HasOne(v => v.VehicleDiagnosticReport)
                .WithOne(vdr => vdr.Vehicle)
                .HasForeignKey<VehicleDiagnosticReport>(vdr => vdr.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
