using MediatR;
using System;

namespace MatchDayApp.Application.Events.UserEvents
{
    public class UserUpdatedEvent : INotification
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string[] Properties { get; set; }
    }
}
