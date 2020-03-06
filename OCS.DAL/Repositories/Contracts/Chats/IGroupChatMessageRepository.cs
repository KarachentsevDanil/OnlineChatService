using OCS.DAL.Entities.Chats;
using OCS.DAL.Entities.Chats.Queries;
using OCS.DAL.Models;
using System.Threading;
using System.Threading.Tasks;

namespace OCS.DAL.Repositories.Contracts.Chats
{
    public interface IGroupChatMessageRepository : IGenericRepository<int, GroupChatMessage>
    {
        Task<PagedItemResultModel<GroupChatMessage>> GetMessagesAsync(GetPagedMessagesQuery query, CancellationToken ct);
    }
}
