using AutoMapper;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Query;
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
    /// Endpoint responsible for manage users
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserAppService _userService;
        private readonly IUriService _uriService;
        private readonly IMapper _mapper;

        public UserController(IUserAppService userService, IMapper mapper, IUriService uriService)
        {
            _userService = userService
                ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
            _uriService = uriService
                ?? throw new ArgumentNullException(nameof(uriService));
        }

        /// <summary>
        /// Returns all users in the system
        /// </summary>
        /// <response code="200">Returns all users in the system</response>
        [HttpGet(ApiRoutes.User.GetAll)]
        [ProducesResponseType(typeof(PagedResponse<UserResponse>), 200)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery pagination)
        {
            var users = await _userService.GetUsersListAsync(pagination);
            var usersResponse = _mapper.Map<IReadOnlyList<UserResponse>>(users);

            if (pagination == null || pagination.PageNumber < 1 || pagination.PageSize < 1)
            {
                return Ok(new PagedResponse<UserResponse>(usersResponse));
            }

            var paginationResponse = PaginationHelpers
                .CreatePaginatedResponse(_uriService, pagination, usersResponse.ToList());

            return Ok(paginationResponse);
        }
    }
}
