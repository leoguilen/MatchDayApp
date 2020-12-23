using MediatR;

namespace MatchDayApp.Application.Events.Usuario
{
    public class UsuarioSenhaResetadaEvent : INotification
    {
        public string Email { get; set; }
    }
}
