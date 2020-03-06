using OCS.BLL.DTOs.Users;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

namespace OCS.BLL.Services.Contracts.Users
{
    public interface IUserContactService
    {
        Task<GetUserContactDto> AddUserToContactAsync(AddUserToContactDto addUserToContactDto, CancellationToken ct = default);

        Task<IImmutableList<GetUserContactDto>> GetUserContactsAsync(string userId, CancellationToken ct = default);
    }
}