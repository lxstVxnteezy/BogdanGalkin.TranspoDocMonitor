using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TranspoDocMonitor.Service.API.Controllers.Base;
using TranspoDocMonitor.Service.Contracts.Vehicle.Create;
using TranspoDocMonitor.Service.HTTP.Handlers.Methods.Vehicle;

namespace TranspoDocMonitor.Service.API.Controllers.Vehicle
{
    [Authorize(Roles = "member, administrator")]
    [Route("api/vehicle")]
    public class VehicleController : BaseApiController
    {
        [HttpPost("/createVehicle")]
        public Task<CreateVehicleResponse> Create(
            [FromServices] ICreateVehicleHandler handler,
            [FromBody] CreateVehicleRequest request,
            CancellationToken ctn)
        {
            return handler.Handle(request, ctn);
        }

        [HttpDelete("/deleteVehicle{id}")]
        public Task<StatusCodeResult> Delete(
            [FromServices] IDeleteVehicleHandler handler,
            [FromRoute] Guid id,
            CancellationToken ctn)
        {
            return handler.Handle(id, ctn);
        }
    }
}
