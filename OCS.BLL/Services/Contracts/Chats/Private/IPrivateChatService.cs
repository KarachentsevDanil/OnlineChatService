using OCS.BLL.DTOs.Chats.Private;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

namespace OCS.BLL.Services.Contracts.Chats.Private
{
    public interface IPrivateChatService
    {
        Task<GetPrivateChatDto> CreatePrivateChatAsync(CreatePrivateChatDto chat, CancellationToken ct = default);

        Task<IImmutableList<GetPrivateChatDto>> GetUserPrivateChatsAsync(string userId, CancellationToken ct = default);

        Task<GetPrivateChatDto> GetChatByIdAsync(int id, CancellationToken ct = default);
    }
}