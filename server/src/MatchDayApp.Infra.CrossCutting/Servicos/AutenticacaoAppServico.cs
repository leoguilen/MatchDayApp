using AutoMapper;
using MatchDayApp.Application.Comandos.Autenticacao;
using MatchDayApp.Application.Events.Usuario;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Models.Auth;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Auth;
using MatchDayApp.Infra.CrossCutting.Servicos.Interfaces;
using MediatR;
using System;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.CrossCutting.Servicos
{
    public class AutenticacaoAppServico : IAutenticacaoAppServico
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AutenticacaoAppServico(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<AutenticacaoResult> ConfirmarEmailAsync(Guid chave)
        {
            var confirmarEmailCommand = new ConfirmarEmailCommand
            {
                ConfirmarEmail = new ConfirmacaoEmailModel { ChaveDeConfirmacao = chave }
            };

            var autenticacaoResult = await _mediator
                .Send(confirmarEmailCommand);

            return autenticacaoResult;
        }

        public async Task<AutenticacaoResult> LoginAsync(LoginRequest request)
        {
            var loginCommand = new LoginCommand
            {
                Login = _mapper
                    .Map<LoginModel>(request)
            };

            var autenticacaoResult = await _mediator
                .Send(loginCommand);

            return autenticacaoResult;
        }

        public async Task<AutenticacaoResult> RegistrarUsuarioAsync(RegistrarUsuarioRequest request)
        {
            var registrarUsuarioCommand = new RegistrarUsuarioCommand
            {
                RegistrarUsuario = _mapper
                    .Map<RegistrarUsuarioModel>(request)
            };

            var autenticacaoResult = await _mediator
                .Send(registrarUsuarioCommand);

            if (!autenticacaoResult.Sucesso)
                return autenticacaoResult;

            await _mediator.Publish(new UsuarioRegistradoEvent
            {
                UsuarioId = autenticacaoResult.Usuario.Id,
                Nome = $"{request.Nome} {request.Sobrenome}",
                Telefone = request.Telefone,
                Email = request.Email
            });

            return autenticacaoResult;
        }

        public async Task<AutenticacaoResult> ResetarSenhaAsync(ResetarSenhaRequest request)
        {
            var resetarSenhaCommand = new ResetarSenhaCommand
            {
                ResetarSenha = _mapper
                    .Map<ResetarSenhaModel>(request)
            };

            var autenticacaoResult = await _mediator
                .Send(resetarSenhaCommand);

            if (!autenticacaoResult.Sucesso)
                return autenticacaoResult;

            await _mediator.Publish(new UsuarioSenhaResetadaEvent
            {
                Email = request.Email
            });

            return autenticacaoResult;
        }
    }
}
