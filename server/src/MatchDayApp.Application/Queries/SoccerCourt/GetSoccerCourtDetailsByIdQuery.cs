using MatchDayApp.Application.Models;
using MediatR;
using System;

namespace MatchDayApp.Application.Queries.SoccerCourt
{
    public class GetSoccerCourtDetailsByIdQuery : IRequest<SoccerCourtModel>
    {
        public Guid Id { get; set; }
    }
}
