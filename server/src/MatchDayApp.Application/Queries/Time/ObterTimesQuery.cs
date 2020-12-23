using MatchDayApp.Application.Models;
using MediatR;
using System.Collections.Generic;

namespace MatchDayApp.Application.Queries.Time
{
    public class ObterTimesQuery : IRequest<IReadOnlyList<TimeModel>>
    {
    }
}
