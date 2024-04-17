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
        public Task<CreateTransportDocumentResponse> Handle(CreateTransportDocumentRequest request, CancellationToken ctn);

    }
    internal class CreateTransportDocumentHandler : ICreateTransportDocumentHandler
    {
        private readonly IRepository<VehicleDocument> _vehicleDocumentRepository;
        private readonly IRepository<UserVehicle> _userVehicleRepository;
        private readonly IUserIdentityProvider _userIdentityProvider;


        public CreateTransportDocumentHandler(IRepository<VehicleDocument> vehicleDocumentRepository, IRepository<UserVehicle> userVehicleRepository, IUserIdentityProvider userIdentityProvider)
        {
            _vehicleDocumentRepository = vehicleDocumentRepository;
            _userVehicleRepository = userVehicleRepository;
            _userIdentityProvider = userIdentityProvider;
        }

        public async Task<CreateTransportDocumentResponse> Handle(CreateTransportDocumentRequest request, CancellationToken ctn)
        {
            AssertExistDocumentType(request);
            var existingUserVehicle = await GetOrCreateUserVehicleAsync(request);

            var newTransportDocument = new VehicleDocument()
            {
                Id = Guid.NewGuid(),
                DocumentNumber = request.DocumentNumber,
                DateOfIssue = request.DateOfIssue,
                ExpirationDateOfIssue = request.ExpirationDateOfIssue,
                DictionaryDocumentTypeId = request.DictionaryDocumentTypeId,
                UserVehicleId = existingUserVehicle.Id

            };
            _vehicleDocumentRepository.Add(newTransportDocument);
            await _vehicleDocumentRepository.SaveChanges(ctn);

            return new CreateTransportDocumentResponse(newTransportDocument.Id);
        }





        private async Task<UserVehicle> GetOrCreateUserVehicleAsync(CreateTransportDocumentRequest request)
        {
            var queryable = _userVehicleRepository.Query;

            var existingUserVehicle = await queryable
                .Include(x => x.VehicleDocuments)
                .FirstOrDefaultAsync(x => x.UserId == _userIdentityProvider.GetCurrentUserId() && x.VehicleId == request.VehicleId);

            if (existingUserVehicle == null)
            {
                existingUserVehicle = new UserVehicle()
                {
                    Id = Guid.NewGuid(),
                    UserId = _userIdentityProvider.GetCurrentUserId(),
                    VehicleId = request.VehicleId
                };
                _userVehicleRepository.Add(existingUserVehicle);
            }

            return existingUserVehicle;

        }

        private void AssertExistDocumentType(CreateTransportDocumentRequest request)
        {
            var isExistTransportDocument = _vehicleDocumentRepository.Query.Any(x => x.DocumentNumber == request.DocumentNumber);
            if (isExistTransportDocument)
                throw OwnError
                    .CanNotCreateTransportDocument
                    .ToException
                    ($"Document with number " +
                     $"{request.DocumentNumber} already exist");
        }



    }
}
