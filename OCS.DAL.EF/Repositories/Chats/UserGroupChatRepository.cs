using Microsoft.EntityFrameworkCore;
using OCS.DAL.EF.Context;
using OCS.DAL.Entities.Chats;
using OCS.DAL.Repositories.Contracts.Chats;
using System.Threading;
using System.Threading.Tasks;

namespace OCS.DAL.EF.Repositories.Chats
{
    public class UserGroupChatRepository : GenericRepository<int, UserGroupChat, OnlineChatServiceDbContext>, IUserGroupChatRepository
    {
        public UserGroupChatRepository(OnlineChatServiceDbContext context) : base(context)
        {
        }

        public async Task<UserGroupChat> GetUserGroupChatAsync(int chatId, string userId, CancellationToken ct)
        {
            return await DbContext.UserGroupChats.FirstOrDefaultAsync(t => t.UserId == userId && t.GroupChatId == chatId, ct);
        }
    }
}