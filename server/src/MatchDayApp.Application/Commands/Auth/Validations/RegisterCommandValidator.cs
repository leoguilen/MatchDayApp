using FluentValidation;
using MatchDayApp.Domain.Resources;

namespace MatchDayApp.Application.Commands.Auth.Validations
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            // FirstName
            RuleFor(prop => prop.Register.FirstName).NotEmpty().WithMessage(Dictionary.MV005);
            RuleFor(prop => prop.Register.FirstName).MinimumLength(4).WithMessage(Dictionary.MV006);
            RuleFor(prop => prop.Register.FirstName).Matches(@"^[A-Z][a-zA-Z]*$").WithMessage(Dictionary.MV009);

            // LastName
            RuleFor(prop => prop.Register.LastName).NotEmpty().WithMessage(Dictionary.MV007);
            RuleFor(prop => prop.Register.LastName).MinimumLength(4).WithMessage(Dictionary.MV008);
            RuleFor(prop => prop.Register.LastName).Matches(@"^[A-Z][a-zA-Z]*$").WithMessage(Dictionary.MV010);
        }
    }
}
