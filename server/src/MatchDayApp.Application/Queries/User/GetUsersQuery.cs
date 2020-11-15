using MatchDayApp.Application.Models;
using MediatR;
using System.Collections.Generic;

namespace MatchDayApp.Application.Queries.User
{
    public class GetUsersQuery : IRequest<IReadOnlyList<UserModel>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
