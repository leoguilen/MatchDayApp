using MatchDayApp.Application.Models;
using MediatR;

namespace MatchDayApp.Application.Commands.User
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public UserModel UpdateUser { get; set; }
    }
}
