using ComStar.ElDabaa.Service.Core.Http.HttpAccessor;
using Microsoft.EntityFrameworkCore;
using TranspoDocMonitor.Service.Contracts.Exceptions;
using TranspoDocMonitor.Service.Contracts.TransportDocument.Create;
using TranspoDocMonitor.Service.Core.Exception;
using TranspoDocMonitor.Service.DataContext.DataAccess.Repositories;
using TranspoDocMonitor.Service.Domain.Library.StagingTables;

namespace TranspoDocMonitor.Service.HTTP.Handlers.Methods.TransportDocuments
{
    public interface ICreateTransportDocumentHandler : IHandler
    {
        public Task<CreateTransportDocumentResponse> Handle(Guid id, CreateTransportDocumentRequest request, CancellationToken ctn);

    }
    internal class CreateTransportDocumentHandler : ICreateTransportDocumentHandler
    {
        private readonly IRepository<VehicleDocument> _vehicleDocumentRepository;
        private readonly IUserIdentityProvider _userIdentityProvider;
        private readonly IRepository<Domain.Library.Entities.Vehicle> _vehicleRepository;


        public CreateTransportDocumentHandler(
            IRepository<VehicleDocument> vehicleDocumentRepository,
            IUserIdentityProvider userIdentityProvider,
            IRepository<Domain.Library.Entities.Vehicle> vehicleRepository)
        {
            _vehicleDocumentRepository = vehicleDocumentRepository;
            _userIdentityProvider = userIdentityProvider;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<CreateTransportDocumentResponse> Handle(Guid id, CreateTransportDocumentRequest request, CancellationToken ctn)
        {
            var foundVehicle = await _vehicleRepository.Query
                .SingleOrDefaultAsync(x => x.Id == id, ctn);
            ExistVehicle(foundVehicle);
            IsValidUser(foundVehicle);

            var newTransportDocument = new VehicleDocument()
            {
                Id = Guid.NewGuid(),
                Beneficiary = request.Beneficiary,
                ContractNumberCCI = request.ContractNumberCCI,
                Insurance = request.Insurance,
                NumberSeriesCCLI = request.NumberSeriesCCLI,
                Policyholder = request.Policyholder,
                VehicleId = foundVehicle.Id,
                СoverageAmount = request.СoverageAmount

            };
            _vehicleDocumentRepository.Add(newTransportDocument);
            await _vehicleDocumentRepository.SaveChanges(ctn);

            return new CreateTransportDocumentResponse(id: newTransportDocument.Id);
        }

        private void IsValidUser(Domain.Library.Entities.Vehicle foundVehicle)
        {
            if (foundVehicle.UserId != _userIdentityProvider.GetCurrentUserId())
                throw OwnError.CanNotAccess.ToException("shown in access");
        }
        private void ExistVehicle(Domain.Library.Entities.Vehicle foundVehicle)
        {
            if (foundVehicle == null)
                throw OwnError.CanNotFindVehicle.ToException("Cannot find vehicle");
        }
    }
}
