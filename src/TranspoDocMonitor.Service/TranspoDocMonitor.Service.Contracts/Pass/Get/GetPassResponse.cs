
using TranspoDocMonitor.Service.Domain.EnumTypes;

namespace TranspoDocMonitor.Service.Contracts.Pass.Get
{
    public record GetPassResponse(Guid Id, int PassNumber, Guid VehicleId, From From);

}
