using FluentValidation;
using OCS.BLL.DTOs.Chats.Group;

namespace OCS.WebApi.Validations.Chats.Groups
{
    public class CreateGroupChatMessageValidator : AbstractValidator<CreateGroupChatMessageDto>
    {
        public CreateGroupChatMessageValidator()
        {
            RuleFor(t => t.Text)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(t => t.ChatId)
                .NotEmpty();
        }
    }
}