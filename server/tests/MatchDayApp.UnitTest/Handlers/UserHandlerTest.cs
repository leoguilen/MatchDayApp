using Bogus;
using FluentAssertions;
using MatchDayApp.Application.Commands.User;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Queries.User;
using MatchDayApp.Domain.Entities.Enum;
using MatchDayApp.Infra.Data.Data;
using MatchDayApp.UnitTest.Configuration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MatchDayApp.UnitTest.Handlers
{
    [Trait("Handler", "User")]
    public class UserHandlerTest
    {
        private readonly IMediator _mediator;
        private readonly MatchDayAppContext _memoryDb;
        private readonly Guid _userId;

        public UserHandlerTest()
        {
            var cfg = ServicesConfiguration.Configure();

            _memoryDb = cfg.GetRequiredService<MatchDayAppContext>()
                .SeedFakeData();

            _mediator = cfg.GetRequiredService<IMediator>();
            _userId = _memoryDb.Users.Last().Id;
        }

        [Fact]
        public async Task Handle_UserHandler_GetAllUsers()
        {
            var getUsersQuery = new GetUsersQuery { };

            var usersResult = await _mediator.Send(getUsersQuery);

            usersResult.Should().HaveCount(3);
        }

        [Fact]
        public async Task Handle_UserHandler_GetUserById()
        {
            var getUserByIdQuery = new GetUserDetailsByIdQuery
            {
                Id = _userId
            };

            var userResult = await _mediator.Send(getUserByIdQuery);

            userResult.Email.Should().Be("test3@email.com");
        }

        [Fact]
        public async Task Handle_UserHandler_GetUserByEmail()
        {
            var getUserByEmailQuery = new GetUserDetailsByEmailQuery
            {
                Email = "test2@email.com"
            };

            var userResult = await _mediator.Send(getUserByEmailQuery);

            userResult.Email.Should().Be("test2@email.com");
        }

        [Fact]
        public async Task Handle_UserHandler_UpdatedUser()
        {
            var updateUserCommand = new UpdateUserCommand
            {
                UpdateUser = new UserModel
                {
                    FirstName = new Faker().Person.FirstName,
                    LastName = new Faker().Person.LastName,
                    Email = "test3@email.com",
                    UserType = UserType.Player
                }
            };

            var cmdResult = await _mediator.Send(updateUserCommand);

            cmdResult.Should().BeTrue();

            var updatedUser = _memoryDb.Users.Last();

            updatedUser.FirstName.Should().Be(updateUserCommand.UpdateUser.FirstName);
            updatedUser.LastName.Should().Be(updateUserCommand.UpdateUser.LastName);
            updatedUser.UserType.Should().Be(updateUserCommand.UpdateUser.UserType);
        }

        [Fact]
        public async Task Handle_UserHandler_DeletedUser()
        {
            var deleteUserCommand = new DeleteUserCommand
            {
                Id = _userId
            };

            var cmdResult = await _mediator.Send(deleteUserCommand);

            cmdResult.Should().BeTrue();
        }
    }
}
