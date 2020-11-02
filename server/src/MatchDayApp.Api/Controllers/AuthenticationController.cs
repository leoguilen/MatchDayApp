using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Auth;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Response.Auth;
using MatchDayApp.Infra.CrossCutting.Services.Interfaces;
using MatchDayApp.Infra.CrossCutting.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MatchDayApp.Api.Controllers
{
    /// <summary>
    /// Endpoint responsible for authentication services
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [AllowAnonymous]
    [Produces("application/json")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthAppService _authService;

        public AuthenticationController(IAuthAppService authService)
        {
            _authService = authService
                ?? throw new ArgumentNullException(nameof(authService));
        }

        /// <summary>
        /// Register a new user in the system
        /// </summary>
        /// <response code="200">Register a new user in the system</response>
        /// <response code="400">An error occurred when try register a new user in the system</response>
        [HttpPost(ApiRoutes.Authentication.Register)]
        [ProducesResponseType(typeof(AuthSuccessResponse), 200)]
        [ProducesResponseType(typeof(AuthFailedResponse), 400)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);

            if (!result.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Message = result.Message,
                    Errors = result.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Message = result.Message,
                Token = result.Token,
                User = result.User
            });
        }

        /// <summary>
        /// Login user in the system
        /// </summary>
        /// <response code="200">user login validated in the system</response>
        /// <response code="400">An error occurred when try login user in the system</response>
        [HttpPost(ApiRoutes.Authentication.Login)]
        [ProducesResponseType(typeof(AuthSuccessResponse), 200)]
        [ProducesResponseType(typeof(AuthFailedResponse), 400)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);

            if (!result.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Message = result.Message,
                    Errors = result.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Message = result.Message,
                Token = result.Token,
                User = result.User
            });
        }

        /// <summary>
        /// Reset user password in the system 
        /// </summary>
        /// <response code="200">User password reseted succefully in the system</response>
        /// <response code="400">An error occurred when try reset user password in the system</response>
        [HttpPost(ApiRoutes.Authentication.ResetPassword)]
        [ProducesResponseType(typeof(AuthSuccessResponse), 200)]
        [ProducesResponseType(typeof(AuthFailedResponse), 400)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var result = await _authService.ResetPasswordAsync(request);

            if (!result.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Message = result.Message,
                    Errors = result.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Message = result.Message
            });
        }
    }
}
