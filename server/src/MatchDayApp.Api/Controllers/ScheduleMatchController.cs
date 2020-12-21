using AutoMapper;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Query;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.ScheduleMatch;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Response;
using MatchDayApp.Infra.CrossCutting.Helpers;
using MatchDayApp.Infra.CrossCutting.Services.Interfaces;
using MatchDayApp.Infra.CrossCutting.V1;
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
    /// Endpoint responsible for schedule matches
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class ScheduleMatchController : ControllerBase
    {
        private readonly IScheduleMatchAppService _matchService;
        private readonly IUriService _uriService;
        private readonly IMapper _mapper;

        public ScheduleMatchController(IScheduleMatchAppService matchService, IUriService uriService, IMapper mapper)
        {
            _matchService = matchService
                ?? throw new ArgumentNullException(nameof(matchService));
            _uriService = uriService
                ?? throw new ArgumentNullException(nameof(uriService));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Returns all scheduled matches
        /// </summary>
        /// <param name="pagination">Pagination options</param>
        /// <response code="200">Returns all scheduled matches</response>
        [HttpGet(ApiRotas.ScheduleMatch.GetAll)]
        [ProducesResponseType(typeof(PagedResponse<PartidaResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery pagination, [FromQuery] PartidaFilterQuery filter)
        {
            var matches = await _matchService
                .GetScheduledMatchesListAsync(pagination, filter);

            var matchesResponse = _mapper
                .Map<IReadOnlyList<PartidaResponse>>(matches);

            if (pagination == null || pagination.PageNumber < 1 || pagination.PageSize < 1)
                return Ok(new PagedResponse<PartidaResponse>(matchesResponse));

            var paginationResponse = PaginationHelpers
                .CreatePaginatedResponse(_uriService, pagination, matchesResponse.ToList());

            return Ok(paginationResponse);
        }

        /// <summary>
        /// Return match by id
        /// </summary>
        /// <param name="matchId">Match identification in the system</param>
        /// <response code="200">Return match by id</response>
        /// <response code="404">Not found match</response>
        [HttpGet(ApiRotas.ScheduleMatch.Get)]
        [ProducesResponseType(typeof(Response<PartidaResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var match = await _matchService
                .GetScheduledMatchByIdAsync(id);

            if (match is null)
                return NotFound();

            return Ok(new Response<PartidaResponse>(_mapper
                .Map<PartidaResponse>(match)));
        }

        /// <summary>
        /// Schedule match
        /// </summary>
        /// <response code="200">Scheduled match</response>
        /// <response code="400">An error when schedule match</response>
        [HttpPost(ApiRotas.ScheduleMatch.ScheduledMatch)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ScheduleMatch([FromBody] MarcarPartidaRequest request)
        {
            var result = await _matchService
                .ScheduleMatchAsync(request);

            if (!result)
            {
                return BadRequest(new Response<object>
                {
                    Message = "Ocorreu um erro ao marcar partida",
                    Succeeded = false
                });
            }

            return Ok(new Response<object>
            {
                Message = "Solicitação para marcar partida feita com sucesso",
                Succeeded = true
            });
        }

        /// <summary>
        /// Confirm match
        /// </summary>
        /// <response code="200">Confirmed match</response>
        /// <response code="400">An error when confirm match</response>
        [HttpPost(ApiRotas.ScheduleMatch.ConfirmMatch)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ConfirmMatch([FromBody] ConfirmarPartidaRequest request)
        {
            var result = await _matchService
                .ConfirmMatchAsync(request);

            if (!result)
            {
                return BadRequest(new Response<object>
                {
                    Message = "Ocorreu um erro ao confirmar a partida",
                    Succeeded = false
                });
            }

            return Ok(new Response<object>
            {
                Message = "Partida confirmada com sucesso",
                Succeeded = true
            });
        }

        /// <summary>
        /// Uncheck match
        /// </summary>
        /// <response code="200">Unchecked match</response>
        /// <response code="400">An error when uncheck match</response>
        [HttpPost(ApiRotas.ScheduleMatch.UncheckMatch)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UncheckMatch([FromBody] DesmarcarPartidaRequest request)
        {
            var result = await _matchService
                .UncheckMatchAsync(request.MatchId);

            if (!result)
            {
                return BadRequest(new Response<object>
                {
                    Message = "Ocorreu um erro ao desmarcar a partida",
                    Succeeded = false
                });
            }

            return Ok(new Response<object>
            {
                Message = "Partida desmarcada com sucesso",
                Succeeded = true
            });
        }
    }
}
