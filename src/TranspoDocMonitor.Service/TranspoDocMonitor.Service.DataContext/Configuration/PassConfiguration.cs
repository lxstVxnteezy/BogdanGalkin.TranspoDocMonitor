using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TranspoDocMonitor.Service.Domain.Library.Entities;

namespace TranspoDocMonitor.Service.DataContext.Configuration
{
    public class PassConfiguration : IEntityTypeConfiguration<Pass>
    {
        public void Configure(EntityTypeBuilder<Pass> builder)
        {
            builder.ToTable("passes");

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.From).HasConversion<string>().HasColumnName("from");
            builder.Property(x => x.VehicleId).HasColumnName("vehicle_id");

            builder.HasOne(x => x.Vehicle)
                .WithMany(x => x.Passes)
                .HasForeignKey(x => x.VehicleId);

            builder
                .HasIndex(x => new {x.VehicleId,x.From})
                .IsUnique();
            builder
                .HasIndex(x => x.PassNumber)
                .IsUnique();
        }
    }
}
