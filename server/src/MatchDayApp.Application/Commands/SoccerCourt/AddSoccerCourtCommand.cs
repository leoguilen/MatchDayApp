using MatchDayApp.Application.Models;
using MediatR;

namespace MatchDayApp.Application.Commands.SoccerCourt
{
    public class AddSoccerCourtCommand : IRequest<bool>
    {
        public SoccerCourtModel SoccerCourt { get; set; }
    }
}
