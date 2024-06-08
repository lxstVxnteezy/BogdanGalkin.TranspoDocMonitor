
using Microsoft.EntityFrameworkCore;
using TranspoDocMonitor.Service.Contracts.Exceptions;
using TranspoDocMonitor.Service.Contracts.User.Info;
using TranspoDocMonitor.Service.Core.Exception;
using TranspoDocMonitor.Service.DataContext.DataAccess.Repositories;
using TranspoDocMonitor.Service.Domain.Identity;

namespace TranspoDocMonitor.Service.HTTP.Handlers.Methods.Users
{
    public interface IInfoUserHandler : IHandler
    {
        public Task<InfoUserResponse> Handle(Guid id, CancellationToken ctn);
    }

    internal class InfoUserHandler: IInfoUserHandler
    {
        private readonly IRepository<User> _userRepository;

        public InfoUserHandler(IRepository<User> userRepository, IRepository<Domain.Library.Entities.Vehicle> vehicleRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<InfoUserResponse> Handle(Guid id, CancellationToken ctn)
        {
            var foundUser = await _userRepository.Query
                .Include(x => x.Role)
                .Include(x => x.Vehicles)
                .SingleOrDefaultAsync(x => x.Id == id, ctn);
            if (foundUser == null)
                throw OwnError.CanNotFindUser.ToException($"User with id = {id} not found");

            return new InfoUserResponse(
                Id: foundUser.Id,
                Login: foundUser.Login,
                FirstName: foundUser.FirstName,
                LastName: foundUser.LastName,
                Surname: foundUser.Surname,
                RoleId: foundUser.RoleId,
                Email:foundUser.Email,
                Vehicles: foundUser.Vehicles.ToArray());
        }
    }
}
