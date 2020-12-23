using MediatR;
using System;

namespace MatchDayApp.Application.Comandos.Partida
{
    public class ConfirmarPartidaCommand : IRequest<bool>
    {
        public Guid TimeId { get; set; }
        public Guid PartidaId { get; set; }
    }
}
