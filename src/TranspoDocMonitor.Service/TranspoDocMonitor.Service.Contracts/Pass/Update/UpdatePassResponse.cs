

using TranspoDocMonitor.Service.Domain.EnumTypes;

namespace TranspoDocMonitor.Service.Contracts.Pass.Update
{
    public record UpdatePassResponse(int PassNumber, DateTime ExDateTime, Guid VehicleId, From From);

}
