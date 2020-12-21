using MatchDayApp.Application.Models;
using MediatR;

namespace MatchDayApp.Application.Comandos.Partida
{
    public class MarcarPartidaCommand : IRequest<PartidaModel>
    {
        public PartidaModel Partida { get; set; }
    }
}
