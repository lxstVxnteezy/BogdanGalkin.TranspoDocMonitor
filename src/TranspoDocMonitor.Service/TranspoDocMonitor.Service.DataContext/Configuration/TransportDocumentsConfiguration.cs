
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TranspoDocMonitor.Service.Domain.Library.StagingTables;

namespace TranspoDocMonitor.Service.DataContext.Configuration
{
    internal class TransportDocumentsConfiguration : IEntityTypeConfiguration<VehicleDocument>
    {
        public void Configure(EntityTypeBuilder<VehicleDocument> builder)
        {
            builder.ToTable("transport_documents");

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.UserVehicleId).HasColumnName("user_vehicle_id");
            builder.Property(x => x.DictionaryDocumentTypeId).HasColumnName("dictionary_document_type_id");
            builder.Property(x => x.DocumentNumber).HasColumnName("document_number");
            builder.Property(x => x.DateOfIssue).HasColumnName("data_of_issue");
            builder.Property(x => x.ExpirationDateOfIssue).HasColumnName("expiration_date_of_Issue");

        }
    }
}
