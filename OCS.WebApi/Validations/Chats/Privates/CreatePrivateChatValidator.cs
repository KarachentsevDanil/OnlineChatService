using FluentValidation;
using OCS.BLL.DTOs.Chats.Private;

namespace OCS.WebApi.Validations.Chats.Privates
{
    public class CreatePrivateChatValidator : AbstractValidator<CreatePrivateChatDto>
    {
        public CreatePrivateChatValidator()
        {
            RuleFor(t => t.ContactId)
                .NotEmpty()
                .MaximumLength(450);
        }
    }
}