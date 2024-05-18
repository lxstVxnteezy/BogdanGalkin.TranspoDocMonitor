using ComStar.ElDabaa.Service.Core.Http.HttpAccessor;
using Microsoft.AspNetCore.Mvc;
using TranspoDocMonitor.Service.Contracts.Exceptions;
using TranspoDocMonitor.Service.Core.Exception;
using TranspoDocMonitor.Service.DataContext.DataAccess.Repositories;
using TranspoDocMonitor.Service.Domain.Identity;

namespace TranspoDocMonitor.Service.HTTP.Handlers.Methods.Users
{
    public interface IDeleteUserHandler : IHandler
    {
        public Task<ActionResult> Handle(Guid id, CancellationToken ctn);
    }


    internal class DeleteUserHandler : IDeleteUserHandler
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUserIdentityProvider _userIdentityProvider;

        public DeleteUserHandler(IRepository<User> userRepository, IUserIdentityProvider userIdentityProvider)
        {
            _userRepository = userRepository;
            _userIdentityProvider = userIdentityProvider;
        }

        public async Task<ActionResult> Handle(Guid id, CancellationToken ctn)
        {
            ValidateUserSelfDeletion(id);
            var foundUser = await GetUserAsync(id, ctn);

            _userRepository.Remove(foundUser);
            await _userRepository.SaveChanges(ctn);

            return new StatusCodeResult(204);
        }

        private void ValidateUserSelfDeletion(Guid id)
        {
            if (id == _userIdentityProvider.GetCurrentUserId())
                throw OwnError.CanNotDeleteUser.ToException("You can't delete yourself");
        }

        private async Task<User> GetUserAsync(Guid id, CancellationToken ctn)
        {
            var foundUser = await _userRepository.FoundByIdAsync(id, ctn);
            if (foundUser == null)
                throw OwnError.CanNotDeleteUser.ToException("User not found in db");

            return foundUser;
        }
    }
}
