using MatchDayApp.Application.Models;
using MediatR;

namespace MatchDayApp.Application.Events.Time
{
    public class TimePartidaMarcadaEvent : INotification
    {
        public PartidaModel Partida { get; set; }
    }
}
