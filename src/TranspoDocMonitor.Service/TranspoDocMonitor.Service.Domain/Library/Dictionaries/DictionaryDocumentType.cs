using TranspoDocMonitor.Service.Domain.Base;

namespace TranspoDocMonitor.Service.Domain.Library.Dictionaries
{
    public class DictionaryDocumentType : BaseEntity
    {
        public string DocumentName { get; set; } = null!;
    }
}
