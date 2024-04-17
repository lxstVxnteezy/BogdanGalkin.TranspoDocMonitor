using TranspoDocMonitor.Service.Contracts.Exceptions;
using TranspoDocMonitor.Service.Contracts.Vehicle.Create;
using TranspoDocMonitor.Service.Core.Exception;
using TranspoDocMonitor.Service.DataContext.DataAccess.Repositories;

namespace TranspoDocMonitor.Service.HTTP.Handlers.Methods.Vehicle
{

    public interface ICreateVehicleHandler : IHandler
    {
        public Task<CreateVehicleResponse> Handle(CreateVehicleRequest request, CancellationToken ctn);
    }

    internal class CreateVehicleHandler: ICreateVehicleHandler
    {
        private readonly IRepository<Domain.Library.Entities.Vehicle> _vehicleRepository;

        public CreateVehicleHandler(IRepository<Domain.Library.Entities.Vehicle> vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<CreateVehicleResponse> Handle(CreateVehicleRequest request, CancellationToken ctn)
        {
            AssertExistRegistrationNumber(request);
            var newVehicle = new Domain.Library.Entities.Vehicle()
            {
                Id = Guid.NewGuid(),
                Make = request.Make,
                AutoColor = request.Color,
                Model = request.Model,
                RegistrationNumber = request.RegistrationNumber,
                Year = request.Year
            };

            _vehicleRepository.Add(newVehicle);
            await _vehicleRepository.SaveChanges(ctn);

            return new CreateVehicleResponse(id: newVehicle.Id);
        }

        private void AssertExistRegistrationNumber(CreateVehicleRequest request)
        {
            var isExistVehicle = _vehicleRepository.Query.Any(x=>x.RegistrationNumber == request.RegistrationNumber);
            if (isExistVehicle)
                throw OwnError.CanNotCreateVehicle.ToException($"Vehicle with registation number = {request.RegistrationNumber} already exists");
        }
    }
}
 
