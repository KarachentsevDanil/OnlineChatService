using FluentValidation;
using OCS.BLL.DTOs.Users;

namespace OCS.WebApi.Validations.Users
{
    public class AddUserToContactValidator : AbstractValidator<AddUserToContactDto>
    {
        public AddUserToContactValidator()
        {
            RuleFor(t => t.ContactId)
                .NotEmpty()
                .MaximumLength(450);
        }
    }
}