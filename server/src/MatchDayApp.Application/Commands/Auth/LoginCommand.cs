using MatchDayApp.Application.Models;
using MatchDayApp.Application.Models.Auth;
using MediatR;

namespace MatchDayApp.Application.Commands.Auth
{
    public class LoginCommand : IRequest<AuthenticationResult>
    {
        public LoginModel Login { get; set; }
    }
}
