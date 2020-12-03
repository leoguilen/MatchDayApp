using AutoMapper;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Query;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.User;
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
        /// <param name="pagination">Pagination options</param>
        /// <response code="200">Returns all users in the system</response>
        [HttpGet(ApiRoutes.User.GetAll)]
        [ProducesResponseType(typeof(PagedResponse<UserResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery pagination)
        {
            var users = await _userService
                .GetUsersListAsync(pagination);
            var usersResponse = _mapper
                .Map<IReadOnlyList<UserResponse>>(users);

            if (pagination == null || pagination.PageNumber < 1 || pagination.PageSize < 1)
            {
                return Ok(new PagedResponse<UserResponse>(usersResponse));
            }

            var paginationResponse = PaginationHelpers
                .CreatePaginatedResponse(_uriService, pagination, usersResponse.ToList());

            return Ok(paginationResponse);
        }

        /// <summary>
        /// Return user by id
        /// </summary>
        /// <param name="userId">User identification in the system</param>
        /// <response code="200">Return user by id</response>
        /// <response code="404">Not found user</response>
        [HttpGet(ApiRoutes.User.Get)]
        [ProducesResponseType(typeof(Response<UserResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var user = await _userService
                .GetUserByIdAsync(id);

            if (user is null)
                return NotFound();

            return Ok(new Response<UserResponse>(_mapper
                .Map<UserResponse>(user)));
        }

        /// <summary>
        /// Update user informations 
        /// </summary>
        /// <response code="200">Update user informations</response>
        /// <response code="404">Not found user</response>
        /// <response code="400">An error occurred when try update user</response>
        [HttpPut(ApiRoutes.User.Update)]
        [ProducesResponseType(typeof(Response<UserResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromRoute] Guid userId, [FromBody] UpdateUserRequest request)
        {
            var result = await _userService
                .UpdateUserAsync(userId, request);

            if (!result)
                return BadRequest(new Response<object>
                {
                    Message = "Ocorreu um erro ao atualizar usuário",
                    Succeeded = false
                });

            return Ok(new Response<object>
            {
                Message = "Usuário atualizado com sucesso",
                Succeeded = true
            });
        }

        /// <summary>
        /// Delete user in the system
        /// </summary>
        /// <response code="200">Delete user in the system</response>
        /// <response code="404">Not found user</response>
        [HttpDelete(ApiRoutes.User.Delete)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid userId)
        {
            var result = await _userService
                .DeleteUserAsync(userId);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
