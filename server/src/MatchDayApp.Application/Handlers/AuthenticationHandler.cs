using MatchDayApp.Application.Commands.Auth;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Handlers
{
    public class AuthenticationHandler :
        IRequestHandler<LoginCommand, AuthenticationResult>,
        IRequestHandler<RegisterCommand, AuthenticationResult>,
        IRequestHandler<ResetPasswordCommand, AuthenticationResult>,
        IRequestHandler<ConfirmEmailCommand, AuthenticationResult>
    {
        private readonly IAuthService _authService;

        public AuthenticationHandler(IAuthService authService)
        {
            _authService = authService ?? throw new System.ArgumentNullException(nameof(authService));
        }

        public async Task<AuthenticationResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await _authService.LoginAsync(request.Login);
        }

        public async Task<AuthenticationResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await _authService.RegisterAsync(request.Register);
        }

        public async Task<AuthenticationResult> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            return await _authService.ResetPasswordAsync(request.ResetPassword);
        }

        public async Task<AuthenticationResult> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            return await _authService.ConfirmEmailAsync(request.ConfirmEmail);
        }
    }
}
