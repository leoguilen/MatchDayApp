using MatchDayApp.Application.Models;
using MediatR;

namespace MatchDayApp.Application.Commands.ScheduleMatch
{
    public class ScheduleMatchCommand : IRequest<bool>
    {
        public ScheduleMatchModel Match { get; set; }
    }
}
