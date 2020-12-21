using MatchDayApp.Application.Models;
using MediatR;
using System;

namespace MatchDayApp.Application.Queries.SoccerCourt
{
    public class GetSoccerCourtDetailsByIdQuery : IRequest<QuadraModel>
    {
        public Guid Id { get; set; }
    }
}
