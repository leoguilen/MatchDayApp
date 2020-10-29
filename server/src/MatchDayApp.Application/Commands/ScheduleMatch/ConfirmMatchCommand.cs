using MediatR;
using System;

namespace MatchDayApp.Application.Commands.ScheduleMatch
{
    public class ConfirmMatchCommand : IRequest<bool>
    {
        public Guid TeamId { get; set; }
        public Guid MatchId { get; set; }
    }
}
