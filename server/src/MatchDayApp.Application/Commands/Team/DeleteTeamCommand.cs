using MediatR;
using System;

namespace MatchDayApp.Application.Commands.Team
{
    public class DeleteTeamCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
