using OCS.DAL.Entities.Users;
using System.Threading;
using System.Threading.Tasks;

namespace OCS.DAL.Repositories.Contracts.Users
{
    public interface IUserRepository : IGenericRepository<string, User>
    {
        Task<User> GetByEmailAsync(string email, CancellationToken ct = default);
    }
}
