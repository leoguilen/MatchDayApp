using Bogus;
using FluentAssertions;
using MatchDayApp.Domain.Configuracoes;
using MatchDayApp.Domain.Entidades;
using MatchDayApp.Domain.Entidades.Enum;
using MatchDayApp.Domain.Helpers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MatchDayApp.UnitTest.Helpers
{
    [Trait("Helpers", "Token")]
    public class TokenHelperTest
    {
        private readonly string _salt = SenhaHasherHelper.CriarSalt(8);
        private readonly JwtConfiguracao _jwtOptions = new JwtConfiguracao
        {
            Secret = "9ce891b219b6fb5b0088e3e05e05baf5",
            TokenLifetime = TimeSpan.FromMinutes(5)
        };

        [Fact]
        public async Task GerarTokenUsuarioAsync_TokenHelper_GerarERetornarTokenDoUsuario()
        {
            var usuario = new Faker<Usuario>()
                .RuleFor(u => u.Nome, f => f.Person.FirstName)
                .RuleFor(u => u.Sobrenome, f => f.Person.LastName)
                .RuleFor(u => u.Email, f => f.Person.Email)
                .RuleFor(u => u.Username, f => f.UniqueIndex + f.Person.UserName)
                .RuleFor(u => u.Senha, f => SenhaHasherHelper.GerarHash(f.Internet.Password(), _salt))
                .RuleFor(u => u.Salt, _salt)
                .RuleFor(u => u.TipoUsuario, TipoUsuario.Jogador);

            var result = await TokenHelper
                .GerarTokenUsuarioAsync(usuario, _jwtOptions);

            result.Length.Should().BeGreaterThan(20);
            result.Should().NotBeNullOrEmpty();
        }
    }
}
