using MatchDayApp.Application.Models;
using MediatR;

namespace MatchDayApp.Application.Commands.Team
{
    public class AddTeamCommand : IRequest<bool>
    {
        public TeamModel Team { get; set; }
    }
}
