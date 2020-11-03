using AutoMapper;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Response;
using MatchDayApp.Infra.CrossCutting.Services.Interfaces;
using MatchDayApp.Infra.CrossCutting.V1;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        private readonly IMapper _mapper;

        public UserController(IUserAppService userService, IMapper mapper)
        {
            _userService = userService 
                ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper 
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Returns all users in the system
        /// </summary>
        /// <response code="200">Returns all users in the system</response>
        [HttpGet(ApiRoutes.User.GetAll)]
        [ProducesResponseType(typeof(PagedResponse<UserResponse>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetUsersListAsync();
            return Ok(new PagedResponse<UserResponse>(
                _mapper.Map<IReadOnlyList<UserResponse>>(users)));
        }
    }
}
