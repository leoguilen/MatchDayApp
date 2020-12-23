using MatchDayApp.Application.Models;
using MediatR;
using System;

namespace MatchDayApp.Application.Queries.Partida
{
    public class ObterPartidaPorIdQuery : IRequest<PartidaModel>
    {
        public Guid PartidaId { get; set; }
    }
}
