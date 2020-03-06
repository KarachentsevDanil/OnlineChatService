using Microsoft.EntityFrameworkCore;
using OCS.DAL.EF.Context;
using OCS.DAL.Entities.Users;
using OCS.DAL.Repositories.Contracts.Users;
using System.Threading;
using System.Threading.Tasks;
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
    }
}