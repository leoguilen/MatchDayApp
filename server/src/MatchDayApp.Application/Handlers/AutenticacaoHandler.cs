using MatchDayApp.Application.Comandos.Autenticacao;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Handlers
{
    public class AutenticacaoHandler :
        IRequestHandler<LoginCommand, AutenticacaoResult>,
        IRequestHandler<RegistrarUsuarioCommand, AutenticacaoResult>,
        IRequestHandler<ResetarSenhaCommand, AutenticacaoResult>,
        IRequestHandler<ConfirmarEmailCommand, AutenticacaoResult>
    {
        private readonly IAutenticacaoServico _autenticacaoServico;

        public AutenticacaoHandler(IAutenticacaoServico autenticacaoServico)
        {
            _autenticacaoServico = autenticacaoServico
                ?? throw new System.ArgumentNullException(nameof(autenticacaoServico));
        }

        public async Task<AutenticacaoResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await _autenticacaoServico.LoginAsync(request.Login);
        }

        public async Task<AutenticacaoResult> Handle(RegistrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            return await _autenticacaoServico.RegistrarUsuarioAsync(request.RegistrarUsuario);
        }

        public async Task<AutenticacaoResult> Handle(ResetarSenhaCommand request, CancellationToken cancellationToken)
        {
            return await _autenticacaoServico.ResetarSenhaAsync(request.ResetarSenha);
        }

        public async Task<AutenticacaoResult> Handle(ConfirmarEmailCommand request, CancellationToken cancellationToken)
        {
            return await _autenticacaoServico.ConfirmarEmailAsync(request.ConfirmarEmail);
        }
    }
}
