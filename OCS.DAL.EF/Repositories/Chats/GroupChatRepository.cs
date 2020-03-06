using Microsoft.EntityFrameworkCore;
using OCS.DAL.EF.Context;
using OCS.DAL.Entities.Chats;
using OCS.DAL.Repositories.Contracts.Chats;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OCS.DAL.EF.Repositories.Chats
{
    public class GroupChatRepository : GenericRepository<int, GroupChat, OnlineChatServiceDbContext>, IGroupChatRepository
    {
        public GroupChatRepository(OnlineChatServiceDbContext context) : base(context)
        {
        }

        public async Task<ICollection<GroupChat>> GetChatsByUserIdAsync(string userId, CancellationToken ct)
        {
            return await DbContext.GroupChats
                .Include(t => t.Users)
                .Where(t => t.Users.Any(u => u.UserId == userId))
                .ToListAsync(ct);
        }
    }
}