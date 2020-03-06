using AutoMapper;
using Microsoft.Extensions.Logging;
using OCS.BLL.DTOs.Chats.Private;
using OCS.BLL.DTOs.Chats.Queries;
using OCS.BLL.DTOs.Pagination;
using OCS.BLL.Services.Contracts.Chats.Private;
using OCS.DAL.Entities.Chats;
using OCS.DAL.Entities.Chats.Queries;
using OCS.DAL.Models;
using OCS.DAL.UnitOfWorks.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OCS.BLL.Services.Chats.Private
{
    public class PrivateChatMessageService : IPrivateChatMessageService
    {
        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<PrivateChatMessageService> _logger;

        public PrivateChatMessageService(IMapper mapper, IUnitOfWork unitOfWork, ILogger<PrivateChatMessageService> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<GetPrivateChatMessageDto> AddMessageAsync(CreatePrivateChatMessageDto messageDto, CancellationToken ct = default)
        {
            _logger.LogInformation("Create private chat message {PrivateChatMessage}", messageDto);

            PrivateChatMessage message = _mapper.Map<PrivateChatMessage>(messageDto);

            message.CreatedAt = message.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.PrivateChatMessageRepository.Create(message);

            await _unitOfWork.CommitAsync(ct);

            return _mapper.Map<GetPrivateChatMessageDto>(message);
        }

        public async Task<PagedItemResultDto<GetPrivateChatMessageDto>> GetMessagesAsync(GetPagedMessagesQueryDto queryDto, CancellationToken ct = default)
        {
            _logger.LogInformation("Get private chat messages {ChatId}", queryDto.ChatId);

            GetPagedMessagesQuery query = _mapper.Map<GetPagedMessagesQuery>(queryDto);

            PagedItemResultModel<PrivateChatMessage> result =
                await _unitOfWork.PrivateChatMessageRepository.GetMessagesAsync(query, ct);

            return _mapper.Map<PagedItemResultDto<GetPrivateChatMessageDto>>(result);
        }
    }
}