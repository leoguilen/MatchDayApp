using MediatR;
using System;

namespace MatchDayApp.Application.Events.Usuario
{
    public class UsuarioDeletadoEvent : INotification
    {
        public Guid Id { get; set; }
    }
}
