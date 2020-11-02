using FluentValidation;
using MatchDayApp.Domain.Resources;

namespace MatchDayApp.Application.Commands.Auth.Validations
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            // Email
            RuleFor(prop => prop.Login.Email)
                .EmailAddress().WithMessage(Dictionary.MV011);
        }
    }
}
