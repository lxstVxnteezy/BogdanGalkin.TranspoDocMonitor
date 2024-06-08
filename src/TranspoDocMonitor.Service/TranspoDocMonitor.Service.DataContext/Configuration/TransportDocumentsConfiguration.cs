using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TranspoDocMonitor.Service.Domain.Library.StagingTables;

namespace TranspoDocMonitor.Service.DataContext.Configuration
{
    internal class TransportDocumentsConfiguration : IEntityTypeConfiguration<VehicleDocument>
    {
        public void Configure(EntityTypeBuilder<VehicleDocument> builder)
        {
            builder.ToTable("insurance_transport_documents");

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.VehicleId).HasColumnName("vehicle_id");
            builder.Property(x => x.DateOfIssue).HasColumnName("data_of_issue");
            builder.Property(x => x.ExpirationDateOfIssue).HasColumnName("expiration_date_of_Issue");
            builder.Property(x => x.Policyholder).HasColumnName("policyholder");
            builder.Property(x => x.Beneficiary).HasColumnName("beneficiary");
            builder.Property(x => x.ContractNumberCCI).HasColumnName("contract_number_comprehensive_car_insurance");
            builder.Property(x => x.NumberSeriesCCLI).HasColumnName("policy_number_series_compulsory_сivil_liability_insurance");
            builder.Property(x => x.Insurance).HasColumnName("sum_insured");
            builder.Property(x => x.СoverageAmount).HasColumnName("coverage_amount");
        }
    }
}
