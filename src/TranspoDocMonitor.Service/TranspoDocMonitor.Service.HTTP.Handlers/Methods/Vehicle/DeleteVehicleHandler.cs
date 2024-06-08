using ComStar.ElDabaa.Service.Core.Http.HttpAccessor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranspoDocMonitor.Service.Contracts.Exceptions;
using TranspoDocMonitor.Service.Core.Exception;
using TranspoDocMonitor.Service.DataContext.DataAccess.Repositories;

namespace TranspoDocMonitor.Service.HTTP.Handlers.Methods.Vehicle
{

    public interface IDeleteVehicleHandler : IHandler
    {
        public Task<StatusCodeResult> Handle(Guid id, CancellationToken ctn);

    }
    internal class DeleteVehicleHandler : IDeleteVehicleHandler
    {
        private readonly IUserIdentityProvider _userIdentityProvider;
        private readonly IRepository<Domain.Library.Entities.Vehicle> _vehicleRepository;
        public DeleteVehicleHandler(IUserIdentityProvider userIdentityProvider, IRepository<Domain.Library.Entities.Vehicle> vehicleRepository)
        {
            _userIdentityProvider = userIdentityProvider;
            _vehicleRepository = vehicleRepository;
        }


        public async Task<StatusCodeResult> Handle(Guid id, CancellationToken ctn)
        {
            var foundVehicle = await _vehicleRepository.Query.Include(x => x.User)
                .SingleOrDefaultAsync(x => x.Id == id, ctn);

            if (foundVehicle == null)
                throw OwnError.CanNotFindUser.ToException($"User with id {id} not found in db");

            if (foundVehicle.UserId != _userIdentityProvider.GetCurrentUserId())
                throw new Exception("error");
            _vehicleRepository.Remove(foundVehicle);
            await _vehicleRepository.SaveChanges(ctn);

            return new StatusCodeResult(204);
        }
    }
}
