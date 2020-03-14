using OCS.DAL.Entities.Chats;
using OCS.DAL.Entities.Views.Chats;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OCS.DAL.Queries.Chats;

namespace OCS.DAL.Repositories.Contracts.Chats
{
    public interface IPrivateChatRepository : IGenericRepository<int, PrivateChat>
    {
        Task<ICollection<PrivateChat>> GetChatsByUserIdAsync(string userId, CancellationToken ct);

        Task<PrivateChat> GetChatByUserIdAsync(string userId, string invitedUserId, CancellationToken ct);

        Task<ICollection<PrivateChatView>> GetPrivateChatsAsync(GetPrivateChatsQuery query, CancellationToken ct);
    }
}