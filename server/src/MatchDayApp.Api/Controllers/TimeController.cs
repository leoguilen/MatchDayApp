using AutoMapper;
using MatchDayApp.Api.Controllers.Base;
using MatchDayApp.Api.Extensions;
using MatchDayApp.Infra.CrossCutting.Contratos.V1;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Query;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Time;
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
    /// Controller responsável pela gestão dos times
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TimeController : BaseController
    {
        private readonly ITimeAppServico _timeServico;

        public TimeController(ITimeAppServico timeServico,
            ICacheServico cacheServico, IUriServico uriServico, IMapper mapper)
            : base(cacheServico, uriServico, mapper)
        {
            _timeServico = timeServico
                ?? throw new ArgumentNullException(nameof(timeServico));
        }

        /// <summary>
        /// Retornar todos os times do sistema
        /// </summary>
        /// <param name="pagination">Opção paginação</param>
        /// <response code="200">Retornar todos os times do sistema</response>
        [HttpGet(ApiRotas.Time.GetAll)]
        [ProducesResponseType(typeof(PagedResponse<TimeResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll([FromQuery] PaginacaoQuery pagination)
        {
            var timesResponse = CacheServico
                .GetCachedResponse<IReadOnlyList<TimeResponse>>(
                    ApiRotas.Time.GetAll);

            if(timesResponse is null)
            {
                var times = await _timeServico
                    .ObterTimesAsync(pagination);
                timesResponse = Mapper
                    .Map<IReadOnlyList<TimeResponse>>(times);

                CacheServico.SetCacheResponse(
                    ApiRotas.Time.GetAll,
                    timesResponse,
                    TimeSpan.FromMinutes(2));
            }

            if (pagination == null || pagination.NumeroPagina < 1 || pagination.QuantidadePagina < 1)
            {
                return Ok(new PagedResponse<TimeResponse>(timesResponse));
            }

            var paginationResponse = PaginationHelpers
                .CriarRespostaPaginada(UriServico, pagination, timesResponse.ToList());

            return Ok(paginationResponse);
        }

        /// <summary>
        /// Retornar time por id
        /// </summary>
        /// <param name="id">Identificação do time no sistema</param>
        /// <response code="200">Retornar time por id</response>
        /// <response code="404">Nenhum time encontrado</response>
        [HttpGet(ApiRotas.Time.Get)]
        [ProducesResponseType(typeof(Response<TimeResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var timeResponse = CacheServico
                .GetCachedResponse<Response<TimeResponse>>(
                    ApiRotas.Time.Get.Replace("{id}", id.ToString()));

            if(timeResponse is null)
            {
                var time = await _timeServico
                .ObterTimePorIdAsync(id);

                if (time is null)
                    return NotFound();

                timeResponse = new Response<TimeResponse>(Mapper
                    .Map<TimeResponse>(time));

                CacheServico.SetCacheResponse(
                    ApiRotas.Time.Get.Replace("{id}", id.ToString()),
                    timeResponse, TimeSpan.FromMinutes(2));
            }

            return Ok(timeResponse);
        }

        /// <summary>
        /// Criar novo time
        /// </summary>
        /// <response code="201">Novo time criado</response>
        /// <response code="400">Ocorreu um erro ao criar time</response>
        [HttpPost(ApiRotas.Time.Create)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CriarTimeRequest request)
        {
            request.UsuarioProprietarioId = Guid
                .Parse(HttpContext.ObterUsuarioId());

            var novoTime = await _timeServico
                .AdicionarTimeAsync(request);

            if (novoTime is null)
            {
                return BadRequest(new Response<object>
                {
                    Mensagem = "Ocorreu um erro ao adicionar time",
                    Sucesso = false
                });
            }

            return Created(UriServico.GetTimeUri(novoTime.Id.ToString()),
                new Response<object>
                {
                    Mensagem = "Time adicionado com sucesso",
                    Sucesso = true,
                    Dados = novoTime
                });
        }

        /// <summary>
        /// Atualizar time
        /// </summary>
        /// <response code="200">Time atualzado</response>
        /// <response code="400">Ocorreu um erro ao atualizar time</response>
        [HttpPut(ApiRotas.Time.Update)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] AtualizarTimeRequest request)
        {
            var timeAtualizado = await _timeServico
                .AtualizarTimeAsync(id, request);

            if (timeAtualizado is null)
            {
                return BadRequest(new Response<object>
                {
                    Mensagem = "Ocorreu um erro ao atualizar time",
                    Sucesso = false
                });
            }

            return Ok(new Response<object>
            {
                Mensagem = "Time atualizado com sucesso",
                Sucesso = true,
                Dados = timeAtualizado
            });
        }

        /// <summary>
        /// Deletar time
        /// </summary>
        /// <response code="204">Time deletado</response>
        /// <response code="404">Nenhum time encontrado</response>
        [HttpDelete(ApiRotas.Time.Delete)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var timeDeletado = await _timeServico
                .DeletarTimeAsync(id);

            if (!timeDeletado)
            {
                return BadRequest(new Response<object>
                {
                    Mensagem = "Ocorreu um erro ao deletar time",
                    Sucesso = false
                });
            }

            return NoContent();
        }
    }
}
