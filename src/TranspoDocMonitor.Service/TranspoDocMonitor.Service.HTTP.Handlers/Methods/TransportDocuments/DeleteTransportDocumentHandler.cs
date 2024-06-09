using ComStar.ElDabaa.Service.Core.Http.HttpAccessor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranspoDocMonitor.Service.Contracts.Exceptions;
using TranspoDocMonitor.Service.Core.Exception;
using TranspoDocMonitor.Service.DataContext.DataAccess.Repositories;
using TranspoDocMonitor.Service.Domain.Identity;
using TranspoDocMonitor.Service.Domain.Library.StagingTables;

namespace TranspoDocMonitor.Service.HTTP.Handlers.Methods.TransportDocuments
{
    public interface IDeleteTransportDocumentHandler : IHandler
    {
        Task<ActionResult> Handle(Guid id, CancellationToken ctn);
    }

    internal class DeleteTransportDocumentHandler : IDeleteTransportDocumentHandler
    {
        private readonly IRepository<VehicleDocument> _vehicleDocumentRepository;
        private readonly IUserIdentityProvider _userIdentityProvider;
        public DeleteTransportDocumentHandler(
            IRepository<VehicleDocument> vehicleDocumentRepository,
            IRepository<User> userRepository,
            IUserIdentityProvider userIdentityProvider)
        {
            _vehicleDocumentRepository = vehicleDocumentRepository;
            _userIdentityProvider = userIdentityProvider;
        }

        public async Task<ActionResult> Handle(Guid id, CancellationToken ctn)
        {
            var foundDocument = await _vehicleDocumentRepository.Query
                .Include(x => x.Vehicle)
                .ThenInclude(x => x.User)
                .SingleOrDefaultAsync(x => x.Id == id, ctn);

            IsExistTransportDocument(foundDocument);
            IsValidUser(foundDocument);

            _vehicleDocumentRepository.Remove(foundDocument);
            await _vehicleDocumentRepository.SaveChanges(ctn);
            
            return new StatusCodeResult(204);
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
