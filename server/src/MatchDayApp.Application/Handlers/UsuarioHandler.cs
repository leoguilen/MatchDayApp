using MatchDayApp.Application.Comandos.Usuario;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Queries.Usuario;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Handlers
{
    public class UsuarioHandler :
        IRequestHandler<DeletarUsuarioCommand, bool>,
        IRequestHandler<AtualizarUsuarioCommand, UsuarioModel>,
        IRequestHandler<ObterUsuarioPorEmailQuery, UsuarioModel>,
        IRequestHandler<ObterUsuarioPorIdQuery, UsuarioModel>,
        IRequestHandler<ObterUsuariosQuery, IReadOnlyList<UsuarioModel>>
    {
        private readonly IUsuarioServico _usuarioServico;

        public UsuarioHandler(IUsuarioServico usuarioServico)
        {
            _usuarioServico = usuarioServico
                ?? throw new System.ArgumentNullException(nameof(usuarioServico));
        }

        public async Task<IReadOnlyList<UsuarioModel>> Handle(ObterUsuariosQuery request, CancellationToken cancellationToken)
        {
            return await _usuarioServico.ObterUsuariosAsync();
        }

        public async Task<UsuarioModel> Handle(ObterUsuarioPorIdQuery request, CancellationToken cancellationToken)
        {
            return await _usuarioServico.ObterUsuarioPorIdAsync(request.Id);
        }

        public async Task<UsuarioModel> Handle(ObterUsuarioPorEmailQuery request, CancellationToken cancellationToken)
        {
            return await _usuarioServico.ObterUsuarioPorEmailAsync(request.Email);
        }

        public async Task<UsuarioModel> Handle(AtualizarUsuarioCommand request, CancellationToken cancellationToken)
        {
            return await _usuarioServico.AtualizarUsuarioAsync(request.UsuarioId, request.Usuario);
        }

        public async Task<bool> Handle(DeletarUsuarioCommand request, CancellationToken cancellationToken)
        {
            return await _usuarioServico.DeletarUsuarioAsync(request.Id);
        }
    }
}
