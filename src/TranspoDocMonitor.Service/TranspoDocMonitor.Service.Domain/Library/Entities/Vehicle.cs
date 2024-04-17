using TranspoDocMonitor.Service.Domain.Base;
using TranspoDocMonitor.Service.Domain.EnumTypes;
using TranspoDocMonitor.Service.Domain.Library.StagingTables;

namespace TranspoDocMonitor.Service.Domain.Library.Entities
{
    public class Vehicle:BaseEntity
    {
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Year { get; set; }
        public AutoColor AutoColor { get; set; }
        public string RegistrationNumber { get; set; } = null!;


        public virtual ICollection<UserVehicle> UserVehicles { get; set; } = new List<UserVehicle>();



    }
}




