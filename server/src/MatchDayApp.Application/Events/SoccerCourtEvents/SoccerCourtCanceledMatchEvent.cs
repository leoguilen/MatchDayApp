using MatchDayApp.Application.Models;
using MediatR;
using System;

namespace MatchDayApp.Application.Events.SoccerCourtEvents
{
    public class SoccerCourtCanceledMatchEvent : INotification
    {
        public Guid SoccerCourtId { get; set; }
        public ScheduleMatchModel Match { get; set; }
    }
}
