using MediatR;
using System;

namespace MatchDayApp.Application.Commands.SoccerCourt
{
    public class DeleteSoccerCourtCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
