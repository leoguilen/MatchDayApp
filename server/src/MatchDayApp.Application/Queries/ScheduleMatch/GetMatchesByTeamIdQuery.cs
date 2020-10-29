using MatchDayApp.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace MatchDayApp.Application.Queries.ScheduleMatch
{
    public class GetMatchesByTeamIdQuery : IRequest<IEnumerable<ScheduleMatchModel>>
    {
        public Guid TeamId { get; set; }
    }
}
