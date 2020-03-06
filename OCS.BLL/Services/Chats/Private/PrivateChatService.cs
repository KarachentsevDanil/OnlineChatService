using AutoMapper;
using Microsoft.Extensions.Logging;
using OCS.BLL.DTOs.Chats.Private;
using OCS.BLL.Services.Contracts.Chats.Private;
using OCS.DAL.Entities.Chats;
using OCS.DAL.UnitOfWorks.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

namespace OCS.BLL.Services.Chats.Private
{
    public class PrivateChatService : IPrivateChatService
    {
        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<PrivateChatService> _logger;

        public PrivateChatService(IMapper mapper, IUnitOfWork unitOfWork, ILogger<PrivateChatService> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<GetPrivateChatDto> CreatePrivateChatAsync(CreatePrivateChatDto chatDto, CancellationToken ct = default)
        {
            _logger.LogInformation("Create private chat {PrivateChat}", chatDto);

            PrivateChat chat = _mapper.Map<PrivateChat>(chatDto);

            chat.CreatedAt = chat.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.PrivateChatRepository.Create(chat);

            await _unitOfWork.CommitAsync(ct);

            return _mapper.Map<GetPrivateChatDto>(chat);
        }

        public async Task<IImmutableList<GetPrivateChatDto>> GetUserPrivateChatsAsync(string userId, CancellationToken ct = default)
        {
            _logger.LogInformation("Get user private chats by user id {UserId}", userId);

            ICollection<PrivateChat> chats = await _unitOfWork.PrivateChatRepository.GetChatsByUserIdAsync(userId, ct);

            return _mapper.Map<ICollection<GetPrivateChatDto>>(chats).ToImmutableList();
        }

        public async Task<GetPrivateChatDto> GetChatByIdAsync(int id, CancellationToken ct = default)
        {
            _logger.LogInformation("Get user private chat by chat id {ChatId}", id);

            PrivateChat chat = await _unitOfWork.PrivateChatRepository.GetAsync(id, ct);

            return _mapper.Map<GetPrivateChatDto>(chat);
        }
    }
}