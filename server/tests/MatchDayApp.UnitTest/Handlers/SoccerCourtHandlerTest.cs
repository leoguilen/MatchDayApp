using Bogus;
using FluentAssertions;
using MatchDayApp.Application.Commands.SoccerCourt;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Queries.SoccerCourt;
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
    [Trait("Handler", "SoccerCourt")]
    public class SoccerCourtHandlerTest
    {
        private readonly IMediator _mediator;
        private readonly MatchDayAppContext _memoryDb;
        private readonly Guid _soccerCourtId;
        private readonly Faker _faker;

        public SoccerCourtHandlerTest()
        {
            var cfg = ServicesConfiguration.Configure();

            _memoryDb = cfg.GetRequiredService<MatchDayAppContext>()
                .SeedFakeData();

            _mediator = cfg.GetRequiredService<IMediator>();
            _soccerCourtId = _memoryDb.SoccerCourts.Last().Id;

            _faker = new Faker("pt_BR");
        }

        [Fact]
        public async Task Handle_SoccerCourtHandler_GetAllSoccerCourts()
        {
            var getSoccerCourtQuery = new GetSoccerCourtsQuery { };

            var soccerCourtsResult = await _mediator.Send(getSoccerCourtQuery);

            soccerCourtsResult.Should().HaveCount(3);
        }

        [Fact]
        public async Task Handle_SoccerCourtHandler_GetSoccerCourtById()
        {
            var getSoccerCourtByIdQuery = new GetSoccerCourtDetailsByIdQuery
            {
                Id = _soccerCourtId
            };

            var soccerCourtResult = await _mediator.Send(getSoccerCourtByIdQuery);

            soccerCourtResult.Name.Should().Be("Soccer Court 1");
        }

        [Fact]
        public async Task Handle_SoccerCourtHandler_GetSoccerCourtsByGeoLocalization()
        {
            var getSoccerCourtsByGeoLocalizationQuery = new GetSoccerCourtsByGeoLocalizationQuery
            {
                Lat = -23.1087742,
                Long = -46.5546822
            };

            var soccerCourtResult = await _mediator.Send(getSoccerCourtsByGeoLocalizationQuery);

            soccerCourtResult.Should().HaveCount(2);
        }

        [Fact]
        public async Task Handle_SoccerCourtHandler_AddedSoccerCourt()
        {
            var addSoccerCourtCommand = new AddSoccerCourtCommand
            {
                SoccerCourt = new SoccerCourtModel
                {
                    Name = _faker.Company.CompanyName(),
                    Image = _faker.Image.PicsumUrl(),
                    HourPrice = _faker.Random.Decimal(90, 130),
                    Phone = _faker.Phone.PhoneNumber("(##) ####-####"),
                    Address = _faker.Address.FullAddress(),
                    Cep = _faker.Address.ZipCode("#####-###"),
                    Latitude = _faker.Address.Latitude(),
                    Longitude = _faker.Address.Longitude(),
                    OwnerUserId = _memoryDb.Users.Last().Id
                }
            };

            var cmdResult = await _mediator.Send(addSoccerCourtCommand);

            cmdResult.Should().BeTrue();

            var insertedSoccerCourt = _memoryDb.SoccerCourts.Last();

            insertedSoccerCourt.Name.Should().Be(addSoccerCourtCommand.SoccerCourt.Name);
            insertedSoccerCourt.Image.Should().Be(addSoccerCourtCommand.SoccerCourt.Image);
            insertedSoccerCourt.HourPrice.Should().Be(addSoccerCourtCommand.SoccerCourt.HourPrice);
            insertedSoccerCourt.Phone.Should().Be(addSoccerCourtCommand.SoccerCourt.Phone);
            insertedSoccerCourt.Address.Should().Be(addSoccerCourtCommand.SoccerCourt.Address);
            insertedSoccerCourt.Cep.Should().Be(addSoccerCourtCommand.SoccerCourt.Cep);
            insertedSoccerCourt.Latitude.Should().Be(addSoccerCourtCommand.SoccerCourt.Latitude);
            insertedSoccerCourt.Longitude.Should().Be(addSoccerCourtCommand.SoccerCourt.Longitude);
            insertedSoccerCourt.OwnerUserId.Should().Be(addSoccerCourtCommand.SoccerCourt.OwnerUserId);
        }

        [Fact]
        public async Task Handle_SoccerCourtHandler_UpdatedSoccerCourt()
        {
            var updateSoccerCourtCommand = new UpdateSoccerCourtCommand
            {
                Id = _soccerCourtId,
                SoccerCourt = new SoccerCourtModel
                {
                    Name = _faker.Company.CompanyName(),
                    Image = _faker.Image.PicsumUrl()
                }
            };

            var cmdResult = await _mediator.Send(updateSoccerCourtCommand);

            cmdResult.Should().BeTrue();

            var updatedSoccerCourt = _memoryDb.SoccerCourts.Last();

            updatedSoccerCourt.Name.Should().Be(updateSoccerCourtCommand.SoccerCourt.Name);
            updatedSoccerCourt.Image.Should().Be(updateSoccerCourtCommand.SoccerCourt.Image);
        }

        [Fact]
        public async Task Handle_SoccerCourtHandler_DeletedSoccerCourt()
        {
            var deleteSoccerCourtCommand = new DeleteSoccerCourtCommand
            {
                Id = _soccerCourtId
            };

            var cmdResult = await _mediator.Send(deleteSoccerCourtCommand);

            cmdResult.Should().BeTrue();
        }
    }
}
