using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace TranspoDocMonitor.Service.Core.Authorization
{
    public static class JwtTokenBuilder
    {
        public static string Build(ClaimsIdentity claim, DateTime startLifeToken, DateTime endLifeToken)
        {
            var handler = new JwtSecurityTokenHandler();

            var token = handler.CreateJwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                notBefore: startLifeToken,
                subject: claim,
                expires: endLifeToken,
                issuedAt: null,
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return handler.WriteToken(token);
        }
    }
}
