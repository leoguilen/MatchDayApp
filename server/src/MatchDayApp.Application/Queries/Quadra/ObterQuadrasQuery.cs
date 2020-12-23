using MatchDayApp.Application.Models;
using MediatR;
using System.Collections.Generic;

namespace MatchDayApp.Application.Queries.Quadra
{
    public class ObterQuadrasQuery : IRequest<IReadOnlyList<QuadraModel>>
    {
    }
}
