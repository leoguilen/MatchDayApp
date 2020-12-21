using MediatR;
using System;

namespace MatchDayApp.Application.Events.Usuario
{
    public class UsuarioRegistradoEvent : INotification
    {
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
    }
}
