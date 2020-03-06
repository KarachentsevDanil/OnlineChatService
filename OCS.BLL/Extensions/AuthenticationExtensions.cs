using Microsoft.IdentityModel.Tokens;
using OCS.BLL.Configurations;
using System.Text;

namespace OCS.BLL.Extensions
{
    public static class AuthenticationExtensions
    {
        public static SecurityKey GetSymmetricSecurityKey(this AuthenticationConfiguration configuration)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.TokenKey));
        }
    }
}