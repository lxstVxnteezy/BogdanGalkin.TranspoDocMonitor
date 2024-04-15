using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TranspoDocMonitor.Service.Domain.Library.Entities;

namespace TranspoDocMonitor.Service.DataContext.Configuration
{
    internal class VehicleConfiguration
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("vehicles");

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Make).HasColumnName("make");
            builder.Property(x => x.Model).HasColumnName("model");
            builder.Property(x => x.AutoColor).HasColumnName("autoColor");
            builder.Property(x => x.Year).HasColumnName("year");
        }
    }
}
