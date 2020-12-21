using MatchDayApp.Application.Models;
using MediatR;
using System;

namespace MatchDayApp.Application.Commands.Team
{
    public class UpdateTeamCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public TimeModel Team { get; set; }
    }
}
