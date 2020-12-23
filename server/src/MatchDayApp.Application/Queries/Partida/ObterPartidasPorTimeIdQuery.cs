using MatchDayApp.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace MatchDayApp.Application.Queries.Partida
{
    public class ObterPartidasPorTimeIdQuery : IRequest<IEnumerable<PartidaModel>>
    {
        public Guid TimeId { get; set; }
    }
}
