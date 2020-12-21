using MatchDayApp.Application.Models;
using MediatR;
using System.Collections.Generic;

namespace MatchDayApp.Application.Queries.SoccerCourt
{
    public class GetSoccerCourtsQuery : IRequest<IReadOnlyList<QuadraModel>>
    {
    }
}
