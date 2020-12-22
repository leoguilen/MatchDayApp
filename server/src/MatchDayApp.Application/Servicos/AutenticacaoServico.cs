using AutoMapper;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Models.Auth;
using MatchDayApp.Domain.Configuracoes;
using MatchDayApp.Domain.Entidades;
using MatchDayApp.Domain.Helpers;
using MatchDayApp.Domain.Resources;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Servicos
{
    public class AutenticacaoServico : IAutenticacaoServico
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly JwtConfiguracao _jwtOptions;

        public AutenticacaoServico(IUnitOfWork uow, IMapper mapper, JwtConfiguracao jwtOptions)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _jwtOptions = jwtOptions ?? throw new ArgumentNullException(nameof(jwtOptions));
        }

        public async Task<bool> AdicionarSolicitacaoConfirmacaoEmail(Guid usuarioId)
        {
            return await _uow.ConfirmacaoEmailRepositorio
                .AdicionarRequisicaoAsync(usuarioId);
        }

        public async Task<AutenticacaoResult> ConfirmarEmailAsync(ConfirmacaoEmailModel model)
        {
            var confirmacao = await _uow.ConfirmacaoEmailRepositorio
                .ObterRequisicaoPorChaveAsync(model.ChaveDeConfirmacao);

            if (confirmacao is null)
            {
                return new AutenticacaoResult
                {
                    Mensagem = Dicionario.ME006,
                    Sucesso = false
                };
            }

            _uow.ConfirmacaoEmailRepositorio
                .AtualizarRequisicao(confirmacao);

            var usuario = await _uow.UsuarioRepositorio
                .GetByIdAsync(confirmacao.UsuarioId);

            usuario.EmailConfirmado = true;
            await _uow.UsuarioRepositorio.SaveAsync(usuario);

            return new AutenticacaoResult
            {
                Mensagem = Dicionario.MS004,
                Sucesso = true,
            };
        }

        public async Task<AutenticacaoResult> LoginAsync(LoginModel login)
        {
            var usuario = await _uow.UsuarioRepositorio
                .ObterUsuarioPorEmailAsync(login.Email);

            if (usuario == null || usuario.Deletado)
            {
                return new AutenticacaoResult
                {
                    Mensagem = Dicionario.ME004,
                    Sucesso = false,
                    Errors = new[] { Dicionario.MV001 }
                };
            }

            if (!SenhaHasherHelper.SaoIguais(
                login.Senha, usuario.Senha, usuario.Salt))
            {
                return new AutenticacaoResult
                {
                    Mensagem = Dicionario.ME004,
                    Sucesso = false,
                    Errors = new[] { Dicionario.MV002 }
                };
            }

            return new AutenticacaoResult
            {
                Mensagem = Dicionario.MS001,
                Sucesso = true,
                Token = await TokenHelper.GerarTokenUsuarioAsync(usuario, _jwtOptions),
                Usuario = _mapper.Map<UsuarioModel>(usuario)
            };
        }

        public async Task<AutenticacaoResult> RegistrarUsuarioAsync(RegistrarUsuarioModel model)
        {
            var emailExiste = await _uow.UsuarioRepositorio
                .ObterUsuarioPorEmailAsync(model.Email);

            if (emailExiste != null)
            {
                return new AutenticacaoResult
                {
                    Mensagem = Dicionario.ME005,
                    Sucesso = false,
                    Errors = new[] { Dicionario.MV003 }
                };
            }

            var usuarionameExiste = await _uow.UsuarioRepositorio
                .GetAsync(u => u.Username.Contains(model.Username));

            if (usuarionameExiste.Any())
            {
                return new AutenticacaoResult
                {
                    Mensagem = Dicionario.ME005,
                    Sucesso = false,
                    Errors = new[] { Dicionario.MV004 }
                };
            }

            string salt = SenhaHasherHelper.CriarSalt(8);
            string hashedPassword = SenhaHasherHelper
                .GerarHash(model.Senha, salt);

            var novoUsuario = _mapper.Map<Usuario>(model);

            novoUsuario.Salt = salt;
            novoUsuario.Senha = hashedPassword;

            await _uow.UsuarioRepositorio
                .AddRangeAsync(new[] { novoUsuario });

            return new AutenticacaoResult
            {
                Mensagem = Dicionario.MS003,
                Sucesso = true,
                Token = await TokenHelper.GerarTokenUsuarioAsync(novoUsuario, _jwtOptions),
                Usuario = _mapper.Map<UsuarioModel>(novoUsuario)
            };
        }

        public async Task<AutenticacaoResult> ResetarSenhaAsync(ResetarSenhaModel model)
        {
            var usuario = await _uow.UsuarioRepositorio
                .ObterUsuarioPorEmailAsync(model.Email);

            if (usuario == null || usuario.Deletado)
            {
                return new AutenticacaoResult
                {
                    Mensagem = Dicionario.ME001,
                    Sucesso = false,
                };
            }

            usuario.Salt = SenhaHasherHelper.CriarSalt(8);
            usuario.Senha = SenhaHasherHelper
                .GerarHash(model.Senha, usuario.Salt);

            await _uow.UsuarioRepositorio.SaveAsync(usuario);

            return new AutenticacaoResult
            {
                Mensagem = Dicionario.MS002,
                Sucesso = true
            };
        }
    }
}
