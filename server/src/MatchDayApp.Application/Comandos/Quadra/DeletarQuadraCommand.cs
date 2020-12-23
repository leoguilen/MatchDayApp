using MediatR;
using System;

namespace MatchDayApp.Application.Comandos.Quadra
{
    public class DeletarQuadraCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
