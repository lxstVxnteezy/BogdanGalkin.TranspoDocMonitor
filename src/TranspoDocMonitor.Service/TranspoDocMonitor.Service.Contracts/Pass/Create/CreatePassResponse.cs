

using TranspoDocMonitor.Service.Domain.EnumTypes;

namespace TranspoDocMonitor.Service.Contracts.Pass.Create
{
    public record CreatePassResponse(Guid Id,int PassNumber, Guid VehicleId ,From From);


}
