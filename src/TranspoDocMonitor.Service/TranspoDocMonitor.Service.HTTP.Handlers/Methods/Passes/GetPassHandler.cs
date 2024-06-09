using ComStar.ElDabaa.Service.Core.Http.HttpAccessor;
using Microsoft.EntityFrameworkCore;
using TranspoDocMonitor.Service.Contracts.Exceptions;
using TranspoDocMonitor.Service.Contracts.Pass.Get;
using TranspoDocMonitor.Service.Core.Exception;
using TranspoDocMonitor.Service.DataContext.DataAccess.Repositories;
using TranspoDocMonitor.Service.Domain.Identity;
using TranspoDocMonitor.Service.Domain.Library.Entities;

namespace TranspoDocMonitor.Service.HTTP.Handlers.Methods.Passes
{
    public interface IGetPassHandler : IHandler
    {
        Task<GetPassResponse[]> Handle(string email, CancellationToken ctn);
    }
    internal class GetPassHandler : IGetPassHandler
    {
        private readonly IRepository<Pass> _passRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Domain.Library.Entities.Vehicle> _vehicleRepository;

        private readonly IUserIdentityProvider _userIdentityProvider;

        public GetPassHandler(IRepository<Pass> passRepository, IUserIdentityProvider userIdentityProvider, IRepository<User> userRepository, IRepository<Domain.Library.Entities.Vehicle> vehicleRepository)
        {
            _passRepository = passRepository;
            _userIdentityProvider = userIdentityProvider;
            _userRepository = userRepository;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<GetPassResponse[]> Handle(string email, CancellationToken ctn)
        {
            var foundPasses = await _passRepository.Query
                .Include(x => x.Vehicle).ThenInclude(x => x.User)
                .Where(x => x.Vehicle.User.Email == email)
                .ToListAsync(ctn);

            ExistPass(foundPasses);
            IsValidUser(foundPasses);

            return foundPasses.Select(pass => new GetPassResponse(
                Id: pass.Id,
                PassNumber: pass.PassNumber,
                VehicleId: pass.VehicleId,
                From: pass.From
            )).ToArray();
        }

        private void ExistPass(List<Pass> foundPasses)
        {
            if (foundPasses == null || !foundPasses.Any())
                throw OwnError.CanNotFindPass.ToException("cannot find pass");

        }
        private void IsValidUser(List<Pass> foundPasses)
        {
            var currentUserId = _userIdentityProvider.GetCurrentUserId();
            if (!foundPasses.All(pass => pass.Vehicle.UserId == currentUserId))
            {
                throw OwnError.CanNotAccess.ToException("shown in access");
            }
        }
    }
}
