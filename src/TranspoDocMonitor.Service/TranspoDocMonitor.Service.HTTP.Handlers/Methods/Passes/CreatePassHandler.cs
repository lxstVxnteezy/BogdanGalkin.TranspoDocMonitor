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
            var foundVehicle = await _vehicleRepository.Query
                .SingleOrDefaultAsync(x => x.Id == id, ctn);

            ExistVehicle(foundVehicle);
            IsValidUser(foundVehicle);
            EnsureUniqueVehicleIdFrom(request, foundVehicle);
            EnsureUniquePass(request);


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

        private void EnsureUniqueVehicleIdFrom(CreatePassRequest request, Domain.Library.Entities.Vehicle foundVehicle)
        {
            var existingPass = _passRepository.Query
                .SingleOrDefault(p => p.VehicleId == foundVehicle.Id && p.From == request.From);

            if (existingPass != null)
                throw OwnError.CanNotCreateTransportDocument.ToException("A pass with this 'From' and 'VehicleId' already exists.");
        }

        private void EnsureUniquePass(CreatePassRequest request)
        {
            var existNumber = _passRepository.Query
                .SingleOrDefault(x => x.PassNumber == request.PassNumber);
            if (existNumber != null)
                throw OwnError.CanNotCreateTransportDocument.ToException("A pass with this 'number' already exists.");
        }

        private void ExistVehicle(Domain.Library.Entities.Vehicle foundVehicle)
        {
            if (foundVehicle == null)
                throw OwnError.CanNotFindVehicle.ToException("Cannot find vehicle");
        }

        private void IsValidUser(Domain.Library.Entities.Vehicle foundVehicle)
        {
            if (foundVehicle.UserId != _userIdentityProvider.GetCurrentUserId())
                throw OwnError.CanNotAccess.ToException("shown in access");
        }

    }

}
