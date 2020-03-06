using OCS.DAL.Entities.Chats;
using System.Threading;
using System.Threading.Tasks;
using OCS.BLL.DTOs.Chats.Group;
using OCS.BLL.DTOs.Chats.Queries;
using OCS.BLL.DTOs.Pagination;

namespace OCS.BLL.Services.Contracts.Chats.Group
{
    public interface IGroupChatMessageService
    {
        Task<GetGroupChatMessageDto> AddMessageAsync(CreateGroupChatMessageDto message, CancellationToken ct = default);

        Task<PagedItemResultDto<GetGroupChatMessageDto>> GetMessagesAsync(GetPagedMessagesQueryDto query, CancellationToken ct = default);
    }
}