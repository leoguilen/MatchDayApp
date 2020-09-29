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
        [Fact]
        public void Should_Be_Created_New_Entity_User()
        {
            var faker = new Faker("pt_BR");

            var expectedUser = new
            {
                FirstName = faker.Name.FirstName(),
                LastName = faker.Name.LastName(),
                Username = faker.Internet.UserName(),
                Email = faker.Internet.Email(),
                ConfirmedEmail = false,
                Password = faker.Internet.Password(),
                Avatar = faker.Internet.Avatar(),
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
