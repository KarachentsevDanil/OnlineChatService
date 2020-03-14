using AutoMapper;
using Microsoft.Extensions.Logging;
using OCS.BLL.DTOs.Chats.Private;
using OCS.BLL.Exceptions.Chats;
using OCS.BLL.Exceptions.Users;
using OCS.BLL.Services.Contracts.Chats.Private;
using OCS.DAL.Entities.Chats;
using OCS.DAL.Entities.Users;
using OCS.DAL.Entities.Views.Chats;
using OCS.DAL.UnitOfWorks.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using OCS.DAL.Queries.Chats;

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

            await ValidatePrivateChatAsync(chatDto, ct);

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

        public async Task<IImmutableList<GetPrivateChatViewDto>> GetUserPrivateChatsViewAsync(GetPrivateChatsQueryDto query, CancellationToken ct = default)
        {
            _logger.LogInformation("Get user private chats by user id {UserId}", query.UserId);

            GetPrivateChatsQuery dbQuery = _mapper.Map<GetPrivateChatsQuery>(query);

            ICollection<PrivateChatView> chats = await _unitOfWork.PrivateChatRepository.GetPrivateChatsAsync(dbQuery, ct);

            return _mapper.Map<ICollection<GetPrivateChatViewDto>>(chats).ToImmutableList();
        }

        public async Task<GetPrivateChatDto> GetChatByIdAsync(int id, CancellationToken ct = default)
        {
            _logger.LogInformation("Get user private chat by chat id {ChatId}", id);

            PrivateChat chat = await _unitOfWork.PrivateChatRepository.GetAsync(id, ct);

            return _mapper.Map<GetPrivateChatDto>(chat);
        }

        private async Task ValidatePrivateChatAsync(CreatePrivateChatDto chatDto, CancellationToken ct)
        {
            User user =
                await _unitOfWork.UserRepository.GetAsync(chatDto.ContactId, ct);

            if (user == null)
            {
                _logger.LogWarning("User {UserId} not found", chatDto.ContactId);

                throw new UserNotFoundException();
            }

            PrivateChat dbChat =
                await _unitOfWork.PrivateChatRepository.GetChatByUserIdAsync(chatDto.UserId, chatDto.ContactId, ct);

            if (dbChat != null)
            {
                _logger.LogWarning(
                    "User {UserId} already has private chat with Contact {ContactId}",
                    chatDto.UserId,
                    chatDto.ContactId);

                throw new PrivateChatAlreadyExistException();
            }
        }
    }
}