using AutoMapper;
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
        [HttpGet(ApiRoutes.SoccerCourt.GetAll)]
        [ProducesResponseType(typeof(PagedResponse<SoccerCourtResponse>), 200)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery pagination)
        {
            var sc = await _scService
                .GetSoccerCourtsListAsync(pagination);

            var scResponse = _mapper
                .Map<IReadOnlyList<SoccerCourtResponse>>(sc);

            if (pagination == null || pagination.PageNumber < 1 || pagination.PageSize < 1)
            {
                return Ok(new PagedResponse<SoccerCourtResponse>(scResponse));
            }

            var paginationResponse = PaginationHelpers
                .CreatePaginatedResponse(_uriService, pagination, scResponse.ToList());

            return Ok(paginationResponse);
        }

        /// <summary>
        /// Return soccer court by id
        /// </summary>
        /// <param name="teamId">Soccer court identification in the system</param>
        /// <response code="200">Return soccer court by id</response>
        /// <response code="404">Not found soccer court</response>
        [HttpGet(ApiRoutes.SoccerCourt.Get)]
        [ProducesResponseType(typeof(Response<SoccerCourtResponse>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get([FromRoute] Guid scId)
        {
            var sc = await _scService
                .GetSoccerCourtByIdAsync(scId);
            
            if (sc is null)
                return NotFound();

            return Ok(new Response<SoccerCourtResponse>(_mapper
                .Map<SoccerCourtResponse>(sc)));
        }

        /// <summary>
        /// Return soccer court by geolocalization
        /// </summary>
        /// <param name="teamId">User Coordenates</param>
        /// <response code="200">Return soccer court nearby coordenates</response>
        /// <response code="404">Not found soccer court</response>
        [HttpPost(ApiRoutes.SoccerCourt.GetByGeo)]
        [ProducesResponseType(typeof(Response<List<SoccerCourtResponse>>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByGeo([FromBody] GetSoccerCourtsByGeoRequest request)
        {
            var sc = await _scService
                .GetSoccerCourtsByGeoLocalizationAsync(request);

            if (sc is null)
                return NotFound();

            return Ok(new Response<List<SoccerCourtResponse>>(_mapper
                .Map<List<SoccerCourtResponse>>(sc)));
        }
    }
}
