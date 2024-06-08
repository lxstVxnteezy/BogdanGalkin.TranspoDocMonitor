using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TranspoDocMonitor.Service.API.Controllers.Base;
using TranspoDocMonitor.Service.Contracts.User;
using TranspoDocMonitor.Service.Contracts.User.Create;
using TranspoDocMonitor.Service.Contracts.User.Update;
using TranspoDocMonitor.Service.HTTP.Handlers.Methods.Users;

namespace TranspoDocMonitor.Service.API.Controllers.User
{
    [Authorize(Roles = "administrator")]
    [Route("api/user")]
    public class UserController : BaseApiController
    {
        [HttpPost("/createUser")]
        public Task<CreateUserResponse> Create(
            [FromServices] ICreateUserHandler handler,
            [FromBody] CreateUserRequest request,
            CancellationToken ctn)
        {
            return handler.Handle(request, ctn);
        }


        [HttpDelete("/deleteUser{id}")]
        public Task<ActionResult> Delete(
            [FromServices] IDeleteUserHandler handler,
            [FromRoute] Guid id,
            CancellationToken ctn)
        {
            return handler.Handle(id, ctn);
        }

        [HttpPut("{id}/resetPassword")]
        public Task<ActionResult> ResetPassword(
            [FromServices] IResetPasswordUserHandler handler,
            [FromBody] ResetUserPasswordRequest request,
            [FromRoute] Guid id,
            CancellationToken ctn)
        {
            return handler.Handle(id, request, ctn);
        }

        [HttpPut("{id}/updateUser")]
        public Task<UpdateUserResponse> Update(
            [FromServices] IUpdateUserHandler handler,
            [FromRoute] Guid id,
            [FromBody] UpdateUserRequest request,
            CancellationToken ctn)
        {
            return handler.Handle(id, request, ctn);
        }
    }
}
