using Microsoft.AspNetCore.Mvc;
using TranspoDocMonitor.Service.API.Controllers.Base;
using TranspoDocMonitor.Service.Contracts.Create;
using TranspoDocMonitor.Service.HTTP.Handlers.Methods.Users;

namespace TranspoDocMonitor.Service.API.Controllers.Trash
{

    [ApiController]
    [Route("[controller]")]
    public class TestController : BaseApiController
    {
        [HttpPost]
        public Task<CreateUserResponse> Create(
            [FromServices] ICreateUserHandler handler,
            [FromBody] CreateUserRequest request,
            CancellationToken ctn)
        {
            return handler.Handle(request, ctn);
        }
    }
}
