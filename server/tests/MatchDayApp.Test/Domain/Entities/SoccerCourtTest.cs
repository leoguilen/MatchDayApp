using Bogus;
using ExpectedObjects;
using FluentAssertions;
using MatchDayApp.Domain.Entities;
using System;
using Xunit;

namespace MatchDayApp.Test.Domain.Entities
{
    public class SoccerCourtTest
    {
        private readonly Faker _faker;

        public SoccerCourtTest()
        {
            _faker = new Faker("pt_BR");
        }

        [Fact]
        [Trait("Entity", "SoccerCourt")]
        public void Should_Be_Created_New_Entity_SoccerCourt()
        {
            var expectedSoccerCourt = new
            {
                Name = _faker.Company.CompanyName(),
                Image = _faker.Internet.Avatar(),
                HourPrice = _faker.Random.Decimal(50.0M, 150.0M),
                Phone = _faker.Phone.PhoneNumber("(##) ####-####"),
                Address = _faker.Address.StreetAddress(true),
                Cep = _faker.Address.ZipCode("#####-###"),
                Latitude = _faker.Address.Latitude(),
                Longitude = _faker.Address.Longitude(),
                OwnerUserId = _faker.Random.Guid()
            };

            var newSoccerCourt = new SoccerCourt(
                expectedSoccerCourt.Name, expectedSoccerCourt.Image,
                expectedSoccerCourt.HourPrice, expectedSoccerCourt.Phone,
                expectedSoccerCourt.Address, expectedSoccerCourt.Cep,
                expectedSoccerCourt.Latitude, expectedSoccerCourt.Longitude,
                expectedSoccerCourt.OwnerUserId);

            expectedSoccerCourt
                .ToExpectedObject()
                .ShouldMatch(newSoccerCourt);
            newSoccerCourt.Id
                .Should()
                .NotBeEmpty();
            newSoccerCourt.CreatedAt
                .Should()
                .BeSameDateAs(DateTime.Now);
        }
    }
}
