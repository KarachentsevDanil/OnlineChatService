using Microsoft.EntityFrameworkCore;
using OCS.DAL.EF.Context;
using OCS.DAL.Entities.Chats;
using OCS.DAL.Entities.Chats.Queries;
using OCS.DAL.Models;
using OCS.DAL.Repositories.Contracts.Chats;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OCS.DAL.EF.Repositories.Chats
{
    public class GroupChatMessageRepository : GenericRepository<int, GroupChatMessage, OnlineChatServiceDbContext>, IGroupChatMessageRepository
    {
        public GroupChatMessageRepository(OnlineChatServiceDbContext context) : base(context)
        {
        }

        public async Task<PagedItemResultModel<GroupChatMessage>> GetMessagesAsync(GetPagedMessagesQuery query, CancellationToken ct)
        {
            IQueryable<GroupChatMessage> messagesQuery = DbContext.GroupChatMessages
                .Include(t => t.CreatedByUser)
                .Where(t => t.ChatId == query.ChatId);

            PagedItemResultModel<GroupChatMessage> result = new PagedItemResultModel<GroupChatMessage>
            {
                TotalCount = messagesQuery.Count(),
                Items = await messagesQuery.Skip(query.Skip).Take(query.Take).ToListAsync(ct)
            };

            return result;
        }
    }
}