using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TranspoDocMonitor.Service.Domain.Library.Entities;

namespace TranspoDocMonitor.Service.DataContext.Configuration
{
    public class VehicleDiagnosticReportsConfiguration : IEntityTypeConfiguration<VehicleDiagnosticReport>
    {
        public void Configure(EntityTypeBuilder<VehicleDiagnosticReport> builder)
        {
            builder.ToTable("vehicle_diagnostic_report");

            builder.ToTable("vehicle_diagnostic_report");

            builder.Property(x => x.Id).HasColumnName("id").IsRequired();
            builder.Property(x => x.DiagnosticCardNumber).HasColumnName("diagnostic_card_number").IsRequired();
            builder.Property(x => x.ExpirationDateOfIssue).HasColumnName("expiration_date_of_issue").IsRequired();

            builder.HasOne(vdr => vdr.Vehicle)
                .WithOne(v => v.VehicleDiagnosticReport)
                .HasForeignKey<Vehicle>(v => v.VehicleDiagnosticReportId);
        }
    }
}
