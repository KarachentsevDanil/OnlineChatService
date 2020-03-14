using OCS.DAL.Entities.Users;
using OCS.DAL.Queries.Users;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OCS.DAL.Repositories.Contracts.Users
{
    public interface IUserRepository : IGenericRepository<string, User>
    {
        Task<User> GetByEmailAsync(string email, CancellationToken ct = default);

        Task<ICollection<User>> GetByQueryAsync(GetUsersQuery query, CancellationToken ct = default);
    }
}
