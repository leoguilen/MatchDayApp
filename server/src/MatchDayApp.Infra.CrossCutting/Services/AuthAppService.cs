using MatchDayApp.Application.Commands.Auth;
using MatchDayApp.Application.Events.UserEvents;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Models.Auth;
using MatchDayApp.Infra.CrossCutting.Services.Interfaces;
using MediatR;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.CrossCutting.Services
{
    public class AuthAppService : IAuthAppService
    {
        private readonly IMediator _mediator;

        public AuthAppService(IMediator mediator)
        {
            _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));
        }

        public async Task<AuthenticationResult> LoginAsync(LoginModel login)
        {
            var loginCommand = new LoginCommand
            {
                Login = login
            };

            var authResult = await _mediator.Send(loginCommand);

            return authResult;
        }

        public async Task<AuthenticationResult> RegisterAsync(RegisterModel register)
        {
            var registerCommand = new RegisterCommand
            {
                Register = register
            };

            var authResult = await _mediator.Send(registerCommand);

            if (!authResult.Success)
                return authResult;

            await _mediator.Publish(new UserRegisteredEvent
            {
                Name = $"{register.FirstName} {register.LastName}",
                Email = register.Email
            });

            return authResult;
        }

        public async Task<AuthenticationResult> ResetPasswordAsync(ResetPasswordModel resetPassword)
        {
            var resetPassCommand = new ResetPasswordCommand
            {
                ResetPassword = resetPassword
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
