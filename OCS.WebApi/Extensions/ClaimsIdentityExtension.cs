using OCS.DAL.Entities.Users;
using System.Security.Claims;

namespace OCS.WebApi.Extensions
{
    public static class ClaimsIdentityExtension
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return ((ClaimsIdentity)claimsPrincipal.Identity).FindFirst(nameof(User.Id))?.Value;
        }

        public static string GetUserEmail(this ClaimsPrincipal claimsPrincipal)
        {
            return ((ClaimsIdentity)claimsPrincipal.Identity).FindFirst(nameof(User.Id))?.Value;
        }
    }
}