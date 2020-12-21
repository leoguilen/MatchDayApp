using MediatR;
using System;

namespace MatchDayApp.Application.Comandos.Time
{
    public class DeletarTimeCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
