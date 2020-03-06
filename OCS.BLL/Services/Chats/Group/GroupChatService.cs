using AutoMapper;
using Microsoft.Extensions.Logging;
using OCS.BLL.DTOs.Chats.Group;
using OCS.BLL.Services.Contracts.Chats.Group;
using OCS.DAL.Entities.Chats;
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
    }
}