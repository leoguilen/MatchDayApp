using MatchDayApp.Application.Models;
using MediatR;
using System;

namespace MatchDayApp.Application.Comandos.Partida
{
    public class DesmarcarPartidaCommand : IRequest<PartidaModel>
    {
        public Guid PartidaId { get; set; }
    }
}
