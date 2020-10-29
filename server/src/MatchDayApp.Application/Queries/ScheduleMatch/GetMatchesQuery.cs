using MatchDayApp.Application.Models;
using MediatR;
using System.Collections.Generic;

namespace MatchDayApp.Application.Queries.ScheduleMatch
{
    public class GetMatchesQuery : IRequest<IReadOnlyList<ScheduleMatchModel>>
    {
    }
}
