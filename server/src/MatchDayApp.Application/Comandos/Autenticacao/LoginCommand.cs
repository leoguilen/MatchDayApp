using MatchDayApp.Application.Models;
using MatchDayApp.Application.Models.Auth;
using MediatR;

namespace MatchDayApp.Application.Comandos.Autenticacao
{
    public class LoginCommand : IRequest<AutenticacaoResult>
    {
        public LoginModel Login { get; set; }
    }
}
