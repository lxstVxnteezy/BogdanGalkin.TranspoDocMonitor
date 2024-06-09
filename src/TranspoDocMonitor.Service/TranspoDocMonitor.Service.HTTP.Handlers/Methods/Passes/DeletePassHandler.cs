using ComStar.ElDabaa.Service.Core.Http.HttpAccessor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranspoDocMonitor.Service.Contracts.Exceptions;
using TranspoDocMonitor.Service.Core.Exception;
using TranspoDocMonitor.Service.DataContext.DataAccess.Repositories;
using TranspoDocMonitor.Service.Domain.Library.Entities;

namespace TranspoDocMonitor.Service.HTTP.Handlers.Methods.Passes
{
    public interface IDeletePassHandler : IHandler
    {
        Task<ActionResult> Handle(Guid id, CancellationToken ctn);
    }
    internal class DeletePassHandler : IDeletePassHandler
    {
        private readonly IRepository<Pass> _passRepository;
        private readonly IUserIdentityProvider _userIdentityProvider;

        public DeletePassHandler(IRepository<Pass> passRepository, IUserIdentityProvider userIdentityProvider)
        {
            _passRepository = passRepository;
            _userIdentityProvider = userIdentityProvider;
        }

        public async Task<ActionResult> Handle(Guid id, CancellationToken ctn)
        {
            var foundPass = await _passRepository.Query
                .Include(x => x.Vehicle).ThenInclude(x => x.User)
                .SingleOrDefaultAsync(x => x.Id == id, ctn);
            IsExist(foundPass);
            IsValidUser(foundPass);

            _passRepository.Remove(foundPass);
            await _passRepository.SaveChanges(ctn);

            return new StatusCodeResult(204);
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
    }
}
