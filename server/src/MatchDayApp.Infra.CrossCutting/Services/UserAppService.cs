using MatchDayApp.Application.Commands.User;
using MatchDayApp.Application.Events.UserEvents;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Queries.User;
using MatchDayApp.Infra.CrossCutting.Services.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.CrossCutting.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IMediator _mediator;

        public UserAppService(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var deleteUserCommand = new DeleteUserCommand
            {
                Id = userId
            };

            var result = await _mediator.Send(deleteUserCommand);

            if (!result)
                return result;

            await _mediator.Publish(new UserDeletedEvent { Id = userId });

            return result;
        }

        public async Task<UserModel> GetUserByEmailAsync(string userEmail)
        {
            var getUserByEmailQuery = new GetUserDetailsByEmailQuery
            {
                Email = userEmail
            };

            var user = await _mediator.Send(getUserByEmailQuery);

            return user;
        }

        public async Task<UserModel> GetUserByIdAsync(Guid userId)
        {
            var getUserByIdQuery = new GetUserDetailsByIdQuery
            {
                Id = userId
            };

            var user = await _mediator.Send(getUserByIdQuery);

            return user;
        }

        public async Task<IReadOnlyList<UserModel>> GetUsersListAsync()
        {
            var getUsersQuery = new GetUsersQuery { };

            var users = await _mediator.Send(getUsersQuery);

            return users;
        }

        public async Task<bool> UpdateUserAsync(UserModel user)
        {
            var updateUserCommand = new UpdateUserCommand
            {
                UpdateUser = user
            };

            var result = await _mediator.Send(updateUserCommand);

            return result;
        }
    }
}
