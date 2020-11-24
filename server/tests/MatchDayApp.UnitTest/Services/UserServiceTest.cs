using AutoMapper;
using Bogus;
using FluentAssertions;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Services;
using MatchDayApp.Infra.Data.Data;
using MatchDayApp.UnitTest.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MatchDayApp.UnitTest.Services
{
    [Trait("Services", "User")]
    public class UserServiceTest
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserService _userService;
        private readonly MatchDayAppContext _memoryDb;

        public UserServiceTest()
        {
            var cfg = ServicesConfiguration.Configure();

            _memoryDb = cfg.GetRequiredService<MatchDayAppContext>()
                .SeedFakeData();
            _uow = cfg.GetRequiredService<IUnitOfWork>();

            _userService = new UserService(_uow,
                cfg.GetRequiredService<IMapper>());
        }


        [Fact]
        public async Task DeleteUserAsync_UserService_DeletedUserInSystem()
        {
            var userId = _memoryDb.Users.Last().Id;

            var cmdResult = await _userService
                .DeleteUserAsync(userId);

            cmdResult.Should().BeTrue();

            var deletedUser = await _uow.Users
                .GetByIdAsync(userId);

            deletedUser.Deleted.Should().BeTrue();
        }

        [Fact]
        public async Task GetUserByEmailAsync_UserService_GetUserIfSameEmail()
        {
            var userEmail = "test2@email.com";

            var user = await _userService
                .GetUserByEmailAsync(userEmail);

            user.Email.Should().Be(userEmail);
        }

        [Fact]
        public async Task GetUserByIdAsync_UserService_GetUserIfSameId()
        {
            var userId = _memoryDb.Users.Last().Id;

            var user = await _userService
                .GetUserByIdAsync(userId);

            user.Email.Should().Be("test3@email.com");
        }

        [Fact]
        public async Task GetUsersListAsync_UserService_ListAllUsers()
        {
            var users = await _userService
                .GetUsersListAsync();

            users.Should().HaveCount(3);
        }

        [Fact]
        public async Task UpdateUserAsync_UserService_UpdatedUserInSystem()
        {
            var newFirstname = new Faker().Person.FirstName;
            var newLastname = new Faker().Person.LastName;
            var userId = _memoryDb.Users.ToList()[2].Id;

            var user = await _userService
                .GetUserByIdAsync(userId);
            user.FirstName = newFirstname;
            user.LastName = newLastname;

            var cmdResult = await _userService
                .UpdateUserAsync(userId, user);

            cmdResult.Should().BeTrue();

            var updatedUser = await _userService
                .GetUserByIdAsync(userId);

            updatedUser.FirstName.Should().Be(newFirstname);
            updatedUser.LastName.Should().Be(newLastname);
        }
    }
}
