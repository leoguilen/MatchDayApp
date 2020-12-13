using MatchDayApp.Application.Models;
using MatchDayApp.Application.Models.Auth;
using MediatR;
using System;

namespace MatchDayApp.Application.Commands.Auth
{
    public class ConfirmEmailCommand : IRequest<AuthenticationResult>
    {
        public ConfirmEmailModel ConfirmEmail { get; set; }
    }
}
