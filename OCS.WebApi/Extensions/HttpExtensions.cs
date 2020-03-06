using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;

namespace OCS.WebApi.Extensions
{
    public static class HttpExtensions
    {
        public static string GetJwtToken(this HttpRequest request)
        {
            var authHeader = request.Headers[HeaderNames.Authorization].ToString();
            var token = authHeader.Replace("Bearer", string.Empty, StringComparison.InvariantCultureIgnoreCase).Trim();
            return token;
        }
    }
}