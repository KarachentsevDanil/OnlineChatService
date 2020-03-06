using FluentValidation;
using OCS.BLL.DTOs.Users;

namespace OCS.WebApi.Validations.Users
{
    public class UserRegistrationValidator : AbstractValidator<UserRegistrationDto>
    {
        public UserRegistrationValidator()
        {
            RuleFor(t => t.Email)
                .EmailAddress()
                .MaximumLength(250);

            RuleFor(t => t.Password)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(50);

            RuleFor(t => t.FirstName)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(t => t.LastName)
                .NotEmpty()
                .MaximumLength(250);
        }
    }
}