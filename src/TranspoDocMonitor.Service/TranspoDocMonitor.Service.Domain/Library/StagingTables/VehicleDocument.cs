using TranspoDocMonitor.Service.Domain.Base;
using TranspoDocMonitor.Service.Domain.Library.Entities;

namespace TranspoDocMonitor.Service.Domain.Library.StagingTables
{
    public class VehicleDocument : BaseEntity
    {
        public DateTime DateOfIssue { get; set; }
        public DateTime ExpirationDateOfIssue { get; set; }
        public string Policyholder { get; set; } = null!;
        public string Beneficiary { get; set; } = null!;

        public int ContractNumberCCI { get; set; }
        public int NumberSeriesCCLI { get; set; }

        public Decimal Insurance { get; set; }
        public Decimal СoverageAmount { get; set; }



        public Guid VehicleId { get; set; }
        public virtual Vehicle? Vehicle { get; set; } = null!;


    }
}
