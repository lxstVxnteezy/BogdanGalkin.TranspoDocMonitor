using TranspoDocMonitor.Service.Domain.EnumTypes;

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
        int Horsepower,
        DateTime ExpirationDateOfIssue);

}
