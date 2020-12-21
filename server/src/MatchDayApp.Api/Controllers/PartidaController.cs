using AutoMapper;
using MatchDayApp.Infra.CrossCutting.Contratos.V1;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Partida;
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
    /// Controller responsável pela gestão das partidas
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class PartidaController : ControllerBase
    {
        private readonly IPartidaAppServico _partidaService;
        private readonly IUriServico _uriServico;
        private readonly IMapper _mapper;

        public PartidaController(IPartidaAppServico partidaService, IUriServico uriServico, IMapper mapper)
        {
            _partidaService = partidaService
                ?? throw new ArgumentNullException(nameof(partidaService));
            _uriServico = uriServico
                ?? throw new ArgumentNullException(nameof(uriServico));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Retornar todas as partidas do sistema
        /// </summary>
        /// <param name="pagination">Opção de paginação</param>
        /// <response code="200">Retornar todas as partidas do sistema</response>
        [HttpGet(ApiRotas.Partida.GetAll)]
        [ProducesResponseType(typeof(PagedResponse<PartidaResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll([FromQuery] PaginacaoQuery pagination, [FromQuery] PartidaFilterQuery filter)
        {
            var partidas = await _partidaService
                .ObterPartidasAsync(pagination, filter);

            var partidasResponse = _mapper
                .Map<IReadOnlyList<PartidaResponse>>(partidas);

            if (pagination == null || pagination.NumeroPagina < 1 || pagination.QuantidadePagina < 1)
                return Ok(new PagedResponse<PartidaResponse>(partidasResponse));

            var paginationResponse = PaginationHelpers
                .CriarRespostaPaginada(_uriServico, pagination, partidasResponse.ToList());

            return Ok(paginationResponse);
        }

        /// <summary>
        /// Retornar partida pelo Id
        /// </summary>
        /// <param name="id">Identificação da partida</param>
        /// <response code="200">Retornar partida pelo id</response>
        /// <response code="404">Nenhum partida encontrada</response>
        [HttpGet(ApiRotas.Partida.Get)]
        [ProducesResponseType(typeof(Response<PartidaResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var partida = await _partidaService
                .ObterPartidaPorIdAsync(id);

            if (partida is null)
                return NotFound();

            return Ok(new Response<PartidaResponse>(_mapper
                .Map<PartidaResponse>(partida)));
        }

        /// <summary>
        /// Marcar nova partida no sistema
        /// </summary>
        /// <response code="200">Marcar nova partida no sistema</response>
        /// <response code="400">Ocorreu um erro ao marcar partida</response>
        [HttpPost(ApiRotas.Partida.MarcarPartida)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> MarcarPartida([FromBody] MarcarPartidaRequest request)
        {
            var novaPartida = await _partidaService
                .MarcarPartidaAsync(request);

            if (novaPartida is null)
            {
                return BadRequest(new Response<object>
                {
                    Mensagem = "Ocorreu um erro ao marcar partida",
                    Sucesso = false
                });
            }

            return Ok(new Response<object>
            {
                Mensagem = "Solicitação para marcar partida feita com sucesso",
                Sucesso = true,
                Dados = novaPartida
            });
        }

        /// <summary>
        /// Confirmar solicitação de partida
        /// </summary>
        /// <response code="200">Confirmada partida</response>
        /// <response code="400">Ocorreu um erro ao confirmar partida</response>
        [HttpPost(ApiRotas.Partida.ConfirmarPartida)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ConfirmarPartida([FromBody] ConfirmarPartidaRequest request)
        {
            var partidaConfirmada = await _partidaService
                .ConfirmarPartidaAsync(request);

            if (partidaConfirmada is false)
            {
                return BadRequest(new Response<object>
                {
                    Mensagem = "Ocorreu um erro ao confirmar a partida",
                    Sucesso = false
                });
            }

            return Ok(new Response<object>
            {
                Mensagem = "Partida confirmada com sucesso",
                Sucesso = true
            });
        }

        /// <summary>
        /// Desmarcar partida
        /// </summary>
        /// <response code="200">Partida desmarcada</response>
        /// <response code="400">Ocorreu um erro ao desmarcar partida</response>
        [HttpPost(ApiRotas.Partida.DesmarcarPartida)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DesmarcarPartida([FromBody] DesmarcarPartidaRequest request)
        {
            var partidaDesmarcada = await _partidaService
                .DesmarcarPartidaAsync(request.PartidaId);

            if (partidaDesmarcada is null)
            {
                return BadRequest(new Response<object>
                {
                    Mensagem = "Ocorreu um erro ao desmarcar a partida",
                    Sucesso = false
                });
            }

            return Ok(new Response<object>
            {
                Mensagem = "Partida desmarcada com sucesso",
                Sucesso = true,
                Dados = partidaDesmarcada
            });
        }
    }
}
