using FluentValidation;
using OCS.BLL.DTOs.Chats.Private;

namespace OCS.WebApi.Validations.Chats.Privates
{
    public class CreatePrivateChatMessageValidator : AbstractValidator<CreatePrivateChatMessageDto>
    {
        public CreatePrivateChatMessageValidator()
        {
            RuleFor(t => t.Text)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(t => t.ChatId)
                .NotEmpty();
        }
    }
}