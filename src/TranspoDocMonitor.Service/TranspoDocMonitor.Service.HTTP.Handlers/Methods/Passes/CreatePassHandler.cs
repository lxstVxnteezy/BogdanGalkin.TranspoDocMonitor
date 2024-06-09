using ComStar.ElDabaa.Service.Core.Http.HttpAccessor;
using Microsoft.EntityFrameworkCore;
using TranspoDocMonitor.Service.Contracts.Exceptions;
using TranspoDocMonitor.Service.Contracts.Pass.Create;
using TranspoDocMonitor.Service.Core.Exception;
using TranspoDocMonitor.Service.DataContext.DataAccess.Repositories;
using TranspoDocMonitor.Service.Domain.Library.Entities;

namespace TranspoDocMonitor.Service.HTTP.Handlers.Methods.Passes
{
    public interface ICreatePassHandler : IHandler
    {
        Task<CreatePassResponse> Handle(Guid id, CreatePassRequest request, CancellationToken ctn);
    }

    internal class CreatePassHandler : ICreatePassHandler
    {
        private readonly IRepository<Domain.Library.Entities.Vehicle> _vehicleRepository;
        private readonly IRepository<Pass> _passRepository;
        private readonly IUserIdentityProvider _userIdentityProvider;

        public CreatePassHandler(
            IRepository<Domain.Library.Entities.Vehicle> vehicleRepository,
            IRepository<Pass> passRepository,
            IUserIdentityProvider userIdentityProvider)
        {
            _vehicleRepository = vehicleRepository;
            _passRepository = passRepository;
            _userIdentityProvider = userIdentityProvider;
        }

        public async Task<CreatePassResponse> Handle(Guid id, CreatePassRequest request, CancellationToken ctn)
        {
            var foundVehicle = await _vehicleRepository.Query.SingleOrDefaultAsync(x => x.Id == id, ctn);

            if (foundVehicle == null)
                throw OwnError.CanNotFindVehicle.ToException("Cannot find vehicle");

            if (foundVehicle.UserId != _userIdentityProvider.GetCurrentUserId())
                throw OwnError.CanNotAccess.ToException("shown in access");

            var newPass = new Pass
            {
                Id = Guid.NewGuid(),
                From = request.From,
                PassNumber = request.PassNumber,
                ExDateTime = DateTime.UtcNow,
                VehicleId = foundVehicle.Id
            };

            _passRepository.Add(newPass);
            await _passRepository.SaveChanges(ctn);


            return new CreatePassResponse(
                Id: newPass.Id,
                PassNumber: newPass.PassNumber,
                VehicleId: newPass.VehicleId,
                From: newPass.From
            );

        }
    }

}
