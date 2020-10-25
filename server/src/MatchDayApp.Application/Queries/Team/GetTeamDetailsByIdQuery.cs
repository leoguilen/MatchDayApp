using MatchDayApp.Application.Models;
using MediatR;
using System;

namespace MatchDayApp.Application.Queries.Team
{
    public class GetTeamDetailsByIdQuery : IRequest<TeamModel>
    {
        public Guid Id { get; set; }
    }
}
