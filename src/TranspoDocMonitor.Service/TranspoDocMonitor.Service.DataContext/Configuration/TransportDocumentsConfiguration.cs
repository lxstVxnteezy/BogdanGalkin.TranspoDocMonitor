

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TranspoDocMonitor.Service.Domain.Library.StagingTables;

namespace TranspoDocMonitor.Service.DataContext.Configuration
{
    internal class TransportDocumentsConfiguration
    {
        public void Configure(EntityTypeBuilder<TransportDocuments> builder)
        {
            builder.ToTable("transportDocuments");

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.DictionaryDocumentTypeId).HasColumnName("DocTypeId");
            builder.Property(x => x.UserTransportDocId).HasColumnName("TrasportUserDocId");

            builder.Property(x => x.DocumentNumber).HasColumnName("documentNumber");
            builder.Property(x => x.DateOfIssue).HasColumnName("dateOfIssue");
            builder.Property(x => x.DictionaryDocumentType).HasColumnName("documentType");
            builder.Property(x => x.ExpirationDateOfIssue).HasColumnName("expirationDateOfIssue");
        }

    }
}
