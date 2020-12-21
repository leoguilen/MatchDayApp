using FluentValidation;
using MatchDayApp.Domain.Resources;

namespace MatchDayApp.Application.Comandos.Time.Validacoes
{
    public class AtualizarTimeCommandValidator : AbstractValidator<AtualizarTimeCommand>
    {
        public AtualizarTimeCommandValidator()
        {
            // Name
            RuleFor(prop => prop.Time.Nome)
                .NotEmpty().WithMessage(Dicionario.MV016);
            RuleFor(prop => prop.Time.Nome)
                .MinimumLength(4).WithMessage(Dicionario.MV017)
                .When(x => !string.IsNullOrEmpty(x.Time.Nome));
            RuleFor(prop => prop.Time.Nome)
                .Matches(@"^[a-zA-Z]*$").WithMessage(Dicionario.MV018);
        }
    }
}
