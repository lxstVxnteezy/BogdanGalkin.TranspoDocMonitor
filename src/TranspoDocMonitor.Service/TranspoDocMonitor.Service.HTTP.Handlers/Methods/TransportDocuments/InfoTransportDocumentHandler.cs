using ComStar.ElDabaa.Service.Core.Http.HttpAccessor;
using Microsoft.EntityFrameworkCore;
using TranspoDocMonitor.Service.Contracts.Exceptions;
using TranspoDocMonitor.Service.Contracts.TransportDocument.GetDocument;
using TranspoDocMonitor.Service.Core.Exception;
using TranspoDocMonitor.Service.DataContext.DataAccess.Repositories;
using TranspoDocMonitor.Service.Domain.Identity;
using TranspoDocMonitor.Service.Domain.Library.StagingTables;

namespace TranspoDocMonitor.Service.HTTP.Handlers.Methods.TransportDocuments
{
    public interface IGetTransportDocumentHandler : IHandler
    {
        public Task<Contracts.TransportDocument.GetDocument.InfoTransportDocumentResponse[]> Handle(string email, CancellationToken ctn);

    }

    internal class InfoTransportDocumentHandler : IGetTransportDocumentHandler
    {
        private readonly IRepository<VehicleDocument> _vehicleDocumentRepository;
        private readonly IUserIdentityProvider _userIdentityProvider;
        public InfoTransportDocumentHandler(
            IRepository<VehicleDocument> vehicleDocumentRepository, 
            IRepository<User> userRepository, 
            IUserIdentityProvider userIdentityProvider)
        {
            _vehicleDocumentRepository = vehicleDocumentRepository;
            _userIdentityProvider = userIdentityProvider;
        }



        private void ExistPass(List<VehicleDocument> foundPasses)
        {
            if (foundPasses == null || !foundPasses.Any())
                throw OwnError.CanNotFindPass.ToException("cannot find pass");

        }
        private void IsValidUser(List<VehicleDocument> foundPasses)
        {
            var currentUserId = _userIdentityProvider.GetCurrentUserId();
            if (!foundPasses.All(pass => pass.Vehicle.UserId == currentUserId))
            {
                throw OwnError.CanNotAccess.ToException("shown in access");
            }
        }

        public async Task<Contracts.TransportDocument.GetDocument.InfoTransportDocumentResponse[]> Handle(string email, CancellationToken ctn)
        {
            var foundDocuments = await _vehicleDocumentRepository.Query
                .Include(x => x.Vehicle).ThenInclude(x => x.User)
                .Where(x => x.Vehicle.User.Email == email).
                ToListAsync(ctn);

            ExistPass(foundDocuments);
            IsValidUser(foundDocuments);

            return foundDocuments.Select(document => new InfoTransportDocumentResponse(
                Policyholder: document.Policyholder,
                Beneficiary: document.Beneficiary,
                ContractNumberCCI: document.ContractNumberCCI,
                NumberSeriesCCLI: document.NumberSeriesCCLI,
                Insurance: document.Insurance,
                СoverageAmount: document.СoverageAmount,
                VehicleId: document.VehicleId
            )).ToArray();
        }
    }
}
