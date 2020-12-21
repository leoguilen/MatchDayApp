using FluentValidation;
using MatchDayApp.Application.Comandos.Autenticacao;
using MatchDayApp.Domain.Resources;
using System.Text.RegularExpressions;

namespace MatchDayApp.Application.Comandos.Autenticao.Validacoes
{
    public class ResetarSenhaCommandValidator : AbstractValidator<ResetarSenhaCommand>
    {
        public ResetarSenhaCommandValidator()
        {
            // Email
            RuleFor(prop => prop.ResetarSenha.Email)
                .EmailAddress().WithMessage(Dicionario.MV011);

            // Password
            RuleFor(prop => prop.ResetarSenha.Senha)
                .NotEmpty().WithMessage(Dicionario.MV013);
            RuleFor(prop => prop.ResetarSenha.Senha)
                .Must(pwd => Regex.IsMatch(pwd, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$"))
                .WithMessage(Dicionario.MV014)
                .When(x => !string.IsNullOrEmpty(x.ResetarSenha.Senha));

            // Confirm Password
            RuleFor(prop => prop.ResetarSenha.ConfirmacaoSenha)
                .Equal(prop => prop.ResetarSenha.Senha)
                .WithMessage(Dicionario.MV015)
                .When(x => !string.IsNullOrEmpty(x.ResetarSenha.Senha));
        }
    }
}
