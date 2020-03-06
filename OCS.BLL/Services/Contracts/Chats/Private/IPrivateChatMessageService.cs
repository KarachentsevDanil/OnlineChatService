using OCS.BLL.DTOs.Chats.Private;
using OCS.BLL.DTOs.Pagination;
using System.Threading;
using System.Threading.Tasks;
using OCS.BLL.DTOs.Chats.Queries;

namespace OCS.BLL.Services.Contracts.Chats.Private
{
    public interface IPrivateChatMessageService
    {
        Task<GetPrivateChatMessageDto> AddMessageAsync(CreatePrivateChatMessageDto message, CancellationToken ct = default);

        Task<PagedItemResultDto<GetPrivateChatMessageDto>> GetMessagesAsync(GetPagedMessagesQueryDto query, CancellationToken ct = default);
    }
}