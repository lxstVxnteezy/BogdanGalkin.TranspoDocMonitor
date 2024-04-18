using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TranspoDocMonitor.Service.API.Controllers.Base;
using TranspoDocMonitor.Service.Contracts.Shared.Auth;
using IAuthorizationHandler = TranspoDocMonitor.Service.HTTP.Handlers.Methods.Auth.IAuthorizationHandler;

namespace TranspoDocMonitor.Service.API.Controllers.Auth
{

    [Authorize]
    [Route("api/auth")]
    public class AuthController : BaseApiController
    {
        [AllowAnonymous]
        [HttpPost("login")]
        public Task<AuthResponse> Login(
            [FromServices] IAuthorizationHandler handler,
            [FromBody] AuthRequest request,
            CancellationToken ctn)
        {
            return handler.Handle(request, ctn);
        }

    }
}
