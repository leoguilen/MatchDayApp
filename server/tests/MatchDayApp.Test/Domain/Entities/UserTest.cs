using Bogus;
using ExpectedObjects;
using FluentAssertions;
using MatchDayApp.Domain.Entities;
using System;
using Xunit;

namespace MatchDayApp.Test.Domain.Entities
{
    public class UserTest
    {
        private readonly Faker _faker;

        public UserTest()
        {
            _faker = new Faker("pt_BR");
        }

        [Fact]
        [Trait("Entity", "User")]
        public void Should_Be_Created_New_Entity_User()
        {
            var expectedUser = new
            {
                FirstName = _faker.Name.FirstName(),
                LastName = _faker.Name.LastName(),
                Username = _faker.Internet.UserName(),
                Email = _faker.Internet.Email(),
                ConfirmedEmail = false,
                Password = _faker.Internet.Password(),
                Avatar = _faker.Internet.Avatar(),
                Deleted = false
            };

            var newUser = new User(
                    expectedUser.FirstName, expectedUser.LastName,
                    expectedUser.Username, expectedUser.Email,
                    expectedUser.Password, expectedUser.Avatar);

            expectedUser
                .ToExpectedObject()
                .ShouldMatch(newUser);
            newUser.Id
                .Should()
                .NotBeEmpty();
            newUser.CreatedAt
                .Should()
                .BeSameDateAs(DateTime.Now);
        }
    }
}
