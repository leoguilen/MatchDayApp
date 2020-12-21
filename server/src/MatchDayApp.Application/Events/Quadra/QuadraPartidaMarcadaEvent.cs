using MatchDayApp.Application.Models;
using MediatR;

namespace MatchDayApp.Application.Events.Quadra
{
    public class QuadraPartidaMarcadaEvent : INotification
    {
        public PartidaModel Partida { get; set; }
    }
}
