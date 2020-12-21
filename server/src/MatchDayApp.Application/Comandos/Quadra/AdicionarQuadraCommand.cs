using MatchDayApp.Application.Models;
using MediatR;

namespace MatchDayApp.Application.Comandos.Quadra
{
    public class AdicionarQuadraCommand : IRequest<QuadraModel>
    {
        public QuadraModel Quadra { get; set; }
    }
}
