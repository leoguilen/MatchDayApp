using MediatR;

namespace MatchDayApp.Application.Events.UserEvents
{
    public class UserRegisteredEvent : INotification
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
