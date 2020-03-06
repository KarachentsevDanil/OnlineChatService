using FluentValidation;
using OCS.BLL.DTOs.Chats.Group;

namespace OCS.WebApi.Validations.Chats.Groups
{
    public class AddUserToGroupChatValidator : AbstractValidator<AddUserToGroupChatDto>
    {
        public AddUserToGroupChatValidator()
        {
            RuleFor(t => t.UserId)
                .NotEmpty()
                .MaximumLength(450);

            RuleFor(t => t.ChatId)
                .NotEmpty();
        }
    }
}