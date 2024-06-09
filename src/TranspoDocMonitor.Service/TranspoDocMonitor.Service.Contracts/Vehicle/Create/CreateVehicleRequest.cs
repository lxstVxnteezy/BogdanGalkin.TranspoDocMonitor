using System.Numerics;
using TranspoDocMonitor.Service.Domain.EnumTypes;
using TranspoDocMonitor.Service.Domain.Library.Entities;

namespace TranspoDocMonitor.Service.Contracts.Vehicle.Create
{
    public record CreateVehicleRequest(
        string Make, 
        string Model, 
        string Year, 
        AutoColor Color, 
        string RegistrationNumber,
        string VehicleIdentificationNumber,
        double EngineCapacity,
        decimal Price,
        long DiagnosticCardNumber,
        DateTime ExpirationDateOfIssue);

}
