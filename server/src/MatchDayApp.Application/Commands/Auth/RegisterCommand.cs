using MatchDayApp.Application.Models;
using MatchDayApp.Application.Models.Auth;
using MediatR;

namespace MatchDayApp.Application.Commands.Auth
{
    public class RegisterCommand : IRequest<AuthenticationResult>
    {
        public RegistrarUsuarioModel Register { get; set; }
    }
}
