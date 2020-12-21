using FluentAssertions;
using MatchDayApp.Infra.Data.Data;
using MatchDayApp.UnitTest.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MatchDayApp.UnitTest.Persistence
{
    [Trait("Repositories", "UserConfirmEmailRepository")]
    public class UserConfirmEmailRepositoryTest
    {
        private readonly MatchDayAppContext _memoryDb;
        private readonly IConfirmacaoEmailRepositorio _confirmEmailRepository;

        public UserConfirmEmailRepositoryTest()
        {
            var configServices = ServicesConfiguration.Configure();

            _memoryDb = configServices
                .GetRequiredService<MatchDayAppContext>()
                .SeedFakeData();

            _confirmEmailRepository = new ConfirmacaoEmailRepositorio(_memoryDb);
        }

        [Fact]
        public async Task AddRequestAsync_UserConfirmEmail_InsertRequestToUserConfirmRegisteredEmail()
        {
            var userId = _memoryDb.Users.Last().Id;

            var result = await _confirmEmailRepository
                .AddRequestAsync(userId);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task GetRequestByKeyAsync_UserConfirmEmail_ReturnRequestToConfirmEmail()
        {
            var confirmKey = Guid.Parse("C9267B0B-54A1-4971-9ED7-173008905696");

            var request = await _confirmEmailRepository
                .GetRequestByKeyAsync(confirmKey);

            request.ConfirmKey.Should().Be(confirmKey);
        }

        [Fact]
        public void UpdateRequest_UserConfirmEmail_UpdatedConfirmedFlag()
        {
            var request = _memoryDb.UserConfirmEmails.First();

            var result = _confirmEmailRepository
                .UpdateRequest(request);

            result.Should().BeTrue();
        }

    }
}
