using Microsoft.AspNetCore.SignalR;
using OCS.WebApi.Extensions;

namespace OCS.WebApi.SignalR.PrincipalProviders
{
    public class ChatUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User.GetUserId();
        }
    }
}