﻿using AutoMapper;
using MatchDayApp.Api.Controllers.Base;
using MatchDayApp.Infra.CrossCutting.Contratos.V1;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Query;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Usuario;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Respostas;
using MatchDayApp.Infra.CrossCutting.Helpers;
using MatchDayApp.Infra.CrossCutting.Servicos.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MatchDayApp.Api.Controllers
{
    /// <summary>
    /// Controller responsável pela gestão dos usuário no sistema
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioAppServico _usuarioServico;

        public UsuarioController(IUsuarioAppServico usuarioServico,
            ICacheServico cacheServico, IUriServico uriServico, IMapper mapper)
            : base(cacheServico, uriServico, mapper)
        {
            _usuarioServico = usuarioServico
                ?? throw new ArgumentNullException(nameof(usuarioServico));
        }

        /// <summary>
        /// Retornar todos os usuário do sistema
        /// </summary>
        /// <param name="pagination">Opções de paginação</param>
        /// <response code="200">Retornar todos os usuário do sistema</response>
        [HttpGet(ApiRotas.Usuario.GetAll)]
        [ProducesResponseType(typeof(PagedResponse<UsuarioResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll([FromQuery] PaginacaoQuery pagination, [FromServices] IUriServico uriServico)
        {
            var usuariosResponse = CacheServico
                .GetCachedResponse<IReadOnlyList<UsuarioResponse>>(
                    ApiRotas.Usuario.GetAll);

            if(usuariosResponse is null)
            {
                var usuarios = await _usuarioServico
                    .ObterUsuariosAsync(pagination);
                usuariosResponse = Mapper
                    .Map<IReadOnlyList<UsuarioResponse>>(usuarios);

                CacheServico.SetCacheResponse(
                    ApiRotas.Usuario.GetAll,
                    usuariosResponse,
                    TimeSpan.FromMinutes(2));
            }

            if (pagination == null || pagination.NumeroPagina < 1 || pagination.QuantidadePagina < 1)
            {
                return Ok(new PagedResponse<UsuarioResponse>(usuariosResponse));
            }

            var paginationResponse = PaginationHelpers
                .CriarRespostaPaginada(uriServico, pagination, usuariosResponse.ToList());

            return Ok(paginationResponse);
        }

        /// <summary>
        /// Retornar usuário por id
        /// </summary>
        /// <param name="id">Identificação do usuário no sistema</param>
        /// <response code="200">Retornar usuário por id</response>
        /// <response code="404">Nenhum usuário encontrado</response>
        [HttpGet(ApiRotas.Usuario.Get)]
        [ProducesResponseType(typeof(Response<UsuarioResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var usuarioResponse = CacheServico
                .GetCachedResponse<Response<UsuarioResponse>>(
                    ApiRotas.Usuario.Get.Replace("{id}", id.ToString()));

            if(usuarioResponse is null)
            {
                var usuario = await _usuarioServico
                    .ObterUsuarioPorIdAsync(id);

                if (usuario is null)
                    return NotFound();

                usuarioResponse = new Response<UsuarioResponse>(Mapper
                    .Map<UsuarioResponse>(usuario));

                CacheServico.SetCacheResponse(
                    ApiRotas.Usuario.Get.Replace("{id}", id.ToString()),
                    usuarioResponse, TimeSpan.FromMinutes(2));
            }

            return Ok(usuarioResponse);
        }

        /// <summary>
        /// Atualizar usuário
        /// </summary>
        /// <response code="200">Usuário atualizado</response>
        /// <response code="400">Ocorreu um erro ao atualizar usuário</response>
        [HttpPut(ApiRotas.Usuario.Update)]
        [ProducesResponseType(typeof(Response<UsuarioResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] AtualizarUsuarioRequest request)
        {
            var usuarioAtualizado = await _usuarioServico
                .AtualizarUsuarioAsync(id, request);

            if (usuarioAtualizado is null)
                return BadRequest(new Response<object>
                {
                    Mensagem = "Ocorreu um erro ao atualizar usuário",
                    Sucesso = false
                });

            return Ok(new Response<object>
            {
                Mensagem = "Usuário atualizado com sucesso",
                Sucesso = true,
                Dados = usuarioAtualizado
            });
        }

        /// <summary>
        /// Deletar usuário
        /// </summary>
        /// <response code="200">Usuário deletado do sistema (lógicamente)</response>
        /// <response code="404">Nenhum usuário encontrado</response>
        [HttpDelete(ApiRotas.Usuario.Delete)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var usuarioDeletado = await _usuarioServico
                .DeletarUsuarioAsync(id);

            if (!usuarioDeletado)
                return NotFound();

            return NoContent();
        }
    }
}
