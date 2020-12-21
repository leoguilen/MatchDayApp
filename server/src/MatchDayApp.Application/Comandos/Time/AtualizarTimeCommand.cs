using MatchDayApp.Application.Models;
using MediatR;
using System;

namespace MatchDayApp.Application.Comandos.Time
{
    public class AtualizarTimeCommand : IRequest<TimeModel>
    {
        public Guid Id { get; set; }
        public TimeModel Time { get; set; }
    }
}
