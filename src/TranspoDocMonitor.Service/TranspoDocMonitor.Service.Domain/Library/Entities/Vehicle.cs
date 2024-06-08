using TranspoDocMonitor.Service.Domain.Base;
using TranspoDocMonitor.Service.Domain.EnumTypes;
using TranspoDocMonitor.Service.Domain.Library.StagingTables;

namespace TranspoDocMonitor.Service.Domain.Library.Entities
{
    public class Vehicle : BaseEntity
    {
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Year { get; set; } = null!;
        public AutoColor AutoColor { get; set; }
        public string RegistrationNumber { get; set; } = null!;
        public string VehicleIdentificationNumber { get; set; } = null!;
        public double EngineCapacity { get; set; } 
        public decimal Price { get; set; }
        public virtual ICollection<UserVehicle> UserVehicles { get; set; } = new List<UserVehicle>();

    }
}




