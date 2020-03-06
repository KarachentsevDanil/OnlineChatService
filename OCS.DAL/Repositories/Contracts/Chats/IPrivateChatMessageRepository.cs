using OCS.DAL.Entities.Chats;
using OCS.DAL.Entities.Chats.Queries;
using OCS.DAL.Models;
using System.Threading;
using System.Threading.Tasks;

namespace OCS.DAL.Repositories.Contracts.Chats
{
    public interface IPrivateChatMessageRepository : IGenericRepository<int, PrivateChatMessage>
    {
        Task<PagedItemResultModel<PrivateChatMessage>> GetMessagesAsync(GetPagedMessagesQuery query, CancellationToken ct);
    }
}
