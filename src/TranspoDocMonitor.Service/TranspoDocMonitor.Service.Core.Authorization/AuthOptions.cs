using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TranspoDocMonitor.Service.Core.Authorization
{
    internal class AuthOptions
    {
        public const string Issuer = "TranspoDocMonitorServer";

        public const string Audience = "TranspoDocMonitorClient";

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
            => new(Encoding.ASCII.GetBytes("2D4A614E645267556B58703273357638792F423F4428472B4B6250655368566D"));
    }
}
