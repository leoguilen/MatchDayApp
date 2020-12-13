using MediatR;
using System;

namespace MatchDayApp.Application.Events.UserEvents
{
    public class UserRegisteredEvent : INotification
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
