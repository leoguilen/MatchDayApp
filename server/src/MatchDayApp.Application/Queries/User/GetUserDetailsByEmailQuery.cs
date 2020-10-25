using MatchDayApp.Application.Models;
using MediatR;

namespace MatchDayApp.Application.Queries.User
{
    public class GetUserDetailsByEmailQuery : IRequest<UserModel>
    {
        public string Email { get; set; }
    }
}
