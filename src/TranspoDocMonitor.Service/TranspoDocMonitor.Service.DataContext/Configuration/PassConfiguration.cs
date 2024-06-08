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
            builder.Property(x => x.From).HasColumnName("from");
            builder.Property(x => x.ExpirationDateOfIssue).HasColumnName("expiration_date_of_issue");
            builder.Property(x => x.VehicleId).HasColumnName("vehicle_id");
        }
    }
}
