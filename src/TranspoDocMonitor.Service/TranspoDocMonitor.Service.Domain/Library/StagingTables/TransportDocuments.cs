using TranspoDocMonitor.Service.Domain.Base;
using TranspoDocMonitor.Service.Domain.Library.Dictionaries;

namespace TranspoDocMonitor.Service.Domain.Library.StagingTables
{
    public class TransportDocuments : BaseEntity
    {
        public int DocumentNumber { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime ExpirationDateOfIssue { get; set; }


        public Guid UserTransportDocId { get; set; }
        public UserTransport UsertTransport { get; set; } = null!;
        public Guid DictionaryDocumentTypeId { get; set; }
        public DictionaryDocumentType DictionaryDocumentType { get; set; } = null!;

    }
}
