using FluentValidation;
using MatchDayApp.Domain.Resources;
using System.Text.RegularExpressions;

namespace MatchDayApp.Application.Commands.Auth.Validations
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            // Email
            RuleFor(prop => prop.ResetPassword.Email)
                .EmailAddress().WithMessage(Dictionary.MV011);

            // Password
            RuleFor(prop => prop.ResetPassword.Password)
                .NotEmpty().WithMessage(Dictionary.MV013);
            RuleFor(prop => prop.ResetPassword.Password)
                .Must(pwd => Regex.IsMatch(pwd, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$"))
                .WithMessage(Dictionary.MV014)
                .When(x => !string.IsNullOrEmpty(x.ResetPassword.Password));

            // Confirm Password
            RuleFor(prop => prop.ResetPassword.ConfirmPassword)
                .Equal(prop => prop.ResetPassword.Password)
                .WithMessage(Dictionary.MV015)
                .When(x => !string.IsNullOrEmpty(x.ResetPassword.Password));
        }
    }
}
