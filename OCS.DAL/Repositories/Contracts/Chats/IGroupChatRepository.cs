using OCS.DAL.Entities.Chats;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OCS.DAL.Repositories.Contracts.Chats
{
    public interface IGroupChatRepository : IGenericRepository<int, GroupChat>
    {
        Task<ICollection<GroupChat>> GetChatsByUserIdAsync(string userId, CancellationToken ct);
    }
}