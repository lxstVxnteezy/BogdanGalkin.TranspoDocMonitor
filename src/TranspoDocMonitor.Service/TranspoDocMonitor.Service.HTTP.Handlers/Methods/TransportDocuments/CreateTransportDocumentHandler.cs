using ComStar.ElDabaa.Service.Core.Http.HttpAccessor;
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
        private readonly IUserIdentityProvider _userIdentityProvider;


        public CreateTransportDocumentHandler(IRepository<VehicleDocument> vehicleDocumentRepository, IUserIdentityProvider userIdentityProvider)
        {
            _vehicleDocumentRepository = vehicleDocumentRepository;
            _userIdentityProvider = userIdentityProvider;
        }

        public async Task<CreateTransportDocumentResponse> Handle(CreateTransportDocumentRequest request, CancellationToken ctn)
        {

            var newTransportDocument = new VehicleDocument()
            {
                Id = Guid.NewGuid(),
                DateOfIssue = request.DateOfIssue,
                ExpirationDateOfIssue = request.ExpirationDateOfIssue,

            };
            _vehicleDocumentRepository.Add(newTransportDocument);
            await _vehicleDocumentRepository.SaveChanges(ctn);

            return new CreateTransportDocumentResponse(newTransportDocument.Id);
        }

        

    }
}
