using System.Collections.Immutable;
using OCS.BLL.DTOs.Users;
using System.Threading;
using System.Threading.Tasks;

namespace OCS.BLL.Services.Contracts.Users
{
    public interface IUserService
    {
        Task<IImmutableList<GetUserDto>> GetByQueryAsync(GetUsersQueryDto query, CancellationToken ct = default);

        Task<GetUserDto> GetByIdAsync(string id, CancellationToken ct = default);

        Task<GetUserDto> GetByEmailAsync(string email, CancellationToken ct = default);

        Task<GetUserDto> SetUserOnlineStatusAsync(SetUserOnlineDto userOnlineDto, CancellationToken ct = default);
    }
}