using MatchDayApp.Application.Models;
using MediatR;
using System;

namespace MatchDayApp.Application.Events.TeamEvents
{
    public class TeamRemovedPlayerEvent : INotification
    {
        public Guid TeamId { get; set; }
        public UserModel User { get; set; }
    }
}
