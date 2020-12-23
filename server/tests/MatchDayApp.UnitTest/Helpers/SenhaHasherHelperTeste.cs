using Bogus;
using FluentAssertions;
using MatchDayApp.Domain.Helpers;
using Xunit;

namespace MatchDayApp.UnitTest.Helpers
{
    [Trait("Helpers", "Senha Hasher")]
    public class SenhaHasherHelperTeste
    {
        private const int _tamanhoSalt = 8;

        [Fact]
        public void CriarSalt_SenhaHasherHelper_CriarSaltValido()
        {
            string salt = string.Empty;

            salt = SenhaHasherHelper.CriarSalt(_tamanhoSalt);

            salt.Should()
                .NotBeNullOrEmpty()
                .And.HaveLength(12)
                .And.NotMatchRegex("^[A-Z][a-zA-Z]*$");
        }

        [Theory]
        [InlineData("teste")]
        [InlineData("10#ASDMN3233_")]
        [InlineData("RaNdOmPwD123")]
        public void GerarHash_SenhaHasherHelper_GerarHashValidoDaSenha(string senha)
        {
            string hash = string.Empty;
            string salt = SenhaHasherHelper.CriarSalt(_tamanhoSalt);

            hash = SenhaHasherHelper.GerarHash(senha, salt);

            hash.Should()
                .NotBeNullOrEmpty()
                .And.HaveLength(44)
                .And.NotMatchRegex("^[A-Z][a-zA-Z]*$");
        }

        [Theory]
        [InlineData("TesTEE@123")]
        [InlineData("PWDword2020#")]
        [InlineData("HaShEdPwD7820=")]
        public void SaoIguais_SenhaHasherHelper_ValidarHashDeSenhaSeIgualASenha(string senha)
        {
            string senhaSalt = SenhaHasherHelper.CriarSalt(_tamanhoSalt);
            string senhaHash = SenhaHasherHelper.GerarHash(senha, senhaSalt);

            bool senhaValida = SenhaHasherHelper.SaoIguais(senha, senhaHash, senhaSalt);

            senhaValida.Should().BeTrue();
        }

        [Theory]
        [InlineData("TesTEE@123")]
        [InlineData("PWDword2020#")]
        [InlineData("HaShEdPwD7820=")]
        public void SaoIguais_SenhaHasherHelper_InvalidarSenhaSeHashDeSenhaNaoForIgual(string senha)
        {
            string senhaFake = new Faker().Internet.Password();
            string senhaSalt = SenhaHasherHelper.CriarSalt(_tamanhoSalt);
            string senhaHash = SenhaHasherHelper.GerarHash(senha, senhaSalt);

            bool senhaInvalida = SenhaHasherHelper.SaoIguais(senhaFake, senhaHash, senhaSalt);

            senhaInvalida.Should().BeFalse();
        }
    }
}
