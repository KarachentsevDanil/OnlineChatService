using FluentValidation;
using OCS.BLL.DTOs.Users;

namespace OCS.WebApi.Validations.Users
{
    public class UserLoginValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginValidator()
        {
            RuleFor(t => t.Email)
                .EmailAddress()
                .MaximumLength(250);

            RuleFor(t => t.Password)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(50);
        }
    }
}