using FluentValidation;
using MatchDayApp.Application.Comandos.Autenticacao;
using MatchDayApp.Domain.Resources;
using System.Text.RegularExpressions;

namespace MatchDayApp.Application.Comandos.Autenticao.Validacoes
{
    public class RegistrarUsuarioCommandValidator : AbstractValidator<RegistrarUsuarioCommand>
    {
        public RegistrarUsuarioCommandValidator()
        {
            // Nome
            RuleFor(prop => prop.RegistrarUsuario.Nome)
                .NotEmpty().WithMessage(Dicionario.MV005);
            RuleFor(prop => prop.RegistrarUsuario.Nome)
                .MinimumLength(4).WithMessage(Dicionario.MV006)
                .When(x => !string.IsNullOrEmpty(x.RegistrarUsuario.Nome));
            RuleFor(prop => prop.RegistrarUsuario.Nome)
                .Matches(@"^[a-zA-Z]*$").WithMessage(Dicionario.MV009);

            // Sobrenome
            RuleFor(prop => prop.RegistrarUsuario.Sobrenome)
                .NotEmpty().WithMessage(Dicionario.MV007);
            RuleFor(prop => prop.RegistrarUsuario.Sobrenome)
                .MinimumLength(4).WithMessage(Dicionario.MV008)
                .When(x => !string.IsNullOrEmpty(x.RegistrarUsuario.Sobrenome));
            RuleFor(prop => prop.RegistrarUsuario.Sobrenome)
                .Matches(@"^[a-zA-Z]*$").WithMessage(Dicionario.MV010);

            // Email
            RuleFor(prop => prop.RegistrarUsuario.Email)
                .EmailAddress().WithMessage(Dicionario.MV011);

            // Username
            RuleFor(prop => prop.RegistrarUsuario.Username)
                .NotEmpty().WithMessage(Dicionario.MV012);

            // Senha
            RuleFor(prop => prop.RegistrarUsuario.Senha)
                .NotEmpty().WithMessage(Dicionario.MV013);
            RuleFor(prop => prop.RegistrarUsuario.Senha)
                .Must(pwd => Regex.IsMatch(pwd, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$"))
                .WithMessage(Dicionario.MV014)
                .When(x => !string.IsNullOrEmpty(x.RegistrarUsuario.Senha));

            // Confirmação Senha
            RuleFor(prop => prop.RegistrarUsuario.ConfirmacaoSenha)
                .Equal(prop => prop.RegistrarUsuario.Senha)
                .WithMessage(Dicionario.MV015)
                .When(x => !string.IsNullOrEmpty(x.RegistrarUsuario.Senha));
        }
    }
}
