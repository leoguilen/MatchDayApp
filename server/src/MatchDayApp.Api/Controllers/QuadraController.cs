using AutoMapper;
using MatchDayApp.Api.Extensions;
using MatchDayApp.Infra.CrossCutting.Contratos.V1;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Quadra;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Query;
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
    /// Controller responsável pela gestão das quadras
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class QuadraController : ControllerBase
    {
        private readonly IQuadraAppServico _quadraServico;
        private readonly IUriServico _uriServico;
        private readonly IMapper _mapper;

        public QuadraController(IQuadraAppServico quadraServico, IMapper mapper, IUriServico uriServico)
        {
            _quadraServico = quadraServico
                ?? throw new ArgumentNullException(nameof(quadraServico));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
            _uriServico = uriServico
                ?? throw new ArgumentNullException(nameof(uriServico));
        }

        /// <summary>
        /// Retornar todas as quadras do sistema
        /// </summary>
        /// <param name="pagination">Opções de paginação</param>
        /// <response code="200">Retornar todas as quadras do sistema</response>
        [HttpGet(ApiRotas.Quadra.GetAll)]
        [ProducesResponseType(typeof(PagedResponse<QuadraResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll([FromQuery] PaginacaoQuery pagination)
        {
            var quadra = await _quadraServico
                .ObterQuadrasAsync(pagination);

            var quadraResponse = _mapper
                .Map<IReadOnlyList<QuadraResponse>>(quadra);

            if (pagination == null || pagination.NumeroPagina < 1 || pagination.QuantidadePagina < 1)
            {
                return Ok(new PagedResponse<QuadraResponse>(quadraResponse));
            }

            var paginationResponse = PaginationHelpers
                .CriarRespostaPaginada(_uriServico, pagination, quadraResponse.ToList());

            return Ok(paginationResponse);
        }

        /// <summary>
        /// Retornar quadra por id
        /// </summary>
        /// <param name="id">Identificação da quadra no sistema</param>
        /// <response code="200">Retornar quadra por id</response>
        /// <response code="404">Nenhuma quadra encontrada</response>
        [HttpGet(ApiRotas.Quadra.Get)]
        [ProducesResponseType(typeof(Response<QuadraResponse>), 200)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var quadra = await _quadraServico
                .ObterQuadraPorIdAsync(id);

            if (quadra is null)
                return NotFound();

            return Ok(new Response<QuadraResponse>(_mapper
                .Map<QuadraResponse>(quadra)));
        }

        /// <summary>
        /// Criar nova quadra no sistema
        /// </summary>
        /// <response code="201">Nova quadra criada</response>
        /// <response code="400">Ocorreu um erro ao criar quadra</response>
        [HttpPost(ApiRotas.Quadra.Create)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CriarQuadraRequest request)
        {
            request.UsuarioProprietarioId = Guid
                .Parse(HttpContext.ObterUsuarioId());

            var novaQuadra = await _quadraServico
                .AdicionarQuadraAsync(request);

            if (novaQuadra is null)
            {
                return BadRequest(new Response<object>
                {
                    Mensagem = "Ocorreu um erro ao adicionar quadra",
                    Sucesso = false
                });
            }

            return Created(_uriServico.GetQuadraUri(novaQuadra.Id.ToString()),
                new Response<object>
                {
                    Mensagem = "Quadra adicionada com sucesso",
                    Sucesso = true,
                    Dados = novaQuadra
                });
        }

        /// <summary>
        /// Atualizar quadra
        /// </summary>
        /// <response code="200">Quadra atualizada</response>
        /// <response code="400">Ocorreu um erro ao atualizar quadra</response>
        [HttpPut(ApiRotas.Quadra.Update)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] AtualizarQuadraRequest request)
        {
            var quadraAtualizada = await _quadraServico
                .AtualizarQuadraAsync(id, request);

            if (quadraAtualizada is null)
            {
                return BadRequest(new Response<object>
                {
                    Mensagem = "Ocorreu um erro ao atualizar quadra",
                    Sucesso = false
                });
            }

            return Ok(new Response<object>
            {
                Mensagem = "Quadra atualizada com sucesso",
                Sucesso = true,
                Dados = quadraAtualizada
            });
        }

        /// <summary>
        /// Deletar quadra
        /// </summary>
        /// <response code="204">Quadra deletada</response>
        /// <response code="404">Nenhuma quadra encontrada</response>
        [HttpDelete(ApiRotas.Quadra.Delete)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var quadraDeletada = await _quadraServico
                .DeletarQuadraAsync(id);

            if (!quadraDeletada)
            {
                return BadRequest(new Response<object>
                {
                    Mensagem = "Ocorreu um erro ao deletar quadra",
                    Sucesso = false
                });
            }

            return NoContent();
        }
    }
}
