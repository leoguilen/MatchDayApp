using AutoMapper;
using Bogus;
using FluentAssertions;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Services;
using MatchDayApp.Infra.Data.Data;
using MatchDayApp.UnitTest.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MatchDayApp.UnitTest.Services
{
    [Trait("Services", "SoccerCourt")]
    public class SoccerCourtServiceTest
    {
        private readonly IUnitOfWork _uow;
        private readonly IQuadraFutebolServico _soccerCourtService;
        private readonly MatchDayAppContext _memoryDb;

        private readonly Guid _soccerCourtId;

        public SoccerCourtServiceTest()
        {
            var cfg = ServicesConfiguration.Configure();

            _memoryDb = cfg.GetRequiredService<MatchDayAppContext>()
                .SeedFakeData();
            _uow = cfg.GetRequiredService<IUnitOfWork>();

            _soccerCourtService = new QuadraFutebolServico(_uow,
                cfg.GetRequiredService<IMapper>());

            _soccerCourtId = _memoryDb.SoccerCourts.Last().Id;
        }

        [Fact]
        public async Task DeleteSoccerCourtAsync_SoccerCourtService_DeletedSoccerCourtInSystem()
        {
            var cmdResult = await _soccerCourtService
                .DeleteSoccerCourtAsync(_soccerCourtId);

            cmdResult.Should().BeTrue();

            var deletedSoccerCourt = await _uow.SoccerCourts
                .GetByIdAsync(_soccerCourtId);

            deletedSoccerCourt.Should().BeNull();
        }

        [Fact]
        public async Task GetSoccerCourtByIdAsync_SoccerCourtService_GetSoccerCourtIfSameId()
        {
            var soccerCourt = await _soccerCourtService
                .GetSoccerCourtByIdAsync(_soccerCourtId);

            soccerCourt.Name.Should().Be("Soccer Court 1");
            soccerCourt.Address.Should().Be("Av. teste 10, teste");
        }

        [Fact]
        public async Task GetSoccerCourtsListAsync_SoccerCourtService_ListAllSoccerCourts()
        {
            var soccerCourts = await _soccerCourtService
                .GetSoccerCourtsListAsync();

            soccerCourts.Should().HaveCount(3);
        }

        [Fact]
        public async Task GetSoccerCourtsByGeoLocalization_SoccerCourtService_GetSoccerCourtsByGeoLocalization()
        {
            (double lat, double lon) = (-23.1087742, -46.5546822);

            var soccerCourts = await _soccerCourtService
                .GetSoccerCourtsByGeoLocalizationAsync(lat, lon);

            soccerCourts.Should().HaveCount(2);
        }

        [Fact]
        public async Task UpdateSoccerCourtAsync_SoccerCourtService_UpdatedSoccerCourtInSystem()
        {
            var newSoccerCourtName = new Faker().Company.CompanyName();
            var newSoccerCourtAddress = new Faker().Address.FullAddress();

            var soccerCourt = await _soccerCourtService
                .GetSoccerCourtByIdAsync(_soccerCourtId);
            soccerCourt.Name = newSoccerCourtName;
            soccerCourt.Address = newSoccerCourtAddress;

            var cmdResult = await _soccerCourtService
                .UpdateSoccerCourtAsync(_soccerCourtId, soccerCourt);

            cmdResult.Should().BeTrue();

            var updatedSoccerCourt = await _soccerCourtService
                .GetSoccerCourtByIdAsync(_soccerCourtId);

            updatedSoccerCourt.Name.Should().Be(newSoccerCourtName);
            updatedSoccerCourt.Address.Should().Be(newSoccerCourtAddress);
        }

        [Fact]
        public async Task AddSoccerCourtAsync_SoccerCourtService_AddedSoccerCourtInSystem()
        {
            var faker = new Faker("pt_BR");
            var newSoccerCourt = new QuadraModel
            {
                Name = faker.Company.CompanyName(),
                Image = faker.Image.PicsumUrl(),
                HourPrice = faker.Random.Decimal(90, 130),
                Phone = faker.Phone.PhoneNumber("(##) ####-####"),
                Address = faker.Address.FullAddress(),
                Cep = faker.Address.ZipCode("#####-###"),
                Latitude = faker.Address.Latitude(),
                Longitude = faker.Address.Longitude(),
                OwnerUserId = _memoryDb.Users.Last().Id
            };

            var cmdResult = await _soccerCourtService
                .AddSoccerCourtAsync(newSoccerCourt);

            cmdResult.Should().BeTrue();

            var insertedSoccerCourt = _memoryDb.SoccerCourts.Last();

            insertedSoccerCourt.Name.Should().Be(newSoccerCourt.Name);
            insertedSoccerCourt.Image.Should().Be(newSoccerCourt.Image);
            insertedSoccerCourt.HourPrice.Should().Be(newSoccerCourt.HourPrice);
            insertedSoccerCourt.Phone.Should().Be(newSoccerCourt.Phone);
            insertedSoccerCourt.Address.Should().Be(newSoccerCourt.Address);
            insertedSoccerCourt.Cep.Should().Be(newSoccerCourt.Cep);
            insertedSoccerCourt.Latitude.Should().Be(newSoccerCourt.Latitude);
            insertedSoccerCourt.Longitude.Should().Be(newSoccerCourt.Longitude);
            insertedSoccerCourt.OwnerUserId.Should().Be(newSoccerCourt.OwnerUserId);
        }
    }
}
