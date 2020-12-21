using AutoMapper;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Services
{
    public class UsuarioServico : IUsuarioServico
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UsuarioServico(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> AtualizarUsuarioAsync(Guid usuarioId, UsuarioModel usuario)
        {
            var usuarioAtualizar = await _uow.UsuarioRepositorio
                .GetByIdAsync(usuarioId);

            if (usuarioAtualizar != null)
            {
                usuarioAtualizar.Nome = usuario.Nome ?? usuarioAtualizar.Nome;
                usuarioAtualizar.Sobrenome = usuario.Sobrenome ?? usuarioAtualizar.Sobrenome;
                usuarioAtualizar.Email = usuario.Email ?? usuarioAtualizar.Email;
                usuarioAtualizar.Telefone = usuario.Telefone ?? usuarioAtualizar.Telefone;
                usuarioAtualizar.Username = usuario.Username ?? usuarioAtualizar.Username;
                usuarioAtualizar.TipoUsuario = usuario.TipoUsuario;
                usuarioAtualizar.Avatar = usuario.Avatar ?? usuarioAtualizar.Avatar;

                await _uow.UsuarioRepositorio
                    .SaveAsync(usuarioAtualizar);

                return true;
            }

            return false;
        }

        public async Task<bool> DeletarUsuarioAsync(Guid usuarioId)
        {
            var usuario = await _uow.UsuarioRepositorio
                .GetByIdAsync(usuarioId);

            if (usuario != null)
            {
                usuario.Deletado = true;

                await _uow.UsuarioRepositorio
                    .SaveAsync(usuario);

                return true;
            }

            return false;
        }

        public async Task<UsuarioModel> ObterUsuarioPorEmailAsync(string usuarioEmail)
        {
            var usuario = await _uow.UsuarioRepositorio
                .ObterUsuarioPorEmailAsync(usuarioEmail);

            if (usuario != null && usuario.Deletado is false)
                return _mapper.Map<UsuarioModel>(usuario);

            return null;
        }

        public async Task<UsuarioModel> ObterUsuarioPorIdAsync(Guid usuarioId)
        {
            var usuario = await _uow.UsuarioRepositorio
                .GetByIdAsync(usuarioId);

            if (usuario != null && usuario.Deletado is false)
                return _mapper.Map<UsuarioModel>(usuario);

            return null;
        }

        public async Task<IReadOnlyList<UsuarioModel>> ObterUsuariosAsync()
        {
            var users = await _uow.UsuarioRepositorio
                .ListAllAsync();

            return _mapper.Map<IReadOnlyList<UsuarioModel>>(
                users.Where(us => us.Deletado is false));
        }
    }
}
