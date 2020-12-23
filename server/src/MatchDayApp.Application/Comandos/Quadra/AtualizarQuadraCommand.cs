using MatchDayApp.Application.Models;
using MediatR;
using System;

namespace MatchDayApp.Application.Comandos.Quadra
{
    public class AtualizarQuadraCommand : IRequest<QuadraModel>
    {
        public Guid Id { get; set; }
        public QuadraModel Quadra { get; set; }
    }
}
