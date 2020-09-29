using Bogus;
using ExpectedObjects;
using FluentAssertions;
using MatchDayApp.Domain.Entities;
using System;
using Xunit;

namespace MatchDayApp.Test.Domain.Entities
{
    public class PlayerTest
    {
        private readonly Faker _faker;

        public PlayerTest()
        {
            _faker = new Faker("pt_BR");
        }

        [Fact]
        [Trait("Entity", "Player")]
        public void Should_Be_Created_New_Entity_User()
        {
            var expectedPlayer = new
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

            var newPlayer = new User(
                    expectedPlayer.FirstName, expectedPlayer.LastName,
                    expectedPlayer.Username, expectedPlayer.Email,
                    expectedPlayer.Password, expectedPlayer.Avatar);

            expectedPlayer
                .ToExpectedObject()
                .ShouldMatch(newPlayer);
            newPlayer.Id
                .Should()
                .NotBeEmpty();
            newPlayer.CreatedAt
                .Should()
                .BeSameDateAs(DateTime.Now);
        }
    }
}
