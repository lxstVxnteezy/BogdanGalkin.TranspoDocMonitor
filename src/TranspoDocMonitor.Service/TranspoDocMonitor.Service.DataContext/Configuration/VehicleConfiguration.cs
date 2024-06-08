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

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Make).HasColumnName("make");
            builder.Property(x => x.Model).HasColumnName("model");
            builder.Property(x => x.AutoColor).HasConversion<string>().HasColumnName("auto_color");
            builder.Property(x => x.RegistrationNumber).HasColumnName("registration_number");
            builder.Property(x => x.Year).HasColumnName("year_of_issue");
            builder.Property(x => x.VehicleIdentificationNumber).HasColumnName("vehicle_identification_number");
            builder.Property(x => x.EngineCapacity).HasColumnName("engine_capacity");
            builder.Property(x => x.Price).HasColumnName("price");

            builder.HasOne(x => x.VehicleDiagnosticReport)
                .WithOne(x => x.Vehicle)
                .HasForeignKey<VehicleDiagnosticReport>(x=>x.Id)
                .IsRequired();
        }
    }
}
