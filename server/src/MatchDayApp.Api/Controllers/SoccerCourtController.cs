using AutoMapper;
using MatchDayApp.Api.Extensions;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Query;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.SoccerCourt;
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
    /// Endpoint responsible for manage soccer courts
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class SoccerCourtController : ControllerBase
    {
        private readonly ISoccerCourtAppService _scService;
        private readonly IUriService _uriService;
        private readonly IMapper _mapper;

        public SoccerCourtController(ISoccerCourtAppService scService, IMapper mapper, IUriService uriService)
        {
            _scService = scService
                ?? throw new ArgumentNullException(nameof(scService));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
            _uriService = uriService
                ?? throw new ArgumentNullException(nameof(uriService));
        }

        /// <summary>
        /// Returns all soccer courts in the system
        /// </summary>
        /// <param name="pagination">Pagination options</param>
        /// <response code="200">Returns all soccer courts in the system</response>
        [HttpGet(ApiRotas.SoccerCourt.GetAll)]
        [ProducesResponseType(typeof(PagedResponse<QuadraResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery pagination)
        {
            var sc = await _scService
                .GetSoccerCourtsListAsync(pagination);

            var scResponse = _mapper
                .Map<IReadOnlyList<QuadraResponse>>(sc);

            if (pagination == null || pagination.PageNumber < 1 || pagination.PageSize < 1)
            {
                return Ok(new PagedResponse<QuadraResponse>(scResponse));
            }

            var paginationResponse = PaginationHelpers
                .CreatePaginatedResponse(_uriService, pagination, scResponse.ToList());

            return Ok(paginationResponse);
        }

        /// <summary>
        /// Return soccer court by id
        /// </summary>
        /// <param name="scId">Soccer court identification in the system</param>
        /// <response code="200">Return soccer court by id</response>
        /// <response code="404">Not found soccer court</response>
        [HttpGet(ApiRotas.SoccerCourt.Get)]
        [ProducesResponseType(typeof(Response<QuadraResponse>), 200)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var sc = await _scService
                .GetSoccerCourtByIdAsync(id);

            if (sc is null)
                return NotFound();

            return Ok(new Response<QuadraResponse>(_mapper
                .Map<QuadraResponse>(sc)));
        }

        /// <summary>
        /// Create soccer court
        /// </summary>
        /// <response code="201">Created soccer court</response>
        /// <response code="400">An error when create soccer court</response>
        [HttpPost(ApiRotas.SoccerCourt.Create)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CriarQuadraRequest request)
        {
            request.OwnerUserId = Guid
                .Parse(HttpContext.GetUserId());

            var result = await _scService
                .AddSoccerCourtAsync(request);

            if (!result)
            {
                return BadRequest(new Response<object>
                {
                    Message = "Ocorreu um erro ao adicionar quadra",
                    Succeeded = false
                });
            }

            return Created(string.Empty, new Response<object>
            {
                Message = "Quadra adicionada com sucesso",
                Succeeded = true
            });
        }

        /// <summary>
        /// Update soccer court
        /// </summary>
        /// <response code="200">Updated soccer court</response>
        /// <response code="400">An error when update soccer court</response>
        /// <response code="404">Not found soccer court</response>
        [HttpPut(ApiRotas.SoccerCourt.Update)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] AtualizarQuadraRequest request)
        {
            var result = await _scService
                .UpdateSoccerCourtAsync(id, request);

            if (!result)
            {
                return BadRequest(new Response<object>
                {
                    Message = "Ocorreu um erro ao atualizar quadra",
                    Succeeded = false
                });
            }

            return Ok(new Response<object>
            {
                Message = "Quadra atualizado com sucesso",
                Succeeded = true
            });
        }

        /// <summary>
        /// Delete soccer court
        /// </summary>
        /// <response code="204">Deleted soccer court</response>
        /// <response code="404">Not found soccer court</response>
        [HttpDelete(ApiRotas.SoccerCourt.Delete)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _scService
                .DeleteSoccerCourtAsync(id);

            if (!result)
            {
                return BadRequest(new Response<object>
                {
                    Message = "Ocorreu um erro ao deletar quadra",
                    Succeeded = false
                });
            }

            return NoContent();
        }
    }
}
