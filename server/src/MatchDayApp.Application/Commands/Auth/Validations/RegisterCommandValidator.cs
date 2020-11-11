using FluentValidation;
using MatchDayApp.Domain.Resources;
using System.Text.RegularExpressions;

namespace MatchDayApp.Application.Commands.Auth.Validations
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            // FirstName
            RuleFor(prop => prop.Register.FirstName)
                .NotEmpty().WithMessage(Dictionary.MV005);
            RuleFor(prop => prop.Register.FirstName)
                .MinimumLength(4).WithMessage(Dictionary.MV006)
                .When(x => !string.IsNullOrEmpty(x.Register.FirstName));
            RuleFor(prop => prop.Register.FirstName)
                .Matches(@"^[a-zA-Z]*$").WithMessage(Dictionary.MV009);

            // LastName
            RuleFor(prop => prop.Register.LastName)
                .NotEmpty().WithMessage(Dictionary.MV007);
            RuleFor(prop => prop.Register.LastName)
                .MinimumLength(4).WithMessage(Dictionary.MV008)
                .When(x => !string.IsNullOrEmpty(x.Register.LastName));
            RuleFor(prop => prop.Register.LastName)
                .Matches(@"^[a-zA-Z]*$").WithMessage(Dictionary.MV010);

            // Email
            RuleFor(prop => prop.Register.Email)
                .EmailAddress().WithMessage(Dictionary.MV011);

            // Username
            RuleFor(prop => prop.Register.UserName)
                .NotEmpty().WithMessage(Dictionary.MV012);

            // Password
            RuleFor(prop => prop.Register.Password)
                .NotEmpty().WithMessage(Dictionary.MV013);
            RuleFor(prop => prop.Register.Password)
                .Must(pwd => Regex.IsMatch(pwd, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$"))
                .WithMessage(Dictionary.MV014)
                .When(x => !string.IsNullOrEmpty(x.Register.Password));

            // Confirm Password
            RuleFor(prop => prop.Register.ConfirmPassword)
                .Equal(prop => prop.Register.Password)
                .WithMessage(Dictionary.MV015)
                .When(x => !string.IsNullOrEmpty(x.Register.Password));
        }
    }
}
