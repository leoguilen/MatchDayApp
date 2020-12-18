using Bogus;
using FluentAssertions;
using MatchDayApp.Domain.Entities;
using MatchDayApp.Domain.Repository;
using MatchDayApp.Domain.Specification.SoccerCourtSpec;
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
    [Trait("Repositories", "SoccerCourtRepository")]
    public class SoccerCourtRepositoryTest
    {
        private readonly MatchDayAppContext _memoryDb;
        private readonly ISoccerCourtRepository _soccerCourtRepository;

        private readonly Faker<QuadraFutebol> _fakeSoccerCourt;
        private readonly QuadraFutebol _soccerCourtTest;
        private readonly object _expectedSoccerCourt;

        public SoccerCourtRepositoryTest()
        {
            var configServices = ServicesConfiguration.Configure();

            _memoryDb = configServices
                .GetRequiredService<MatchDayAppContext>()
                .SeedFakeData();

            _soccerCourtRepository = new QuadraFutebolRepositorio(_memoryDb);
            _soccerCourtTest = _memoryDb.SoccerCourts.First();

            _fakeSoccerCourt = new Faker<QuadraFutebol>()
                .RuleFor(sc => sc.Name, f => f.Company.CompanyName())
                .RuleFor(sc => sc.Image, f => f.Image.PicsumUrl())
                .RuleFor(sc => sc.HourPrice, f => f.Random.Decimal(80M, 200M))
                .RuleFor(sc => sc.Phone, f => f.Phone.PhoneNumber("(##) ####-####"))
                .RuleFor(sc => sc.Address, f => f.Address.FullAddress())
                .RuleFor(sc => sc.Cep, f => f.Address.ZipCode("#####-###"))
                .RuleFor(sc => sc.Latitude, f => f.Address.Latitude())
                .RuleFor(sc => sc.Longitude, f => f.Address.Longitude())
                .RuleFor(sc => sc.OwnerUserId, f => _memoryDb.Users.First().Id);

            _expectedSoccerCourt = new
            {
                Name = "Soccer Court 3",
                Image = "soccerCourt3.png",
                HourPrice = 90M,
                Phone = "(11) 3692-1472",
                Address = "Av. teste 321, teste",
                Cep = "01012-345",
                Latitude = -23.1096504,
                Longitude = -46.533172,
            };
        }

        [Fact, Order(1)]
        public void ShouldCanConnectInMemoryDatabase()
        {
            _memoryDb.Database.IsInMemory().Should().BeTrue();
            _memoryDb.Database.CanConnect().Should().BeTrue();
        }

        [Fact, Order(2)]
        public async Task ListAllAsync_SoccerCourt_AllSoccerCourtsRegistered()
        {
            var soccerCourts = await _soccerCourtRepository
                .ListAllAsync();

            soccerCourts.Should()
                .NotBeNull()
                .And.HaveCount(3)
                .And.SatisfyRespectively(
                    sc1 =>
                    {
                        sc1.Name.Should().Be("Soccer Court 3");
                        sc1.HourPrice.Should().Be(90M);
                        sc1.OwnerUser.Username.Should().Be("test3");
                        sc1.OwnerUser.Email.Should().Be("test3@email.com");
                    },
                    sc2 =>
                    {
                        sc2.Name.Should().Be("Soccer Court 2");
                        sc2.HourPrice.Should().Be(110M);
                        sc2.OwnerUser.Username.Should().Be("test2");
                        sc2.OwnerUser.Email.Should().Be("test2@email.com");
                    },
                    sc3 =>
                    {
                        sc3.Name.Should().Be("Soccer Court 1");
                        sc3.HourPrice.Should().Be(100M);
                        sc3.OwnerUser.Username.Should().Be("test1");
                        sc3.OwnerUser.Email.Should().Be("test1@email.com");
                    });
        }

        [Fact, Order(3)]
        public async Task GetByIdAsync_SoccerCourt_OneSoccerCourtWithSameId()
        {
            var soccerCourt = await _soccerCourtRepository
                .GetByIdAsync(_soccerCourtTest.Id);

            soccerCourt.Should().BeEquivalentTo(_expectedSoccerCourt, options =>
                options.ExcludingMissingMembers());
            soccerCourt.OwnerUser.Should()
                .NotBeNull()
                .And.BeOfType<Usuario>();
        }

        [Fact, Order(4)]
        public async Task GetByIdAsync_SoccerCourt_NullWithSoccerCourtIdNotRegistered()
        {
            var invalidId = Guid.NewGuid();

            var soccerCourt = await _soccerCourtRepository
                .GetByIdAsync(invalidId);

            soccerCourt.Should().BeNull();
        }

        [Fact, Order(5)]
        public async Task GetAsync_SoccerCourt_GetSoccerCourtWithMatchTheOwnerUserSpecification()
        {
            var spec = new SoccerCourtWithUserSpecification(_soccerCourtTest.OwnerUserId);
            var soccerCourt = (await _soccerCourtRepository.GetAsync(spec)).FirstOrDefault();

            soccerCourt.Should().BeEquivalentTo(_expectedSoccerCourt, options =>
                options.ExcludingMissingMembers());
            soccerCourt.OwnerUser.Should()
                .NotBeNull()
                .And.BeOfType<Usuario>();
        }

        [Fact, Order(6)]
        public async Task GetAsync_SoccerCourt_GetSoccerCourtWithMatchTheSoccerCourtNameSpecification()
        {
            var spec = new SoccerCourtWithUserSpecification("Soccer Court 3");
            var soccerCourt = (await _soccerCourtRepository.GetAsync(spec)).FirstOrDefault();

            soccerCourt.Should().BeEquivalentTo(_expectedSoccerCourt, options =>
                options.ExcludingMissingMembers());
            soccerCourt.OwnerUser.Should()
                .NotBeNull()
                .And.BeOfType<Usuario>();
        }

        [Fact, Order(7)]
        public async Task GetAsync_SoccerCourt_GetSoccerCourtsOrdernedByName()
        {
            var teams = await _soccerCourtRepository
                .GetAsync(null, x => x.OrderBy(u => u.Name), "OwnerUser", true);

            teams.Should()
                .NotBeNull()
                .And.HaveCount(3)
                .And.SatisfyRespectively(
                    sc1 =>
                    {
                        sc1.Name.Should().Be("Soccer Court 1");
                        sc1.HourPrice.Should().Be(100M);
                        sc1.OwnerUser.Username.Should().Be("test1");
                        sc1.OwnerUser.Email.Should().Be("test1@email.com");
                    },
                    sc2 =>
                    {
                        sc2.Name.Should().Be("Soccer Court 2");
                        sc2.HourPrice.Should().Be(110M);
                        sc2.OwnerUser.Username.Should().Be("test2");
                        sc2.OwnerUser.Email.Should().Be("test2@email.com");
                    },
                    sc3 =>
                    {
                        sc3.Name.Should().Be("Soccer Court 3");
                        sc3.HourPrice.Should().Be(90M);
                        sc3.OwnerUser.Username.Should().Be("test3");
                        sc3.OwnerUser.Email.Should().Be("test3@email.com");
                    });
        }

        [Fact, Order(8)]
        public async Task CountAsync_SoccerCourt_CountTotalSoccerCourtsMatchedTheSpecification()
        {
            const int expectedTotalCount = 1;

            var spec = new SoccerCourtWithUserSpecification("Soccer Court 1");
            var soccerCourtCount = await _soccerCourtRepository.CountAsync(spec);

            soccerCourtCount.Should()
                .Be(expectedTotalCount);
        }

        [Fact, Order(9)]
        public async Task AddRangeAsync_SoccerCourt_AddedListSoccerCourts()
        {
            var result = await _soccerCourtRepository
                .AddRangeAsync(_fakeSoccerCourt.Generate(5));

            result.Should().NotBeNull()
                .And.HaveCount(5);
            _memoryDb.SoccerCourts.Should().HaveCount(8);
        }

        [Fact, Order(10)]
        public async Task SaveAsync_SoccerCourt_UpdatedSoccerCourt()
        {
            var soccerCourtToUpdate = _memoryDb.SoccerCourts.Last();

            soccerCourtToUpdate.Name = "Soccer court updated";
            soccerCourtToUpdate.HourPrice = 150M;

            var result = await _soccerCourtRepository
                .SaveAsync(soccerCourtToUpdate);

            result.Should().NotBeNull()
                .And.BeOfType<QuadraFutebol>();
            result.Name.Should().Be("Soccer court updated");
            result.HourPrice.Should().Be(150M);
        }

        [Fact, Order(11)]
        public async Task DeleteAsync_SoccerCourt_UpdatedSoccerCourt()
        {
            var soccerCourtToDelete = _memoryDb.SoccerCourts.Last();

            await _soccerCourtRepository
                .DeleteAsync(soccerCourtToDelete);

            var deletedSoccerCourt = await _soccerCourtRepository
                .GetByIdAsync(soccerCourtToDelete.Id);

            deletedSoccerCourt.Should().BeNull();
            _memoryDb.SoccerCourts.Should().HaveCount(2);
        }

        [Fact, Order(12)]
        public async Task GetAsync_SoccerCourt_GetSoccerCourtsNearbyToUserSpecification()
        {
            var spec = new QuadrasProximasAoUsuarioEspecificao(-23.109136, -46.5582639);
            var soccerCourts = await _soccerCourtRepository.GetAsync(spec);

            soccerCourts.Should()
                .HaveCount(2)
                .And.SatisfyRespectively(
                sc1 =>
                {
                    sc1.Name.Should().Be("Soccer Court 1");
                    sc1.Address.Should().Be("Av. teste 10, teste");
                    sc1.OwnerUser.Should().NotBeNull();
                    sc1.OwnerUser.Username.Should().Be("test1");
                },
                sc2 =>
                {
                    sc2.Name.Should().Be("Soccer Court 3");
                    sc2.Address.Should().Be("Av. teste 321, teste");
                    sc2.OwnerUser.Should().NotBeNull();
                    sc2.OwnerUser.Username.Should().Be("test3");
                });
        }
    }
}
