using Microsoft.EntityFrameworkCore;
using TranspoDocMonitor.Service.Contracts.Exceptions;
using TranspoDocMonitor.Service.Contracts.User.Update;
using TranspoDocMonitor.Service.Core.Exception;
using TranspoDocMonitor.Service.DataContext.DataAccess.Repositories;
using TranspoDocMonitor.Service.Domain.Identity;

namespace TranspoDocMonitor.Service.HTTP.Handlers.Methods.Users
{
    public interface IUpdateUserHandler : IHandler
    {
        public Task<UpdateUserResponse> Handle(Guid id, UpdateUserRequest request, CancellationToken ctn);
    }
    public class UpdateUserHandler : IUpdateUserHandler
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;

        public UpdateUserHandler(
            IRepository<User> userRepository,
            IRepository<Role> roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }
        public async Task<UpdateUserResponse> Handle(Guid id, UpdateUserRequest request, CancellationToken ctn)
        {
            AssertRole(request);
            var foundUser = await _userRepository.Query
                .Include(x => x.Role)
                .SingleOrDefaultAsync(x => x.Id == id, ctn);

            if (foundUser == null)
                throw OwnError.CanNotFindUser.ToException($"User with id = {id} not found");

            foundUser.Surname = request.Surname;
            foundUser.FirstName = request.FirstName;
            foundUser.LastName = request.LastName;
            foundUser.Email = request.Email;
            foundUser.RoleId = request.RoleId;

            await _userRepository.SaveChanges(ctn);

            return new UpdateUserResponse(
                Login: foundUser.Login,
                FirstName: foundUser.FirstName,
                LastName: foundUser.LastName,
                Surname: foundUser.Surname,
                RoleId: foundUser.RoleId,
                Email: foundUser.Email
            );
        }

        private void AssertRole(UpdateUserRequest request)
        {
            var findRole = _roleRepository.Query.SingleOrDefaultAsync(x => x.Id == request.RoleId);
            if (findRole == null)
                throw OwnError.CanNotFindRole.ToException($"Role with id {request.RoleId} not found in db");
        }
    }
}
