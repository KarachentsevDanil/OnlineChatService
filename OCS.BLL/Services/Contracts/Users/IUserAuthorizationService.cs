using OCS.BLL.DTOs.Users;
using System.Threading;
using System.Threading.Tasks;

namespace OCS.BLL.Services.Contracts.Users
{
    public interface IUserAuthorizationService
    {
        Task<UserTokenDto> LoginUserAsync(UserLoginDto loginDto, CancellationToken ct = default);

        Task<GetUserDto> RegisterUserAsync(UserRegistrationDto registrationDto, CancellationToken ct = default);
    }
}
