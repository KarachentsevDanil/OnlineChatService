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
    public class PrivateChatRepository : GenericRepository<int, PrivateChat, OnlineChatServiceDbContext>, IPrivateChatRepository
    {
        public PrivateChatRepository(OnlineChatServiceDbContext context) : base(context)
        {
        }

        public override async Task<PrivateChat> GetAsync(int id, CancellationToken ct = default)
        {
            return await DbContext.PrivateChats
                .Include(t => t.CreatedByUser)
                .Include(t => t.InvitedUser)
                .FirstOrDefaultAsync(t => t.Id == id, ct);
        }

        public async Task<ICollection<PrivateChat>> GetChatsByUserIdAsync(string userId, CancellationToken ct)
        {
            return await DbContext.PrivateChats
                .Include(t => t.CreatedByUser)
                .Include(t => t.InvitedUser)
                .Where(t => t.CreatedByUserId == userId || t.InvitedUserId == userId)
                .ToListAsync(ct);
        }

        public async Task<PrivateChat> GetChatByUserIdAsync(string userId, string invitedUserId, CancellationToken ct)
        {
            return await DbContext.PrivateChats
                .FirstOrDefaultAsync(
                    t => (t.CreatedByUserId == userId && t.InvitedUserId == invitedUserId) ||
                         (t.InvitedUserId == userId && t.CreatedByUserId == invitedUserId),
                    ct);
        }
    }
}