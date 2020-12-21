using MatchDayApp.Application.Events.Quadra;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Handlers
{
    public class QuadraEventHandler :
        INotificationHandler<QuadraPartidaCanceladaEvent>,
        INotificationHandler<QuadraPartidaMarcadaEvent>
    {
        public Task Handle(QuadraPartidaMarcadaEvent notification, CancellationToken cancellationToken)
        {
            //Enviar notificação para proprietario da quadra
            return Task.CompletedTask;
        }

        public Task Handle(QuadraPartidaCanceladaEvent notification, CancellationToken cancellationToken)
        {
            //Enviar notificação para proprietario da quadra
            return Task.CompletedTask;
        }
    }
}
