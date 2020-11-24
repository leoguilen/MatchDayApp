using MatchDayApp.Application.Models;
using MediatR;
using System;

namespace MatchDayApp.Application.Commands.User
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public UserModel UpdateUser { get; set; }
    }
}
