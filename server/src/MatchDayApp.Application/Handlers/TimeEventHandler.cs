using MatchDayApp.Application.Events.Time;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Handlers
{
    public class TimeEventHandler :
        INotificationHandler<TimeJogadorAdicionadoEvent>,
        INotificationHandler<TimePartidaCanceladaEvent>,
        INotificationHandler<TimeJogadorSaiuEvent>,
        INotificationHandler<TimeJogadorRemovidoEvent>,
        INotificationHandler<TimePartidaMarcadaEvent>
    {
        public Task Handle(TimeJogadorAdicionadoEvent notification, CancellationToken cancellationToken)
        {
            //Enviar notificação para integrante e proprietario do time
            return Task.CompletedTask;
        }

        public Task Handle(TimePartidaCanceladaEvent notification, CancellationToken cancellationToken)
        {
            //Enviar notificação para integrantes dos times
            return Task.CompletedTask;
        }

        public Task Handle(TimeJogadorSaiuEvent notification, CancellationToken cancellationToken)
        {
            //Enviar notificação para proprietario do time
            return Task.CompletedTask;
        }

        public Task Handle(TimeJogadorRemovidoEvent notification, CancellationToken cancellationToken)
        {
            //Enviar notificação para integrante do time
            return Task.CompletedTask;
        }

        public Task Handle(TimePartidaMarcadaEvent notification, CancellationToken cancellationToken)
        {
            //Enviar notificação para integrantes dos times
            return Task.CompletedTask;
        }
    }
}
