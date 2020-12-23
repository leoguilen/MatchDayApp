using MediatR;
using System;

namespace MatchDayApp.Application.Events.Quadra
{
    public class QuadraPartidaCanceladaEvent : INotification
    {
        public Guid PartidaId { get; set; }
    }
}
