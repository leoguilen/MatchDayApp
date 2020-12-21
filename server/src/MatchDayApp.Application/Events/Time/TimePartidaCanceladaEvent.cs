using MediatR;
using System;

namespace MatchDayApp.Application.Events.Time
{
    public class TimePartidaCanceladaEvent : INotification
    {
        public Guid PartidaId { get; set; }
    }
}
