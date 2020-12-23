using MatchDayApp.Application.Models;
using MatchDayApp.Application.Models.Auth;
using MediatR;

namespace MatchDayApp.Application.Comandos.Autenticacao
{
    public class ConfirmarEmailCommand : IRequest<AutenticacaoResult>
    {
        public ConfirmacaoEmailModel ConfirmarEmail { get; set; }
    }
}
