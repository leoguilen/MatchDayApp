using MatchDayApp.Application.Events.TeamEvents;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Handlers
{
    public class TeamEventHandler :
        INotificationHandler<TeamAddedPlayerEvent>,
        INotificationHandler<TeamCanceledMatchEvent>,
        INotificationHandler<TeamExitedPlayerEvent>,
        INotificationHandler<TeamRemovedPlayerEvent>,
        INotificationHandler<TeamScheduledMatchEvent>
    {
        public Task Handle(TeamAddedPlayerEvent notification, CancellationToken cancellationToken)
        {
            //Enviar notificação para integrante e proprietario do time
            return Task.CompletedTask;
        }

        public Task Handle(TeamCanceledMatchEvent notification, CancellationToken cancellationToken)
        {
            //Enviar notificação para integrantes dos times
            return Task.CompletedTask;
        }

        public Task Handle(TeamExitedPlayerEvent notification, CancellationToken cancellationToken)
        {
            //Enviar notificação para proprietario do time
            return Task.CompletedTask;
        }

        public Task Handle(TeamRemovedPlayerEvent notification, CancellationToken cancellationToken)
        {
            //Enviar notificação para integrante do time
            return Task.CompletedTask;
        }

        public Task Handle(TeamScheduledMatchEvent notification, CancellationToken cancellationToken)
        {
            //Enviar notificação para integrantes dos times
            return Task.CompletedTask;
        }
    }
}
