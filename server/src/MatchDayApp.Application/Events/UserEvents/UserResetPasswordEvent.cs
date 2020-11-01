using MediatR;

namespace MatchDayApp.Application.Events.UserEvents
{
    public class UserResetPasswordEvent : INotification
    {
        public string Email { get; set; }
    }
}
