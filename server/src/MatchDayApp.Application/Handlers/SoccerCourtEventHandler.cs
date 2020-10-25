using MatchDayApp.Application.Events.SoccerCourtEvents;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Handlers
{
    public class SoccerCourtEventHandler :
        INotificationHandler<SoccerCourtCanceledMatchEvent>,
        INotificationHandler<SoccerCourtScheduledMatchEvent>
    {
        public Task Handle(SoccerCourtScheduledMatchEvent notification, CancellationToken cancellationToken)
        {
            //Enviar notificação para proprietario da quadra
            return Task.CompletedTask;
        }

        public Task Handle(SoccerCourtCanceledMatchEvent notification, CancellationToken cancellationToken)
        {
            //Enviar notificação para proprietario da quadra
            return Task.CompletedTask;
        }
    }
}
