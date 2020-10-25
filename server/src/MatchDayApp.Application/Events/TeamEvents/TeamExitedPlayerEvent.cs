using MatchDayApp.Application.Models;
using MediatR;
using System;

namespace MatchDayApp.Application.Events.TeamEvents
{
    public class TeamExitedPlayerEvent : INotification
    {
        public Guid TeamId { get; set; }
        public UserModel User { get; set; }
    }
}
