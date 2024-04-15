using TranspoDocMonitor.Service.Contracts.Create;
using TranspoDocMonitor.Service.Contracts.Exceptions;
using TranspoDocMonitor.Service.Core.Authorization;
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
        public CreateUserHandler(
            IRepository<User> userRepository,
            IRepository<Role> roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken ctn)
        {
            AssertExistRole(request);
            AssertExistEmailUser(request);
            AssertExistLoginUser(request);

            var newUser = new User()
            {
                Login = request.Login,
                FirstName = request.FirstName,
                Hash = request.Password.ComputeHash(),
                LastName = request.LastName,
                Id = Guid.NewGuid(),
                Surname = request.Surname,
                RoleId = request.RoleId
            };
            _userRepository.Add(newUser);
            await _userRepository.SaveChanges(ctn);
            return new CreateUserResponse(id: newUser.Id);

        }

        private void AssertExistRole(CreateUserRequest request)
        {
            var role = _roleRepository.FoundById(request.RoleId);
            if (role == null)
                throw OwnError.CanNotFindRole.ToException("Not find role in db");
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
