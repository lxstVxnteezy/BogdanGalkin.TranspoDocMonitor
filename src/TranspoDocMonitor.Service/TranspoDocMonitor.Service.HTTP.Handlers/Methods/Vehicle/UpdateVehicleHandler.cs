using ComStar.ElDabaa.Service.Core.Http.HttpAccessor;
using Microsoft.EntityFrameworkCore;
using TranspoDocMonitor.Service.Contracts.Exceptions;
using TranspoDocMonitor.Service.Contracts.Vehicle.Update;
using TranspoDocMonitor.Service.Core.Exception;
using TranspoDocMonitor.Service.DataContext.DataAccess.Repositories;

namespace TranspoDocMonitor.Service.HTTP.Handlers.Methods.Vehicle
{
    public interface IUpdateVehicleHandler : IHandler
    {
        public Task<UpdateVehicleResponse> Handle(Guid id, UpdateVehicleRequest request, CancellationToken ctn);

    }
    internal class UpdateVehicleHandler : IUpdateVehicleHandler
    {
        private readonly IRepository<Domain.Library.Entities.Vehicle> _vehicleRepository;
        private readonly IUserIdentityProvider _userIdentityProvider;
        public UpdateVehicleHandler(IRepository<Domain.Library.Entities.Vehicle> vehicleRepository, IUserIdentityProvider userIdentityProvider)
        {
            _vehicleRepository = vehicleRepository;
            _userIdentityProvider = userIdentityProvider;
        }

        public async Task<UpdateVehicleResponse> Handle(Guid id, UpdateVehicleRequest request, CancellationToken ctn)
        {
            var foundVehicle = await _vehicleRepository.Query.Include(x => x.User).Include(x=>x.VehicleDiagnosticReport)
                .SingleOrDefaultAsync(x => x.Id == id, ctn);

            if (foundVehicle == null)
                throw OwnError.CanNotFindUser.ToException("User not found in db");
            if (foundVehicle.UserId != _userIdentityProvider.GetCurrentUserId())
                throw OwnError.CanNotAccess.ToException("Shown in access");

            foundVehicle.Make = request.Make;
            foundVehicle.Model = request.Model;
            foundVehicle.Year = request.Year;
            foundVehicle.AutoColor = request.Color;
            foundVehicle.RegistrationNumber = request.RegistrationNumber;
            foundVehicle.VehicleIdentificationNumber = request.VehicleIdentificationNumber;
            foundVehicle.EngineCapacity = request.EngineCapacity;
            foundVehicle.Price = request.Price;
            foundVehicle.VehicleDiagnosticReport.DiagnosticCardNumber = request.DiagnosticCardNumber;
            foundVehicle.VehicleDiagnosticReport.ExpirationDateOfIssue = request.ExpirationDateOfIssue;

            await _vehicleRepository.SaveChanges(ctn);

            return new UpdateVehicleResponse(
                Make: foundVehicle.Make,
                Model: foundVehicle.Model,
                Year: foundVehicle.Year,
                Color: foundVehicle.AutoColor,
                RegistrationNumber: foundVehicle.RegistrationNumber,
                VehicleIdentificationNumber: foundVehicle.VehicleIdentificationNumber,
                EngineCapacity: foundVehicle.EngineCapacity,
                Price: foundVehicle.Price,
                DiagnosticCardNumber: foundVehicle.VehicleDiagnosticReport.DiagnosticCardNumber,
                ExpirationDateOfIssue: foundVehicle.VehicleDiagnosticReport.ExpirationDateOfIssue
            );

        }
    }
}
