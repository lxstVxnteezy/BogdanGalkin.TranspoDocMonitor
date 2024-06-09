using ComStar.ElDabaa.Service.Core.Http.HttpAccessor;
using Microsoft.EntityFrameworkCore;
using TranspoDocMonitor.Service.Contracts.Exceptions;
using TranspoDocMonitor.Service.Contracts.Pass.Update;
using TranspoDocMonitor.Service.Core.Exception;
using TranspoDocMonitor.Service.DataContext.DataAccess.Repositories;
using TranspoDocMonitor.Service.Domain.Library.Entities;

namespace TranspoDocMonitor.Service.HTTP.Handlers.Methods.Passes
{

    public interface IUpdatePassHandler : IHandler
    {
        Task<UpdatePassResponse> Handle(Guid id, UpdatePassRequest request, CancellationToken ctn);
    }

    internal class UpdatePassHandler : IUpdatePassHandler
    {
        private readonly IRepository<Pass> _passRepository;
        private readonly IUserIdentityProvider _userIdentityProvider;

        public UpdatePassHandler(IRepository<Pass> passRepository, IUserIdentityProvider userIdentityProvider)
        {
            _passRepository = passRepository;
            _userIdentityProvider = userIdentityProvider;
        }

        public async Task<UpdatePassResponse> Handle(Guid id, UpdatePassRequest request, CancellationToken ctn)
        {


            var foundPass = await _passRepository.Query
                .Include(x => x.Vehicle).ThenInclude(x => x.User)
                .SingleOrDefaultAsync(x => x.Id == id, ctn);
            IsExist(foundPass);
            IsValidUser(foundPass);

            foundPass.ExDateTime = request.ExDateTime;
            foundPass.From = request.From; 
            

            
            EnsureUniqueVehicleIdFrom(request, foundPass.Vehicle);
            await _passRepository.SaveChanges(ctn);



            return new UpdatePassResponse
            (PassNumber: foundPass.PassNumber,
                ExDateTime: foundPass.ExDateTime,
                VehicleId: foundPass.VehicleId,
                From: foundPass.From);
        }




        private void IsExist(Pass foundPass)
        {
            if (foundPass == null)
                throw OwnError.CanNotFindPass.ToException("Cannot find pass");
        }


        private void IsValidUser(Pass foundPass)
        {
            if (foundPass.Vehicle.UserId != _userIdentityProvider.GetCurrentUserId())
                throw OwnError.CanNotAccess.ToException("shown in access");
        }

        private void EnsureUniqueVehicleIdFrom(UpdatePassRequest request, Domain.Library.Entities.Vehicle foundVehicle)
        {
            var existingPass = _passRepository.Query
                .SingleOrDefault(p => p.VehicleId == foundVehicle.Id && p.From == request.From);

            if (existingPass != null)
                throw OwnError.CanNotCreateTransportDocument.ToException("A pass with this 'From' and 'VehicleId' already exists.");
        }

    }
}
