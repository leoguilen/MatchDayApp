using MatchDayApp.Application.Models;
using MediatR;
using System.Collections.Generic;

namespace MatchDayApp.Application.Queries.Partida
{
    public class ObterPartidasQuery : IRequest<IReadOnlyList<PartidaModel>>
    {
    }
}
