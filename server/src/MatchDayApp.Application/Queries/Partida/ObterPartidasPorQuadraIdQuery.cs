using MatchDayApp.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace MatchDayApp.Application.Queries.Partida
{
    public class ObterPartidasPorQuadraIdQuery : IRequest<IEnumerable<PartidaModel>>
    {
        public Guid QuadraId { get; set; }
    }
}
