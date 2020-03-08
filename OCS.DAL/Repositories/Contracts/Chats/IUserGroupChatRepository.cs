using System.Threading;
using System.Threading.Tasks;
using OCS.DAL.Entities.Chats;

namespace OCS.DAL.Repositories.Contracts.Chats
{
    public interface IUserGroupChatRepository : IGenericRepository<int, UserGroupChat>
    {
        Task<UserGroupChat> GetUserGroupChatAsync(int chatId, string userId, CancellationToken ct);
    }
}
