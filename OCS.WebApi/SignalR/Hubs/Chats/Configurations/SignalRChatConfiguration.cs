using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using OCS.BLL.DTOs.Chats.Group;
using OCS.BLL.DTOs.Chats.Private;
using OCS.BLL.Services.Contracts.Chats.Group;
using OCS.BLL.Services.Contracts.Chats.Private;
using OCS.WebApi.SignalR.Hubs.Chats.Configurations.Contracts;
using OCS.WebApi.SignalR.Hubs.Configurations;

namespace OCS.WebApi.SignalR.Hubs.Chats.Configurations
{
    public class SignalRChatConfiguration : SignalRBaseConfiguration, ISignalRChatConfiguration
    {
        public SignalRChatConfiguration(
            IMapper mapper,
            ILogger<SignalRChatConfiguration> logger,
            IPrivateChatService privateChatService,
            IPrivateChatMessageService privateChatMessageService,
            IGroupChatMessageService groupChatMessageService,
            IGroupChatService groupChatService,
            IValidator<CreatePrivateChatMessageDto> createPrivateChatMessageValidator,
            IValidator<CreateGroupChatMessageDto> createGroupChatMessageValidator)
            : base(mapper, logger)
        {
            PrivateChatService = privateChatService;
            PrivateChatMessageService = privateChatMessageService;
            GroupChatMessageService = groupChatMessageService;
            GroupChatService = groupChatService;
            CreatePrivateChatMessageValidator = createPrivateChatMessageValidator;
            CreateGroupChatMessageValidator = createGroupChatMessageValidator;
        }

        public IPrivateChatService PrivateChatService { get; }

        public IPrivateChatMessageService PrivateChatMessageService { get; }

        public IGroupChatMessageService GroupChatMessageService { get; }

        public IGroupChatService GroupChatService { get; }

        public IValidator<CreatePrivateChatMessageDto> CreatePrivateChatMessageValidator { get; }

        public IValidator<CreateGroupChatMessageDto> CreateGroupChatMessageValidator { get; }
    }
}