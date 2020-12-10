using Bogus;
using FluentAssertions;
using MatchDayApp.Domain.Common.Helpers;
using MatchDayApp.Domain.Entities;
using MatchDayApp.Domain.Entities.Enum;
using MatchDayApp.Domain.Repository;
using MatchDayApp.Domain.Specification.UserSpec;
using MatchDayApp.Infra.Data.Data;
using MatchDayApp.Infra.Data.Repositories;
using MatchDayApp.UnitTest.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.Ordering;

namespace MatchDayApp.UnitTest.Persistence
{
    [Trait("Repositories", "UserRepository")]
    public class UserRepositoryTest
    {
        private readonly MatchDayAppContext _memoryDb;
        private readonly IUserRepository _userRepository;

        private readonly Faker<User> _fakeUser;
        private readonly User _userTest;
        private readonly object _expectedUser;

        public UserRepositoryTest()
        {
            var configServices = ServicesConfiguration.Configure();

            _memoryDb = configServices
                .GetRequiredService<MatchDayAppContext>()
                .SeedFakeData();

            _userRepository = new UserRepository(_memoryDb);
            _userTest = _memoryDb.Users.First();

            string salt = SecurePasswordHasher.CreateSalt(8);
            _fakeUser = new Faker<User>()
                .RuleFor(u => u.FirstName, f => f.Person.FirstName)
                .RuleFor(u => u.LastName, f => f.Person.LastName)
                .RuleFor(u => u.Email, f => f.Person.Email)
                .RuleFor(u => u.PhoneNumber, f => f.Person.Phone)
                .RuleFor(u => u.Username, f => f.UniqueIndex + f.Person.UserName)
                .RuleFor(u => u.Password, f => SecurePasswordHasher.GenerateHash(f.Internet.Password(), salt))
                .RuleFor(u => u.Salt, salt)
                .RuleFor(u => u.UserType, UserType.Player);

            _expectedUser = new
            {
                FirstName = "Test",
                LastName = "One",
                Username = "test1",
                Email = "test1@email.com",
                PhoneNumber = "+551155256325",
                UserType = UserType.SoccerCourtOwner,
            };
        }

        [Fact, Order(1)]
        public void ShouldCanConnectInMemoryDatabase()
        {
            _memoryDb.Database.IsInMemory().Should().BeTrue();
            _memoryDb.Database.CanConnect().Should().BeTrue();
        }

        [Fact, Order(2)]
        public async Task ListAllAsync_User_AllUsersRegistered()
        {
            var users = await _userRepository
                .ListAllAsync();

            users.Should()
                .NotBeNull()
                .And.HaveCount(3)
                .And.SatisfyRespectively(
                    user1 =>
                    {
                        user1.Username.Should().Be("test1");
                        user1.Email.Should().Be("test1@email.com");
                        user1.UserType.Should().Be(UserType.SoccerCourtOwner);
                        user1.UserTeam.Team.Name.Should().Be("Team 1");
                    },
                    user2 =>
                    {
                        user2.Username.Should().Be("test2");
                        user2.Email.Should().Be("test2@email.com");
                        user2.UserType.Should().Be(UserType.TeamOwner);
                        user2.UserTeam.Team.Name.Should().Be("Team 2");
                    },
                    user3 =>
                    {
                        user3.Username.Should().Be("test3");
                        user3.Email.Should().Be("test3@email.com");
                        user3.UserType.Should().Be(UserType.SoccerCourtOwner);
                        user3.UserTeam.Team.Name.Should().Be("Team 3");
                    });
        }

        [Fact, Order(3)]
        public async Task GetByIdAsync_User_OneUserWithSameId()
        {
            var user = await _userRepository
                .GetByIdAsync(_userTest.Id);

            user.Should().BeEquivalentTo(_expectedUser, options =>
                options.ExcludingMissingMembers());
            user.UserTeam.Team.Name.Should().Be("Team 1");
        }

        [Fact, Order(4)]
        public async Task GetByIdAsync_User_NullWithUserIdNotRegistered()
        {
            var invalidId = Guid.NewGuid();

            var user = await _userRepository
                .GetByIdAsync(invalidId);

            user.Should().BeNull();
        }

        [Fact, Order(5)]
        public async Task GetAsync_User_GetUserWithMatchTheUserTypeSpecification()
        {
            var spec = new UserContainEmailOrUsernameSpecification("test1@email.com");
            var user = (await _userRepository.GetAsync(spec)).FirstOrDefault();

            user.Should().BeEquivalentTo(_expectedUser, options =>
                options.ExcludingMissingMembers());
        }

        [Fact, Order(6)]
        public async Task GetAsync_User_GetUserWithMatchThePredicate()
        {
            var user = (await _userRepository
                .GetAsync(u => u.Username == "test1"))
                .FirstOrDefault();

            user.Should().BeEquivalentTo(_expectedUser, options =>
                options.ExcludingMissingMembers());
        }

        [Fact, Order(7)]
        public async Task GetAsync_User_GetUsersWithMatchThePredicateAndOrdernedByDescendingByUsername()
        {
            var users = await _userRepository
                .GetAsync(null, x => x.OrderByDescending(u => u.Username), "", true);

            users.Should()
                .HaveCount(3)
                .And.SatisfyRespectively(
                user1 =>
                {
                    user1.Username.Should().Be("test3");
                    user1.Email.Should().Be("test3@email.com");
                },
                user2 =>
                {
                    user2.Username.Should().Be("test2");
                    user2.Email.Should().Be("test2@email.com");
                },
                user3 =>
                {
                    user3.Username.Should().Be("test1");
                    user3.Email.Should().Be("test1@email.com");
                });
        }

        [Fact, Order(8)]
        public async Task CountAsync_User_CountTotalUsersMatchedTheSpecification()
        {
            const int expectedTotalCount = 2;

            var spec = new UserContainUserTypeSpecification(UserType.SoccerCourtOwner);
            var usersCount = await _userRepository.CountAsync(spec);

            usersCount.Should().Be(expectedTotalCount);
        }

        [Fact, Order(9)]
        public async Task AddRangeAsync_User_AddedListUsers()
        {
            var fakeUsers = _fakeUser.Generate(5);

            fakeUsers.ForEach(u => u.UserTeam = new UserTeam
            { UserId = u.Id, TeamId = Guid.NewGuid(), Accepted = true });

            var result = await _userRepository
                .AddRangeAsync(fakeUsers);

            result.Should().NotBeNull()
                .And.HaveCount(5);
            _memoryDb.Users.Should().HaveCount(8);
            _memoryDb.UserTeams.Should().HaveCount(8);
        }

        [Fact, Order(10)]
        public async Task SaveAsync_User_UpdatedUser()
        {
            var userToUpdate = _memoryDb.Users.Last();

            userToUpdate.Email = "test.updated@email.com";
            userToUpdate.LastName = "Updated";

            var result = await _userRepository
                .SaveAsync(userToUpdate);

            result.Should().NotBeNull()
                .And.BeOfType<User>();
            result.LastName.Should().Be("Updated");
            result.Email.Should().Be("test.updated@email.com");
        }

        [Fact, Order(11)]
        public async Task DeleteAsync_User_UpdatedUser()
        {
            var userToDelete = _memoryDb.Users.Last();

            await _userRepository
                .DeleteAsync(userToDelete);

            var deletedUser = await _userRepository
                .GetByIdAsync(userToDelete.Id);

            deletedUser.Should().BeNull();
            _memoryDb.Users.Should().HaveCount(2);
        }

        [Fact, Order(12)]
        public async Task GetByEmailAsync_User_OneUserWithSameEmail()
        {
            var user = await _userRepository
                .GetByEmailAsync("test1@email.com");

            user.Should().BeEquivalentTo(_expectedUser, options =>
                options.ExcludingMissingMembers());
            user.UserTeam.Team.Name.Should().Be("Team 1");
        }

        [Fact, Order(13)]
        public async Task GetByEmailAsync_User_NullWithEmailNotRegistered()
        {
            var user = await _userRepository
                .GetByEmailAsync(new Faker().Person.Email);

            user.Should().BeNull();
        }
    }
}
