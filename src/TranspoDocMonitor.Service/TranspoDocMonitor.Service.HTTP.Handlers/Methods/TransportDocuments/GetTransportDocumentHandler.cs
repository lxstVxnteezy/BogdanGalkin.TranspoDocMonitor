using TranspoDocMonitor.Service.Contracts.TransportDocument.GetDocument;
using TranspoDocMonitor.Service.DataContext.DataAccess.Repositories;
using TranspoDocMonitor.Service.Domain.Identity;
using TranspoDocMonitor.Service.Domain.Library.StagingTables;

namespace TranspoDocMonitor.Service.HTTP.Handlers.Methods.TransportDocuments
{
    public interface IGetTransportDocumentHandler : IHandler
    {
        public Task<InfoTransportDocumentResponse[]> Handle(InfoTransportDocumentRequest request, CancellationToken ctn);

    }

    internal class GetTransportDocumentHandler : IGetTransportDocumentHandler
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<VehicleDocument> _vehicleDocumentRepository;
        public GetTransportDocumentHandler(IRepository<VehicleDocument> vehicleDocumentRepository, IRepository<User> userRepository)
        {
            _vehicleDocumentRepository = vehicleDocumentRepository;
            _userRepository = userRepository;
        }



        public async Task<InfoTransportDocumentResponse[]> Handle(InfoTransportDocumentRequest request, CancellationToken ctn)
        {
            return null;
        }
    }
}
