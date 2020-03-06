using FluentValidation;
using OCS.BLL.DTOs.Chats.Group;

namespace OCS.WebApi.Validations.Chats.Groups
{
    public class CreateGroupChatValidator : AbstractValidator<CreateGroupChatDto>
    {
        public CreateGroupChatValidator()
        {
            RuleFor(t => t.Name)
                .NotEmpty()
                .MaximumLength(250);
        }
    }
}