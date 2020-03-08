using Microsoft.EntityFrameworkCore;
using OCS.DAL.EF.Context;
using OCS.DAL.Entities.Users;
using OCS.DAL.Repositories.Contracts.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OCS.DAL.EF.Repositories.Users
{
    public class UserContactRepository : GenericRepository<int, UserContact, OnlineChatServiceDbContext>, IUserContactRepository
    {
        public UserContactRepository(OnlineChatServiceDbContext context) : base(context)
        {
        }

        public async Task<ICollection<UserContact>> GetUserContactsAsync(string userId, CancellationToken ct)
        {
            return await DbContext.UserContacts
                .Include(t => t.Contact)
                .Include(t => t.User)
                .Where(t => t.UserId == userId)
                .ToListAsync(ct);
        }

        public async Task<UserContact> GetUserContactAsync(string userId, string contactId, CancellationToken ct)
        {
            return await DbContext.UserContacts.FirstOrDefaultAsync(t => t.UserId == userId && t.ContactId == contactId, ct);
        }
    }
}