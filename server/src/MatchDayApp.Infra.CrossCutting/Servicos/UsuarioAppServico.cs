using AutoMapper;
using MatchDayApp.Application.Comandos.Usuario;
using MatchDayApp.Application.Events.Usuario;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Queries.Usuario;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Query;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Usuario;
using MatchDayApp.Infra.CrossCutting.Servicos.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.CrossCutting.Servicos
{
    public class UsuarioAppServico : IUsuarioAppServico
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UsuarioAppServico(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator
                ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<UsuarioModel> AtualizarUsuarioAsync(Guid usuarioId, AtualizarUsuarioRequest request)
        {
            var atualizarUsuarioCommand = new AtualizarUsuarioCommand
            {
                UsuarioId = usuarioId,
                Usuario = _mapper
                    .Map<UsuarioModel>(request)
            };

            var result = await _mediator
                .Send(atualizarUsuarioCommand);

            return result;
        }

        public async Task<bool> DeletarUsuarioAsync(Guid usuarioId)
        {
            var deletarUsuarioCommand = new DeletarUsuarioCommand
            {
                Id = usuarioId
            };

            var result = await _mediator
                .Send(deletarUsuarioCommand);

            if (!result)
                return result;

            await _mediator.Publish(new UsuarioDeletadoEvent
            {
                Id = usuarioId
            });

            return result;
        }

        public async Task<UsuarioModel> ObterUsuarioPorEmailAsync(string usuarioEmail)
        {
            var obterUsuarioPorEmailQuery = new ObterUsuarioPorEmailQuery
            {
                Email = usuarioEmail
            };

            var usuario = await _mediator
                .Send(obterUsuarioPorEmailQuery);

            return usuario;
        }

        public async Task<UsuarioModel> ObterUsuarioPorIdAsync(Guid usuarioId)
        {
            var obterUsuarioPorIdQuery = new ObterUsuarioPorIdQuery
            {
                Id = usuarioId
            };

            var usuario = await _mediator
                .Send(obterUsuarioPorIdQuery);

            return usuario;
        }

        public async Task<IReadOnlyList<UsuarioModel>> ObterUsuariosAsync(PaginacaoQuery pagination = null)
        {
            var obterUsuariosQuery = new ObterUsuariosQuery { };

            var usuarios = await _mediator
                .Send(obterUsuariosQuery);

            var skip = (pagination.NumeroPagina - 1) * pagination.QuantidadePagina;

            return usuarios
                .Skip(skip)
                .Take(pagination.QuantidadePagina)
                .ToList();
        }
    }
}
