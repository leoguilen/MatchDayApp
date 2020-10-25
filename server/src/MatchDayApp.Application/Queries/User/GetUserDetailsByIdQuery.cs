using MatchDayApp.Application.Models;
using MediatR;
using System;

namespace MatchDayApp.Application.Queries.User
{
    public class GetUserDetailsByIdQuery : IRequest<UserModel>
    {
        public Guid Id { get; set; }
    }
}
