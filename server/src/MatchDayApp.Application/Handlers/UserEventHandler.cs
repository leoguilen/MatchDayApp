using MatchDayApp.Application.Events.UserEvents;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Handlers
{
    public class UserEventHandler :
        INotificationHandler<UserRegisteredEvent>,
        INotificationHandler<UserResetPasswordEvent>,
        INotificationHandler<UserDeletedEvent>,
        INotificationHandler<UserUpdatedEvent>
    {
        public Task Handle(UserUpdatedEvent notification, CancellationToken cancellationToken)
        {
            //Enviar notificação para usuário
            return Task.CompletedTask;
        }

        public Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
        {
            //Enviar notificação para usuário
            return Task.CompletedTask;
        }

        public Task Handle(UserResetPasswordEvent notification, CancellationToken cancellationToken)
        {
            //Enviar notificação para usuário
            return Task.CompletedTask;
        }

        public Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
        {
            //Enviar notificação para usuário
            return Task.CompletedTask;
        }
    }
}
