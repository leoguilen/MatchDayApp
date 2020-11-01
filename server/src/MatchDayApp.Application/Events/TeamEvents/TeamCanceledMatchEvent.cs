using MediatR;
using System;

namespace MatchDayApp.Application.Events.TeamEvents
{
    public class TeamCanceledMatchEvent : INotification
    {
        public Guid MatchId { get; set; }
    }
}
