using Bogus;
using ExpectedObjects;
using FluentAssertions;
using MatchDayApp.Domain.Entities;
using System;
using Xunit;

namespace MatchDayApp.Test.Domain.Entities
{
    public class TeamTest
    {
        private readonly Faker _faker;

        public TeamTest()
        {
            _faker = new Faker("pt_BR");
        }

        [Fact]
        [Trait("Entity", "Team")]
        public void Should_Be_Created_New_Entity_Team()
        {
            var expectedTeam = new
            {
                Name = _faker.Company.CompanyName(),
                Image = _faker.Image.PicsumUrl(),
                TotalPlayers = 0,
                OwnerUserId = _faker.Random.Guid()
            };

            var newTeam = new Team(expectedTeam.Name,
                expectedTeam.Image, expectedTeam.OwnerUserId);

            expectedTeam
                .ToExpectedObject()
                .ShouldMatch(newTeam);
            newTeam.Id
                .Should()
                .NotBeEmpty();
            newTeam.CreatedAt
                .Should()
                .BeSameDateAs(DateTime.Now);
        }
    }
}
