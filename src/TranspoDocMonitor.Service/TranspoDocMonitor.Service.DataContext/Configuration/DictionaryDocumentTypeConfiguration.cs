
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TranspoDocMonitor.Service.Domain.Library.Dictionaries;

namespace TranspoDocMonitor.Service.DataContext.Configuration
{
    internal class DictionaryDocumentTypeConfiguration
    {
        public void Configure(EntityTypeBuilder<DictionaryDocumentType> builder)
        {
            builder.ToTable("dictionariesDocumentType");

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.DocumentName).HasColumnName("DocumentName");



        }
    }
}
