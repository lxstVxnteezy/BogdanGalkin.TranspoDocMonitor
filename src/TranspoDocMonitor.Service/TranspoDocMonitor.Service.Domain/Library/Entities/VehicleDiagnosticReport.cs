using TranspoDocMonitor.Service.Domain.Base;

namespace TranspoDocMonitor.Service.Domain.Library.Entities
{
    public class VehicleDiagnosticReport : BaseEntity
    {
        public long DiagnosticCardNumber { get; set; }
        public DateTime ExpirationDateOfIssue { get; set; }
        public Guid VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; } = null!;
    }
}
