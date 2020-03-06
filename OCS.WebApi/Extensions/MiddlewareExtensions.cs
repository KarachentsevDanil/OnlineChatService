using Microsoft.AspNetCore.Builder;
using OCS.WebApi.Middlewares;

namespace OCS.WebApi.Extensions
{

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}