using Microsoft.EntityFrameworkCore;
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
        private readonly IRepository<UserVehicle> _userVehicleRepository;
        private readonly IRepository<VehicleDocument> _vehicleDocumentRepository;
        public GetTransportDocumentHandler(IRepository<VehicleDocument> vehicleDocumentRepository, IRepository<User> userRepository, IRepository<UserVehicle> userVehicleRepository)
        {
            _vehicleDocumentRepository = vehicleDocumentRepository;
            _userRepository = userRepository;
            _userVehicleRepository = userVehicleRepository;
        }



        public async Task<InfoTransportDocumentResponse[]> Handle(InfoTransportDocumentRequest request, CancellationToken ctn)
        {
            var user = await _userRepository.Query
                .Include(u => u.UserVehicles)
                .ThenInclude(uv => uv.Vehicle)
                .Include(u => u.UserVehicles)
                .ThenInclude(uv => uv.VehicleDocuments)
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return Array.Empty<InfoTransportDocumentResponse>();
            }

            var documents = user.UserVehicles
                .SelectMany(uv => uv.VehicleDocuments)
                .Select(vd => new InfoTransportDocumentResponse(
                    vd.DocumentNumber,
                    vd.DateOfIssue,
                    vd.ExpirationDateOfIssue,
                    vd.UserVehicleId,
                    vd.DictionaryDocumentTypeId))
                .ToArray();
            
            return documents;

        }
    }
}
