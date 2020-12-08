using FluentValidation;
using MatchDayApp.Domain.Resources;

namespace MatchDayApp.Application.Commands.User.Validations
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            // FirstName
            RuleFor(prop => prop.UpdateUser.FirstName)
                .NotEmpty().WithMessage(Dictionary.MV005);
            RuleFor(prop => prop.UpdateUser.FirstName)
                .MinimumLength(4).WithMessage(Dictionary.MV006)
                .When(x => !string.IsNullOrEmpty(x.UpdateUser.FirstName));
            RuleFor(prop => prop.UpdateUser.FirstName)
                .Matches(@"^[a-zA-Z]*$").WithMessage(Dictionary.MV009);

            // LastName
            RuleFor(prop => prop.UpdateUser.LastName)
                .NotEmpty().WithMessage(Dictionary.MV007);
            RuleFor(prop => prop.UpdateUser.LastName)
                .MinimumLength(4).WithMessage(Dictionary.MV008)
                .When(x => !string.IsNullOrEmpty(x.UpdateUser.LastName));
            RuleFor(prop => prop.UpdateUser.LastName)
                .Matches(@"^[a-zA-Z]*$").WithMessage(Dictionary.MV010);

            // Email
            RuleFor(prop => prop.UpdateUser.Email)
                .EmailAddress().WithMessage(Dictionary.MV011);

            // Username
            RuleFor(prop => prop.UpdateUser.Username)
                .NotEmpty().WithMessage(Dictionary.MV012);
        }
    }
}
