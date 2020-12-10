using AutoMapper;
using MatchDayApp.Application.Commands.Auth;
using MatchDayApp.Application.Events.UserEvents;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Models.Auth;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Auth;
using MatchDayApp.Infra.CrossCutting.Services.Interfaces;
using MediatR;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.CrossCutting.Services
{
    public class AuthAppService : IAuthAppService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AuthAppService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }

        public async Task<AuthenticationResult> LoginAsync(LoginRequest login)
        {
            var loginCommand = new LoginCommand
            {
                Login = _mapper
                    .Map<LoginModel>(login)
            };

            var authResult = await _mediator.Send(loginCommand);

            return authResult;
        }

        public async Task<AuthenticationResult> RegisterAsync(RegisterRequest register)
        {
            var registerCommand = new RegisterCommand
            {
                Register = _mapper
                    .Map<RegisterModel>(register)
            };

            var authResult = await _mediator.Send(registerCommand);

            if (!authResult.Success)
                return authResult;

            await _mediator.Publish(new UserRegisteredEvent
            {
                Name = $"{register.FirstName} {register.LastName}",
                PhoneNumber = "+5511981411956",
                Email = register.Email
            });

            return authResult;
        }

        public async Task<AuthenticationResult> ResetPasswordAsync(ResetPasswordRequest resetPassword)
        {
            var resetPassCommand = new ResetPasswordCommand
            {
                ResetPassword = _mapper
                    .Map<ResetPasswordModel>(resetPassword)
            };

            var authResult = await _mediator.Send(resetPassCommand);

            if (!authResult.Success)
                return authResult;

            await _mediator.Publish(new UserResetPasswordEvent
            {
                Email = resetPassword.Email
            });

            return authResult;
        }
    }
}
