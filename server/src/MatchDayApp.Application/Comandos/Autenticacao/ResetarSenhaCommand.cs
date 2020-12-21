using MatchDayApp.Application.Models;
using MatchDayApp.Application.Models.Auth;
using MediatR;

namespace MatchDayApp.Application.Comandos.Autenticacao
{
    public class ResetarSenhaCommand : IRequest<AutenticacaoResult>
    {
        public ResetarSenhaModel ResetarSenha { get; set; }
    }
}
