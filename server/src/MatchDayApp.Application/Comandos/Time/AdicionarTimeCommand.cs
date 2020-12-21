using MatchDayApp.Application.Models;
using MediatR;

namespace MatchDayApp.Application.Comandos.Time
{
    public class AdicionarTimeCommand : IRequest<TimeModel>
    {
        public TimeModel Time { get; set; }
    }
}
