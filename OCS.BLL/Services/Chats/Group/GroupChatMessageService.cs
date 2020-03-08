using AutoMapper;
using Microsoft.Extensions.Logging;
using OCS.BLL.DTOs.Chats.Group;
using OCS.BLL.DTOs.Chats.Queries;
using OCS.BLL.DTOs.Pagination;
using OCS.BLL.Services.Contracts.Chats.Group;
using OCS.DAL.Entities.Chats;
using OCS.DAL.Entities.Chats.Queries;
using OCS.DAL.Models;
using OCS.DAL.UnitOfWorks.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;
using OCS.BLL.Exceptions.Chats;

namespace OCS.BLL.Services.Chats.Group
{
    public class GroupChatMessageService : IGroupChatMessageService
    {
        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<GroupChatMessageService> _logger;

        public GroupChatMessageService(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GroupChatMessageService> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<GetGroupChatMessageDto> AddMessageAsync(CreateGroupChatMessageDto messageDto, CancellationToken ct = default)
        {
            _logger.LogInformation("Create group chat message {GroupChatMessage}", messageDto);

            GroupChat chat = await _unitOfWork.GroupChatRepository.GetAsync(messageDto.ChatId, ct);

            if (chat == null)
            {
                _logger.LogWarning("Group chat with id {GroupChatId} not found", messageDto.ChatId);
                throw new ChatNotFoundException();
            }

            GroupChatMessage message = _mapper.Map<GroupChatMessage>(messageDto);

            message.CreatedAt = message.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.GroupChatMessageRepository.Create(message);

            await _unitOfWork.CommitAsync(ct);

            return _mapper.Map<GetGroupChatMessageDto>(message);
        }

        public async Task<PagedItemResultDto<GetGroupChatMessageDto>> GetMessagesAsync(GetPagedMessagesQueryDto queryDto, CancellationToken ct = default)
        {
            _logger.LogInformation("Get group chat messages {ChatId}", queryDto.ChatId);

            GetPagedMessagesQuery query = _mapper.Map<GetPagedMessagesQuery>(queryDto);

            PagedItemResultModel<GroupChatMessage> result =
                await _unitOfWork.GroupChatMessageRepository.GetMessagesAsync(query, ct);

            return _mapper.Map<PagedItemResultDto<GetGroupChatMessageDto>>(result);
        }
    }
}