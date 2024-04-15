using TranspoDocMonitor.Service.Domain.Base;
using TranspoDocMonitor.Service.Domain.Identity;
using TranspoDocMonitor.Service.Domain.Library.Entities;

namespace TranspoDocMonitor.Service.Domain.Library.StagingTables
{
    public class UserTransport : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
