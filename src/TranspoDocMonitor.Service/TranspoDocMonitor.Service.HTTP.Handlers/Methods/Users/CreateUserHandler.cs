
using TranspoDocMonitor.Service.Contracts.Create;
using TranspoDocMonitor.Service.Contracts.Exceptions;
using TranspoDocMonitor.Service.Core.Exception;
using TranspoDocMonitor.Service.DataContext.DataAccess.Repositories;
using TranspoDocMonitor.Service.Domain.Identity;

namespace TranspoDocMonitor.Service.HTTP.Handlers.Methods.Users
{
    public interface ICreateUserHandler : IHandler
    {
        Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken ctn);
    }




    internal class CreateUserHandler : ICreateUserHandler
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;





        public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken ctn)
        {
            throw new NotImplementedException();
        }












        private void AssertRole(CreateUserRequest request)
        {
            var role = _roleRepository.FoundById(request.RoleId);
            if (role == null)
                throw OwnError.CanNotFindRole.ToException(userMessage: "Role not found");
        }
        private void AssertExistLoginUser(CreateUserRequest request)
        {
            var isExistUser = _userRepository.Query.Any(x => x.Login == request.Login);
            if (isExistUser)
                throw OwnError.UnableToCreateUser.ToException($"User with login = {request.Login} already exists");
        }
        private void AssertExistEmailUser(CreateUserRequest request)
        {
            var isExistUser = _userRepository.Query.Any(x => x.Login == request.Login);
            if (isExistUser)
                throw OwnError.UnableToCreateUser.ToException($"User with login = {request.Login} already exists");
        }


    }
}
