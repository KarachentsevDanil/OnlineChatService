using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using OCS.BLL.DTOs.Chats.Group;
using OCS.BLL.DTOs.Chats.Private;
using OCS.BLL.Services.Contracts.Chats.Group;
using OCS.BLL.Services.Contracts.Chats.Private;
using OCS.WebApi.SignalR.Hubs.Abstract;
using OCS.WebApi.SignalR.Hubs.Chats.Configurations.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OCS.WebApi.SignalR.Hubs.Chats
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatMessageHub : BaseChatHub
    {
        private readonly IPrivateChatMessageService _privateChatMessageService;

        private readonly IPrivateChatService _privateChatService;

        private readonly IValidator<CreatePrivateChatMessageDto> _createPrivateMessageValidator;

        private readonly IGroupChatMessageService _groupChatMessageService;

        private readonly IGroupChatService _groupChatService;

        private readonly IValidator<CreateGroupChatMessageDto> _createGroupChatMessageValidator;

        public ChatMessageHub(ISignalRChatConfiguration configuration)
            : base(configuration)
        {
            _privateChatMessageService = configuration.PrivateChatMessageService;
            _privateChatService = configuration.PrivateChatService;
            _createPrivateMessageValidator = configuration.CreatePrivateChatMessageValidator;

            _groupChatMessageService = configuration.GroupChatMessageService;
            _groupChatService = configuration.GroupChatService;
            _createGroupChatMessageValidator = configuration.CreateGroupChatMessageValidator;
        }

        public async Task SendPrivateMessageAsync(CreatePrivateChatMessageDto model)
        {
            await ExecuteActionAsync(model, ExecuteSendPrivateMessageAsync, _createPrivateMessageValidator);
        }

        public async Task SendGroupMessageAsync(CreateGroupChatMessageDto model)
        {
            await ExecuteActionAsync(model, ExecuteSendGroupMessageAsync, _createGroupChatMessageValidator);
        }

        private async Task ExecuteSendPrivateMessageAsync(CreatePrivateChatMessageDto model, CancellationToken ct = default)
        {
            model.UserId = Context.UserIdentifier;

            GetPrivateChatMessageDto message = await _privateChatMessageService.AddMessageAsync(model, ct);

            GetPrivateChatDto chat = await _privateChatService.GetChatByIdAsync(model.ChatId, ct);

            await Clients.Users(chat.InvitedUser.Id, chat.CreatedByUser.Id).SendAsync(nameof(ChatMessageHub), message, cancellationToken: ct);
        }

        private async Task ExecuteSendGroupMessageAsync(CreateGroupChatMessageDto model, CancellationToken ct = default)
        {
            GetGroupChatMessageDto message = await _groupChatMessageService.AddMessageAsync(model, ct);

            IReadOnlyList<string> userIds =
                message.Users.Select(t => t.Id).Where(t => t != Context.UserIdentifier).ToList();

            await Clients.Users(userIds).SendAsync(nameof(ChatMessageHub), message, cancellationToken: ct);
        }
    }
}