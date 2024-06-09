using TranspoDocMonitor.Service.Domain.Base;
using TranspoDocMonitor.Service.Domain.EnumTypes;
using TranspoDocMonitor.Service.Domain.Identity;

namespace TranspoDocMonitor.Service.Domain.Library.Entities
{
    public class Vehicle : BaseEntity
    {
        public Vehicle()
        {
            Passes = new List<Pass>();
        }    
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Year { get; set; } = null!;
        public AutoColor AutoColor { get; set; }
        public string RegistrationNumber { get; set; } = null!;
        public string VehicleIdentificationNumber { get; set; } = null!;
        public double EngineCapacity { get; set; } 
        public decimal Price { get; set; }
        public int Horsepower { get; set; }
        public virtual VehicleDiagnosticReport VehicleDiagnosticReport { get; set; } = null!;
        public virtual ICollection<Pass> Passes { get; set; }

        public virtual User User { get; set; } = null!;
        public Guid UserId { get; set; }

    }
}




