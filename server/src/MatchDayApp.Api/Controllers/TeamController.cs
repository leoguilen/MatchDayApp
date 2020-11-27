using AutoMapper;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Query;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Team;
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
    /// Endpoint responsible for manage teams
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamAppService _teamService;
        private readonly IUriService _uriService;
        private readonly IMapper _mapper;

        public TeamController(ITeamAppService teamService, IMapper mapper, IUriService uriService)
        {
            _teamService = teamService
                ?? throw new ArgumentNullException(nameof(teamService));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
            _uriService = uriService
                ?? throw new ArgumentNullException(nameof(uriService));
        }

        /// <summary>
        /// Returns all teams in the system
        /// </summary>
        /// <param name="pagination">Pagination options</param>
        /// <response code="200">Returns all teams in the system</response>
        [HttpGet(ApiRoutes.Team.GetAll)]
        [ProducesResponseType(typeof(PagedResponse<TeamResponse>), 200)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery pagination)
        {
            var teams = await _teamService
                .GetTeamsListAsync(pagination);

            var teamsResponse = _mapper
                .Map<IReadOnlyList<TeamResponse>>(teams);

            if (pagination == null || pagination.PageNumber < 1 || pagination.PageSize < 1)
            {
                return Ok(new PagedResponse<TeamResponse>(teamsResponse));
            }

            var paginationResponse = PaginationHelpers
                .CreatePaginatedResponse(_uriService, pagination, teamsResponse.ToList());

            return Ok(paginationResponse);
        }

        /// <summary>
        /// Return team by id
        /// </summary>
        /// <param name="teamId">Team identification in the system</param>
        /// <response code="200">Return team by id</response>
        /// <response code="404">Not found team</response>
        [HttpGet(ApiRoutes.Team.Get)]
        [ProducesResponseType(typeof(Response<TeamResponse>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get([FromRoute] Guid teamId)
        {
            var team = await _teamService
                .GetTeamByIdAsync(teamId);

            if (team is null)
                return NotFound();

            return Ok(new Response<TeamResponse>(_mapper
                .Map<TeamResponse>(team)));
        }

        /// <summary>
        /// Create team
        /// </summary>
        /// <response code="201">Created team</response>
        /// <response code="400">An error when create team</response>
        [HttpPost(ApiRoutes.Team.Create)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] CreateTeamRequest request)
        {
            var result = await _teamService
                .AddTeamAsync(request);

            if (!result)
            {
                return BadRequest(new Response<object>
                {
                    Message = "Ocorreu um erro ao adicionar usuário",
                    Succeeded = false
                });
            }

            return Ok(new Response<object>
            {
                Message = "Time adicionado com sucesso",
                Succeeded = true
            });
        }
    }
}
