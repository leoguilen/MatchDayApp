using MatchDayApp.Application.Models;
using MediatR;
using System;

namespace MatchDayApp.Application.Queries.ScheduleMatch
{
    public class GetMatchByIdQuery : IRequest<ScheduleMatchModel>
    {
        public Guid MatchId { get; set; }
    }
}
