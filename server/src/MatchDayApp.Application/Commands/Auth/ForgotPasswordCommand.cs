using MatchDayApp.Application.Models;
using MediatR;

namespace MatchDayApp.Application.Commands.Auth
{
    public class ForgotPasswordCommand : IRequest<AuthenticationResult>
    {
        public string Email { get; set; }
    }
}
