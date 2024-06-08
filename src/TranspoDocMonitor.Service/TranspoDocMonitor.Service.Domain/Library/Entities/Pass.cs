using TranspoDocMonitor.Service.Domain.Base;
using TranspoDocMonitor.Service.Domain.EnumTypes;

namespace TranspoDocMonitor.Service.Domain.Library.Entities
{
    public class Pass : BaseEntity
    {
        public DateTime ExpirationDateOfIssue { get; set; }
        public int PassNumber { get; set; }

        public Guid VehicleId { get; set; }
        public virtual Vehicle? Vehicle { get; set; }

        public From From { get; set; }
    }
}
