using OCS.DAL.Entities.Users;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OCS.DAL.Repositories.Contracts.Users
{
    public interface IUserContactRepository : IGenericRepository<int, UserContact>
    {
        Task<ICollection<UserContact>> GetUserContactsAsync(string userId, CancellationToken ct);

        Task<UserContact> GetUserContactAsync(string userId, string contactId, CancellationToken ct);
    }
}