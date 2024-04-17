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
            builder.Property(x=>x.Model).HasColumnName("model");
            builder.Property(x => x.AutoColor).HasConversion<string>();
            builder.Property(x => x.AutoColor).HasColumnName("auto_color");
            builder.Property(x => x.RegistrationNumber).HasColumnName("registration_number");
            builder.Property(x => x.Year).HasColumnName("year_of_issue");
        }
    }
}
