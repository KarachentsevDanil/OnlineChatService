using OCS.BLL.DTOs.Chats.Group;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

namespace OCS.BLL.Services.Contracts.Chats.Group
{
    public interface IGroupChatService
    {
        Task<GetGroupChatDto> CreateChatAsync(CreateGroupChatDto chat, CancellationToken ct = default);

        Task AddUserToGroupChatAsync(AddUserToGroupChatDto user, CancellationToken ct = default);

        Task<IImmutableList<GetGroupChatDto>> GetUserGroupChatsAsync(string userId, CancellationToken ct = default);
    }
}