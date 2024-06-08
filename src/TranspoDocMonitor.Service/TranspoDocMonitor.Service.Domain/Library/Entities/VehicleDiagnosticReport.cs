using System.Numerics;
using TranspoDocMonitor.Service.Domain.Base;
using TranspoDocMonitor.Service.Domain.Library.StagingTables;

namespace TranspoDocMonitor.Service.Domain.Library.Entities
{
    public class VehicleDiagnosticReport : BaseEntity
    {
        public BigInteger DiagnosticCardNumber { get; set; }
        public DateTime ExpirationDateOfIssue { get; set; }

        public virtual Vehicle Vehicle { get; set; } = null!;
    }
}
