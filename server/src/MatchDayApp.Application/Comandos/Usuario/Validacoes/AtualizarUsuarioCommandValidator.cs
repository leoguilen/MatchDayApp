using FluentValidation;
using MatchDayApp.Domain.Resources;

namespace MatchDayApp.Application.Comandos.Usuario.Validacoes
{
    public class AtualizarUsuarioCommandValidator : AbstractValidator<AtualizarUsuarioCommand>
    {
        public AtualizarUsuarioCommandValidator()
        {
            // Nome
            RuleFor(prop => prop.Usuario.Nome)
                .NotEmpty().WithMessage(Dicionario.MV005);
            RuleFor(prop => prop.Usuario.Nome)
                .MinimumLength(4).WithMessage(Dicionario.MV006)
                .When(x => !string.IsNullOrEmpty(x.Usuario.Nome));
            RuleFor(prop => prop.Usuario.Nome)
                .Matches(@"^[a-zA-Z]*$").WithMessage(Dicionario.MV009);

            // Sobrenome
            RuleFor(prop => prop.Usuario.Sobrenome)
                .NotEmpty().WithMessage(Dicionario.MV007);
            RuleFor(prop => prop.Usuario.Sobrenome)
                .MinimumLength(4).WithMessage(Dicionario.MV008)
                .When(x => !string.IsNullOrEmpty(x.Usuario.Sobrenome));
            RuleFor(prop => prop.Usuario.Sobrenome)
                .Matches(@"^[a-zA-Z]*$").WithMessage(Dicionario.MV010);

            // Email
            RuleFor(prop => prop.Usuario.Email)
                .EmailAddress().WithMessage(Dicionario.MV011);

            // Username
            RuleFor(prop => prop.Usuario.Username)
                .NotEmpty().WithMessage(Dicionario.MV012);
        }
    }
}
