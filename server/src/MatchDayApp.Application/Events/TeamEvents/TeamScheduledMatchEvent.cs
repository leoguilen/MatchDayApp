using MatchDayApp.Application.Models;
using MediatR;

namespace MatchDayApp.Application.Events.TeamEvents
{
    public class TeamScheduledMatchEvent : INotification
    {
        public ScheduleMatchModel Match { get; set; }
    }
}
