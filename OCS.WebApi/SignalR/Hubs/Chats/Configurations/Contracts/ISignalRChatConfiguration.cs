using FluentValidation;
using OCS.BLL.DTOs.Chats.Group;
using OCS.BLL.DTOs.Chats.Private;
using OCS.BLL.Services.Contracts.Chats.Group;
using OCS.BLL.Services.Contracts.Chats.Private;
using OCS.WebApi.SignalR.Hubs.Configurations.Contracts;

namespace OCS.WebApi.SignalR.Hubs.Chats.Configurations.Contracts
{
    public interface ISignalRChatConfiguration : ISignalRBaseConfiguration
    {
        IPrivateChatService PrivateChatService { get; }

        IPrivateChatMessageService PrivateChatMessageService { get; }

        IGroupChatMessageService GroupChatMessageService { get; }

        IGroupChatService GroupChatService { get; }

        IValidator<CreatePrivateChatMessageDto> CreatePrivateChatMessageValidator { get; }

        IValidator<CreateGroupChatMessageDto> CreateGroupChatMessageValidator { get; }
    }
}