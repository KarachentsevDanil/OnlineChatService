using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OCS.DAL.EF.Context;
using OCS.DAL.Entities.Users;
using OCS.DAL.Repositories.Contracts.Users;
using System.Threading;
using System.Threading.Tasks;
using OCS.DAL.Queries.Users;
using EntityFrameworkHelper = Microsoft.EntityFrameworkCore.EF;

namespace OCS.DAL.EF.Repositories.Users
{
    public class UserRepository : GenericRepository<string, User, OnlineChatServiceDbContext>, IUserRepository
    {
        public UserRepository(OnlineChatServiceDbContext context) : base(context)
        {
        }

        public async Task<User> GetByEmailAsync(string email, CancellationToken ct = default)
        {
            return await DbContext.Users
                .FirstOrDefaultAsync(t => EntityFrameworkHelper.Functions.Like(t.Email, email), ct);
        }

        public async Task<ICollection<User>> GetByQueryAsync(GetUsersQuery query, CancellationToken ct = default)
        {
            IQueryable<User> dbQuery = DbContext.Users
                .Where(t => t.Id != query.UserId);

            if (!string.IsNullOrEmpty(query.Term))
            {
                dbQuery = dbQuery.Where(t => (EntityFrameworkHelper.Functions.Like(t.Email, query.Term) ||
                                              EntityFrameworkHelper.Functions.Like(t.FirstName, query.Term) ||
                                              EntityFrameworkHelper.Functions.Like(t.LastName, query.Term)));
            }

            return await dbQuery.ToListAsync(ct);
        }
    }
}