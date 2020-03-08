using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using Microsoft.Extensions.Primitives;

namespace OCS.WebApi.Extensions
{
    public static class HttpExtensions
    {
        public static string GetJwtToken(this HttpRequest request)
        {
            if (request.Headers.TryGetValue(HeaderNames.Authorization, out StringValues authHeader))
            {
                string bearerPattern = "Bearer ";

                string auth = authHeader.ToString();

                if (!string.IsNullOrEmpty(auth) && auth.StartsWith(
                        bearerPattern,
                        StringComparison.InvariantCulture))
                {
                    return auth.Replace(
                        bearerPattern,
                        string.Empty,
                        StringComparison.InvariantCulture);
                }
            }

            return string.Empty;
        }
    }
}