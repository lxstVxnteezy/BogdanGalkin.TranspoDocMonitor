using TranspoDocMonitor.Service.Domain.Base;
using TranspoDocMonitor.Service.Domain.Library.Dictionaries;

namespace TranspoDocMonitor.Service.Domain.Library.StagingTables
{
    public class VehicleDocument : BaseEntity
    {
        public int DocumentNumber { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime ExpirationDateOfIssue { get; set; }


        public Guid UserVehicleId { get; set; }
        public virtual UserVehicle? UserVehicle { get; set; } = null!;
       
        public Guid DictionaryDocumentTypeId { get; set; }
        public virtual DictionaryDocumentType? DictionaryDocumentType { get; set; } = null!;

    }
}
