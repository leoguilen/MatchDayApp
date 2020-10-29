using MatchDayApp.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace MatchDayApp.Application.Queries.ScheduleMatch
{
    public class GetMatchesBySoccerCourtIdQuery : IRequest<IEnumerable<ScheduleMatchModel>>
    {
        public Guid SoccerCourtId { get; set; }
    }
}
