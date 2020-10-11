﻿using MatchDayApp.Contract.Contract.V1.Request.Auth;
using MediatR;

namespace MatchDayApp.Application.Commands.Auth
{
    public class RegisterCommand : IRequest<RegisterRequest>
    {
    }
}
