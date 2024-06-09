using ComStar.ElDabaa.Service.Core.Http.HttpAccessor;
using Microsoft.EntityFrameworkCore;
using TranspoDocMonitor.Service.Contracts.Exceptions;
using TranspoDocMonitor.Service.Contracts.TransportDocument.Update;
using TranspoDocMonitor.Service.Core.Exception;
using TranspoDocMonitor.Service.DataContext.DataAccess.Repositories;
using TranspoDocMonitor.Service.Domain.Identity;
using TranspoDocMonitor.Service.Domain.Library.StagingTables;

namespace TranspoDocMonitor.Service.HTTP.Handlers.Methods.TransportDocuments
{
    public interface IUpdateTransportDocumentHandler : IHandler
    {
        Task<UpdateTransportDocumentResponse> Handle(Guid id, UpdateTransportDocumentRequest request, CancellationToken ctn);

    }

    internal class UpdateTransportDocumentHandler : IUpdateTransportDocumentHandler
    {
        private readonly IRepository<VehicleDocument> _vehicleDocumentRepository;
        private readonly IUserIdentityProvider _userIdentityProvider;
        public UpdateTransportDocumentHandler(
            IRepository<VehicleDocument> vehicleDocumentRepository,
            IRepository<User> userRepository,
            IUserIdentityProvider userIdentityProvider)
        {
            _vehicleDocumentRepository = vehicleDocumentRepository;
            _userIdentityProvider = userIdentityProvider;
        }
        public async Task<UpdateTransportDocumentResponse> Handle(Guid id, UpdateTransportDocumentRequest request, CancellationToken ctn)
        {
            var foundDocument = await _vehicleDocumentRepository.Query
                .Include(x => x.Vehicle)
                .ThenInclude(x => x.User)
                .SingleOrDefaultAsync(x => x.Id == id, ctn);

            IsExistTransportDocument(foundDocument);
            IsValidUser(foundDocument);

            foundDocument.Policyholder = request.Policyholder;
            foundDocument.Beneficiary = request.Beneficiary;
            foundDocument.ContractNumberCCI = request.ContractNumberCCI;
            foundDocument.NumberSeriesCCLI = request.NumberSeriesCCLI;
            foundDocument.Insurance = request.Insurance;
            foundDocument.СoverageAmount = request.СoverageAmount;

            await _vehicleDocumentRepository.SaveChanges(ctn);

            return new UpdateTransportDocumentResponse(
                Policyholder: foundDocument.Policyholder,
                Beneficiary: foundDocument.Beneficiary,
                ContractNumberCCI: foundDocument.ContractNumberCCI,
                NumberSeriesCCLI: foundDocument.NumberSeriesCCLI,
                Insurance: foundDocument.Insurance,
                СoverageAmount: foundDocument.СoverageAmount,
                VehicleId: foundDocument.VehicleId
                );
        }


        private void IsValidUser(VehicleDocument foundDocument)
        {
            if (foundDocument.Vehicle.UserId != _userIdentityProvider.GetCurrentUserId())
                throw OwnError.CanNotAccess.ToException("shown in access");
        }
        private void IsExistTransportDocument(VehicleDocument foundDocument)
        {
            if (foundDocument == null)
                throw OwnError.CanNotCreateTransportDocument.ToException("cannot not find document in db");
        }
    }
}
