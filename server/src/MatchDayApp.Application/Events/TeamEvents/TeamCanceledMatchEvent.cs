using MatchDayApp.Application.Models;
using MediatR;

namespace MatchDayApp.Application.Events.TeamEvents
{
    public class TeamCanceledMatchEvent : INotification
    {
        public ScheduleMatchModel Match { get; set; }
    }
}
