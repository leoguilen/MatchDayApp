﻿using MatchDayApp.Application.Models;
using MatchDayApp.Application.Models.Auth;
using MediatR;

namespace MatchDayApp.Application.Commands.Auth
{
    public class ResetPasswordCommand : IRequest<AuthenticationResult>
    {
        public ResetarSenhaModel ResetPassword { get; set; }
    }
}
