using MatchDayApp.Application.Models;
using MediatR;

namespace MatchDayApp.Application.Events.SoccerCourtEvents
{
    public class SoccerCourtScheduledMatchEvent : INotification
    {
        public ScheduleMatchModel Match { get; set; }
    }
}
