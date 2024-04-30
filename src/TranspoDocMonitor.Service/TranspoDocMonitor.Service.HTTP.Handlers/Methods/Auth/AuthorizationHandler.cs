using System.Security.Claims;
using ComStar.ElDabaa.Service.Core.Http.HttpAccessor;
using Microsoft.EntityFrameworkCore;
using TranspoDocMonitor.Service.Contracts.Shared.Auth;
using TranspoDocMonitor.Service.Core.Authorization;
using TranspoDocMonitor.Service.DataContext.DataAccess.Repositories;
using TranspoDocMonitor.Service.Domain.Identity;

namespace TranspoDocMonitor.Service.HTTP.Handlers.Methods.Auth
{
    public interface IAuthorizationHandler : IHandler
    {
        Task<AuthResponse> Handle(AuthRequest request, CancellationToken ctn);
    }


    public class AuthorizationHandler : IAuthorizationHandler
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUserIdentityProvider _userIdentityProvider;
        public AuthorizationHandler(IRepository<User> userRepository, IUserIdentityProvider userIdentityProvider)
        {
            _userRepository = userRepository;
            _userIdentityProvider = userIdentityProvider;
        }

        public async Task<AuthResponse> Handle(AuthRequest request, CancellationToken ctn)
        {
            var user = await _userRepository.Query
                .Include(x => x.Role)
                .SingleOrDefaultAsync(x => x.Login == request.Login, ctn);

            if (user == null)
                throw CreateAuthException();

            var resultCheckingPassword = PasswordBuilder.Equal(request.Password, user.Hash);
            if (!resultCheckingPassword)
                throw CreateAuthException();

            var startLifeToken = DateTime.UtcNow;
            var endLifeToken = startLifeToken.Add(TimeSpan.FromDays(1));

            var claimsIdentity = new ClaimsIdentity(
                claims: new[]
                {
                    new Claim(ClaimTypes.Name, request.Login),
                    new Claim(ClaimTypes.Role, user.Role.Name),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                },
                authenticationType: "Token");

            var encodedJwt = JwtTokenBuilder.Build(claimsIdentity, startLifeToken, endLifeToken);

            _userIdentityProvider.GetHttpContext().Response.Cookies.Append("cookies", encodedJwt);

            return new AuthResponse(token: encodedJwt);
        }
        private static UnauthorizedAccessException CreateAuthException() => new("Auth error");

    }
}
