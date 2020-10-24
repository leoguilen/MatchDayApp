using Bogus;
using FluentAssertions;
using MatchDayApp.Domain.Common.Helpers;
using MatchDayApp.Domain.Configuration;
using MatchDayApp.Domain.Entities;
using MatchDayApp.Domain.Entities.Enum;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MatchDayApp.UnitTest.Helpers
{
    [Trait("Helpers", "TokenHelper")]
    public class TokenHelperTest
    {
        private readonly string _salt = SecurePasswordHasher.CreateSalt(8);
        private readonly JwtOptions _jwtOptions = new JwtOptions
        {
            Secret = "9ce891b219b6fb5b0088e3e05e05baf5",
            TokenLifetime = TimeSpan.FromMinutes(5)
        };

        [Fact]
        public async Task GenerateTokenForUserAsync_TokenHelper_ReturnToken()
        {
            var user = new Faker<User>()
                .RuleFor(u => u.FirstName, f => f.Person.FirstName)
                .RuleFor(u => u.LastName, f => f.Person.LastName)
                .RuleFor(u => u.Email, f => f.Person.Email)
                .RuleFor(u => u.Username, f => f.UniqueIndex + f.Person.UserName)
                .RuleFor(u => u.Password, f => SecurePasswordHasher.GenerateHash(f.Internet.Password(), _salt))
                .RuleFor(u => u.Salt, _salt)
                .RuleFor(u => u.UserType, UserType.Player);

            var result = await TokenHelper
                .GenerateTokenForUserAsync(user, _jwtOptions);

            result.Length.Should().BeGreaterThan(20);
            result.Should().NotBeNullOrEmpty();
        }
    }
}
