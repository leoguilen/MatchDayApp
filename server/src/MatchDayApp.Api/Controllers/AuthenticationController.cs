using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Auth;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Response.Auth;
using MatchDayApp.Infra.CrossCutting.Services.Interfaces;
using MatchDayApp.Infra.CrossCutting.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MatchDayApp.Api.Controllers
{
    /// <summary>
    /// Endpoint responsible for authentication services
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    [ApiVersion("1.0")]
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
        [HttpPost(ApiRotas.Authentication.Register)]
        [ProducesResponseType(typeof(AutenticacaoComSucessoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutenticacaoComFalhaResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegistrarUsuarioRequest request)
        {
            var result = await _authService.RegisterAsync(request);

            if (!result.Success)
            {
                return BadRequest(new AutenticacaoComFalhaResponse
                {
                    Message = result.Message,
                    Errors = result.Errors
                });
            }

            return Ok(new AutenticacaoComSucessoResponse
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
        [HttpPost(ApiRotas.Authentication.Login)]
        [ProducesResponseType(typeof(AutenticacaoComSucessoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutenticacaoComFalhaResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);

            if (!result.Success)
            {
                return BadRequest(new AutenticacaoComFalhaResponse
                {
                    Message = result.Message,
                    Errors = result.Errors
                });
            }

            return Ok(new AutenticacaoComSucessoResponse
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
        [HttpPost(ApiRotas.Authentication.ResetPassword)]
        [ProducesResponseType(typeof(AutenticacaoComSucessoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutenticacaoComFalhaResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetarSenhaRequest request)
        {
            var result = await _authService.ResetPasswordAsync(request);

            if (!result.Success)
            {
                return BadRequest(new AutenticacaoComFalhaResponse
                {
                    Message = result.Message,
                    Errors = result.Errors
                });
            }

            return Ok(new AutenticacaoComSucessoResponse
            {
                Message = result.Message
            });
        }

        /// <summary>
        /// Confirm user email in the system
        /// </summary>
        /// <response code="200">Confirmed email with succefully</response>
        /// <response code="400">An error occurred when try confirm user email in the system</response>
        [HttpGet(ApiRotas.Authentication.ConfirmEmail)]
        [ProducesResponseType(typeof(AutenticacaoComSucessoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutenticacaoComFalhaResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ConfirmEmail([FromQuery] Guid key)
        {
            var result = await _authService
                .ConfirmEmailAsync(key);

            if (!result.Success)
            {
                return BadRequest(new AutenticacaoComFalhaResponse
                {
                    Message = result.Message,
                    Errors = result.Errors
                });
            }

            return Ok(new AutenticacaoComSucessoResponse
            {
                Message = result.Message
            });
        }
    }
}
