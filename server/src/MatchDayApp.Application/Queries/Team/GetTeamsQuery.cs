using MatchDayApp.Application.Models;
using MediatR;
using System.Collections.Generic;

namespace MatchDayApp.Application.Queries.Team
{
    public class GetTeamsQuery : IRequest<IReadOnlyList<TimeModel>>
    {
    }
}
