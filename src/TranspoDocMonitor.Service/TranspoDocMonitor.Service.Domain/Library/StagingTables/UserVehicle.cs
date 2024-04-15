using TranspoDocMonitor.Service.Domain.Base;
using TranspoDocMonitor.Service.Domain.Identity;
using TranspoDocMonitor.Service.Domain.Library.Entities;

namespace TranspoDocMonitor.Service.Domain.Library.StagingTables
{
    public class UserVehicle : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User? User { get; set; }
        public Guid VehicleId { get; set; }
        public virtual Vehicle? Vehicle { get; set; }


        public virtual List<VehicleDocument> VehicleDocuments { get; set; } = new();

    }
}
