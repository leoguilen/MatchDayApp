using MediatR;
using System;

namespace MatchDayApp.Application.Events.UserEvents
{
    public class UserDeletedEvent : INotification
    {
        public Guid Id { get; set; }
    }
}
