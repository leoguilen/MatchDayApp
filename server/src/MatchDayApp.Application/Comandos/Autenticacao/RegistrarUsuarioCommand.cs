using MatchDayApp.Application.Models;
using MatchDayApp.Application.Models.Auth;
using MediatR;

namespace MatchDayApp.Application.Comandos.Autenticacao
{
    public class RegistrarUsuarioCommand : IRequest<AutenticacaoResult>
    {
        public RegistrarUsuarioModel RegistrarUsuario { get; set; }
    }
}
