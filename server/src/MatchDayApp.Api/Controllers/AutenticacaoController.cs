using AutoMapper;
using MatchDayApp.Api.Controllers.Base;
using MatchDayApp.Infra.CrossCutting.Contratos.V1;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Auth;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Respostas.Auth;
using MatchDayApp.Infra.CrossCutting.Servicos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MatchDayApp.Api.Controllers
{
    /// <summary>
    /// Controller responsavel por fazer a autenticação do usuário no sistema
    /// </summary>
    [AllowAnonymous]
    public class AutenticacaoController : BaseController
    {
        private readonly IAutenticacaoAppServico _autenticacaoServico;
        
        public AutenticacaoController(IAutenticacaoAppServico autenticacaoServico, 
            ICacheServico cacheServico, IUriServico uriServico, IMapper mapper) 
            : base(cacheServico, uriServico, mapper)
        {
            _autenticacaoServico = autenticacaoServico 
                ?? throw new ArgumentNullException(nameof(autenticacaoServico));
        }

        /// <summary>
        /// Registrar novo usuário no sistema
        /// </summary>
        /// <response code="200">Registrar novo usuário no sistema</response>
        /// <response code="400">Um erro ocorreu ao registrar usuário</response>
        [HttpPost(ApiRotas.Autenticacao.RegistrarUsuario)]
        [ProducesResponseType(typeof(AutenticacaoComSucessoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutenticacaoComFalhaResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RegistrarUsuario([FromBody] RegistrarUsuarioRequest request)
        {
            var autenticacaoResult = await _autenticacaoServico
                .RegistrarUsuarioAsync(request);

            if (!autenticacaoResult.Sucesso)
            {
                return BadRequest(new AutenticacaoComFalhaResponse
                {
                    Mensagem = autenticacaoResult.Mensagem,
                    Errors = autenticacaoResult.Errors
                });
            }

            return Created(UriServico.GetUsuarioUri(
                autenticacaoResult.Usuario.Id.ToString()),
                new AutenticacaoComSucessoResponse
                {
                    Mensagem = autenticacaoResult.Mensagem,
                    Token = autenticacaoResult.Token,
                    Usuario = autenticacaoResult.Usuario
                });
        }

        /// <summary>
        /// Logar usuário no sistema
        /// </summary>
        /// <response code="200">Usuário logado no sistema</response>
        /// <response code="400">Um erro ocorreu ao tentar logar usuário no sistema</response>
        [HttpPost(ApiRotas.Autenticacao.Login)]
        [ProducesResponseType(typeof(AutenticacaoComSucessoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutenticacaoComFalhaResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var autenticacaoResult = await _autenticacaoServico
                .LoginAsync(request);

            if (!autenticacaoResult.Sucesso)
            {
                return BadRequest(new AutenticacaoComFalhaResponse
                {
                    Mensagem = autenticacaoResult.Mensagem,
                    Errors = autenticacaoResult.Errors
                });
            }

            return Ok(new AutenticacaoComSucessoResponse
            {
                Mensagem = autenticacaoResult.Mensagem,
                Token = autenticacaoResult.Token,
                Usuario = autenticacaoResult.Usuario
            });
        }

        /// <summary>
        /// Resetar senha do usuário no sistema
        /// </summary>
        /// <response code="200">Senha do usuário foi resetada no sistema</response>
        /// <response code="400">Um erro ocorreu ao tentar resetar senha do usuário no sistema</response>
        [HttpPost(ApiRotas.Autenticacao.ResetarSenha)]
        [ProducesResponseType(typeof(AutenticacaoComSucessoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutenticacaoComFalhaResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ResetarSenha([FromBody] ResetarSenhaRequest request)
        {
            var autenticacaoResult = await _autenticacaoServico
                .ResetarSenhaAsync(request);

            if (!autenticacaoResult.Sucesso)
            {
                return BadRequest(new AutenticacaoComFalhaResponse
                {
                    Mensagem = autenticacaoResult.Mensagem,
                    Errors = autenticacaoResult.Errors
                });
            }

            return Ok(new AutenticacaoComSucessoResponse
            {
                Mensagem = autenticacaoResult.Mensagem
            });
        }

        /// <summary>
        /// Confirmar email do usuário no sistema
        /// </summary>
        /// <response code="200">Email do usuário confirmado no sistema</response>
        /// <response code="400">Um erro ocorreu ao tentar confirmar email do usuário no sistema</response>
        [HttpGet(ApiRotas.Autenticacao.ConfirmarEmail)]
        [ProducesResponseType(typeof(AutenticacaoComSucessoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutenticacaoComFalhaResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ConfirmarEmail([FromQuery] Guid chave)
        {
            var autenticacaoResult = await _autenticacaoServico
                .ConfirmarEmailAsync(chave);

            if (!autenticacaoResult.Sucesso)
            {
                return BadRequest(new AutenticacaoComFalhaResponse
                {
                    Mensagem = autenticacaoResult.Mensagem,
                    Errors = autenticacaoResult.Errors
                });
            }

            return Ok(new AutenticacaoComSucessoResponse
            {
                Mensagem = autenticacaoResult.Mensagem
            });
        }
    }
}
