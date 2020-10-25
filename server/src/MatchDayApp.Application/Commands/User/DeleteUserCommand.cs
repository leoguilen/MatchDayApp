using MediatR;
using System;

namespace MatchDayApp.Application.Commands.User
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
