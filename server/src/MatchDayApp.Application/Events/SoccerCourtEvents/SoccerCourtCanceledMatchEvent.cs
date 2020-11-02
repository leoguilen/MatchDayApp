using MediatR;
using System;

namespace MatchDayApp.Application.Events.SoccerCourtEvents
{
    public class SoccerCourtCanceledMatchEvent : INotification
    {
        public Guid MatchId { get; set; }
    }
}
