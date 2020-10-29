using MediatR;
using System;

namespace MatchDayApp.Application.Commands.ScheduleMatch
{
    public class UncheckMatchCommand : IRequest<bool>
    {
        public Guid MatchId { get; set; }
    }
}
