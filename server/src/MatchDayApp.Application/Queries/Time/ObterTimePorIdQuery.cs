using MatchDayApp.Application.Models;
using MediatR;
using System;

namespace MatchDayApp.Application.Queries.Time
{
    public class ObterTimePorIdQuery : IRequest<TimeModel>
    {
        public Guid Id { get; set; }
    }
}
