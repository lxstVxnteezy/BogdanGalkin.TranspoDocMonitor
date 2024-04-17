using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TranspoDocMonitor.Service.API.Controllers.Base;
using TranspoDocMonitor.Service.Contracts.User.Create;
using TranspoDocMonitor.Service.HTTP.Handlers.Methods.Users;

namespace TranspoDocMonitor.Service.API.Controllers.User
{
    [Authorize(Roles = "administrator")]
    [Route("api/user")]
    public class UserController : BaseApiController
    {
        [HttpPost("/createUser") ]
        public Task<CreateUserResponse> Create(
            [FromServices] ICreateUserHandler handler,
            [FromBody] CreateUserRequest request,
            CancellationToken ctn)
        {
            return handler.Handle(request, ctn);
        }
    }
}
