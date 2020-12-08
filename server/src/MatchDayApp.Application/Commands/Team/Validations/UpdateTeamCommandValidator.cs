using FluentValidation;
using MatchDayApp.Domain.Resources;

namespace MatchDayApp.Application.Commands.Team.Validations
{
    public class UpdateTeamCommandValidator : AbstractValidator<UpdateTeamCommand>
    {
        public UpdateTeamCommandValidator()
        {
            // Name
            RuleFor(prop => prop.Team.Name)
                .NotEmpty().WithMessage(Dictionary.MV016);
            RuleFor(prop => prop.Team.Name)
                .MinimumLength(4).WithMessage(Dictionary.MV017)
                .When(x => !string.IsNullOrEmpty(x.Team.Name));
            RuleFor(prop => prop.Team.Name)
                .Matches(@"^[a-zA-Z]*$").WithMessage(Dictionary.MV018);
        }
    }
}
