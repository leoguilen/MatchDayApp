using MatchDayApp.Application.Models;
using MediatR;
using System;

namespace MatchDayApp.Application.Queries.Quadra
{
    public class ObterQuadraPorIdQuery : IRequest<QuadraModel>
    {
        public Guid Id { get; set; }
    }
}
