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
    public class PrivateChatMessageRepository : GenericRepository<int, PrivateChatMessage, OnlineChatServiceDbContext>, IPrivateChatMessageRepository
    {
        public PrivateChatMessageRepository(OnlineChatServiceDbContext context) : base(context)
        {
        }

        public async Task<PagedItemResultModel<PrivateChatMessage>> GetMessagesAsync(GetPagedMessagesQuery query, CancellationToken ct)
        {
            IQueryable<PrivateChatMessage> messagesQuery = DbContext.PrivateChatMessages
                .Include(t => t.CreatedByUser)
                .Where(t => t.ChatId == query.ChatId);

            PagedItemResultModel<PrivateChatMessage> result = new PagedItemResultModel<PrivateChatMessage>
            {
                TotalCount = messagesQuery.Count(),
                Items = await messagesQuery.Skip(query.Skip).Take(query.Take).ToListAsync(ct)
            };

            return result;
        }
    }
}