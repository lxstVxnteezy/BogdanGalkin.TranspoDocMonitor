using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranspoDocMonitor.Service.Contracts.Exceptions;
using TranspoDocMonitor.Service.Contracts.User;
using TranspoDocMonitor.Service.Core.Authorization;
using TranspoDocMonitor.Service.Core.Exception;
using TranspoDocMonitor.Service.DataContext.DataAccess.Repositories;
using TranspoDocMonitor.Service.Domain.Identity;

namespace TranspoDocMonitor.Service.HTTP.Handlers.Methods.Users
{
    public interface IResetPasswordUserHandler : IHandler
    {
        Task<ActionResult> Handle(Guid id, ResetUserPasswordRequest request, CancellationToken ctn);
    }

    public class ResetPasswordUserHandler: IResetPasswordUserHandler
    {
        private readonly IRepository<User> _userRepository;

        public ResetPasswordUserHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ActionResult> Handle(Guid id, ResetUserPasswordRequest request, CancellationToken ctn)
        {
            var foundUser = await _userRepository.Query.SingleOrDefaultAsync(x => x.Id == id,ctn);

            if (foundUser == null)
            {
                throw OwnError.CanNotFindUser.ToException($"User with this id: {id} not found");
            }

            foundUser.Hash = request.NewPassword.ComputeHash();
            await _userRepository.SaveChanges(ctn);

            return new StatusCodeResult(204);
        }
    }
}
