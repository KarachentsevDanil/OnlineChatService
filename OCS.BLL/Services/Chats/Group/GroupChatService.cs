using AutoMapper;
using Microsoft.Extensions.Logging;
using OCS.BLL.DTOs.Chats.Group;
using OCS.BLL.Exceptions.Chats;
using OCS.BLL.Exceptions.Users;
using OCS.BLL.Services.Contracts.Chats.Group;
using OCS.DAL.Entities.Chats;
using OCS.DAL.Entities.Users;
using OCS.DAL.UnitOfWorks.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

namespace OCS.BLL.Services.Chats.Group
{
    public class GroupChatService : IGroupChatService
    {
        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<GroupChatService> _logger;

        public GroupChatService(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GroupChatService> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task AddUserToGroupChatAsync(AddUserToGroupChatDto addUserToGroupDto, CancellationToken ct = default)
        {
            _logger.LogInformation("Add user to group chat {AddUserToGroup}", addUserToGroupDto);

            await ValidateAddingUserToGroupChatAsync(addUserToGroupDto, ct);

            UserGroupChat addUserToGroup = _mapper.Map<UserGroupChat>(addUserToGroupDto);

            addUserToGroup.CreatedAt = addUserToGroup.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.UserGroupChatRepository.Create(addUserToGroup);

            await _unitOfWork.CommitAsync(ct);
        }

        public async Task<GetGroupChatDto> CreateChatAsync(CreateGroupChatDto chatDto, CancellationToken ct = default)
        {
            _logger.LogInformation("Create private chat {GroupChat}", chatDto);

            GroupChat chat = _mapper.Map<GroupChat>(chatDto);

            chat.CreatedAt = chat.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.GroupChatRepository.Create(chat);

            await _unitOfWork.CommitAsync(ct);

            return _mapper.Map<GetGroupChatDto>(chat);
        }

        public async Task<IImmutableList<GetGroupChatDto>> GetUserGroupChatsAsync(string userId, CancellationToken ct = default)
        {
            _logger.LogInformation("Get user private chats by user id {UserId}", userId);

            ICollection<GroupChat> chats = await _unitOfWork.GroupChatRepository.GetChatsByUserIdAsync(userId, ct);

            return _mapper.Map<ICollection<GetGroupChatDto>>(chats).ToImmutableList();
        }

        private async Task ValidateAddingUserToGroupChatAsync(AddUserToGroupChatDto addUserToGroupDto, CancellationToken ct)
        {
            User user = await _unitOfWork.UserRepository.GetAsync(addUserToGroupDto.UserId, ct);

            if (user == null)
            {
                _logger.LogWarning("User {UserId} not found", addUserToGroupDto.UserId);

                throw new UserNotFoundException();
            }

            GroupChat chat = await _unitOfWork.GroupChatRepository.GetAsync(addUserToGroupDto.ChatId, ct);

            if (chat == null)
            {
                _logger.LogWarning("Group chat with id {GroupChatId} not found", addUserToGroupDto.ChatId);
                throw new ChatNotFoundException();
            }

            if (chat.OwnerId != addUserToGroupDto.AddedByUserId)
            {
                _logger.LogWarning("Only owner can add user to group chat. {UserId} is not the owner", addUserToGroupDto.AddedByUserId);
                throw new AddUserActionForbiddenException();
            }

            UserGroupChat userGroupChat = await _unitOfWork.UserGroupChatRepository.GetUserGroupChatAsync(
                    addUserToGroupDto.ChatId, addUserToGroupDto.UserId, ct);

            if (userGroupChat != null)
            {
                _logger.LogWarning("User with id {UserId} already exist in chat", addUserToGroupDto.UserId, addUserToGroupDto.ChatId);
                throw new UserAlreadyInGroupChatException();
            }
        }
    }
}