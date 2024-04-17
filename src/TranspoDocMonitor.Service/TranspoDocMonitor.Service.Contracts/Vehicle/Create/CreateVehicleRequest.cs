using TranspoDocMonitor.Service.Domain.EnumTypes;

namespace TranspoDocMonitor.Service.Contracts.Vehicle.Create
{
    public record CreateVehicleRequest(string Make, string Model, int Year, AutoColor Color, string RegistrationNumber);

}
