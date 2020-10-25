using MatchDayApp.Application.Models;
using MediatR;
using System;

namespace MatchDayApp.Application.Commands.SoccerCourt
{
    public class UpdateSoccerCourtCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public SoccerCourtModel SoccerCourt { get; set; }
    }
}
