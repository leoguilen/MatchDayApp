using MatchDayApp.Contract.Contract.V1.Request.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MatchDayApp.Application.Commands.Auth
{
    class LoginCommand : IRequest<AuthRequest>
    {
    }
}
