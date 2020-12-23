using FluentValidation;
using MatchDayApp.Application.Comandos.Autenticacao;
using MatchDayApp.Domain.Resources;

namespace MatchDayApp.Application.Comandos.Autenticao.Validacoes
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            // Email
            RuleFor(prop => prop.Login.Email)
                .EmailAddress().WithMessage(Dicionario.MV011);
        }
    }
}
