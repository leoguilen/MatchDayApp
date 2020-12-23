using FluentAssertions;
using MatchDayApp.Domain.Repositorios;
using MatchDayApp.Infra.Data.Data;
using MatchDayApp.Infra.Data.Repositorios;
using MatchDayApp.UnitTest.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MatchDayApp.UnitTest.Repositorios
{
    [Trait("Repositorios", "Confirmacao Email")]
    public class ConfirmacaoEmailRepositorioTeste
    {
        private readonly MatchDayAppContext _memoryDb;
        private readonly IConfirmacaoEmailRepositorio _confirmacaoEmailRepositorio;

        public ConfirmacaoEmailRepositorioTeste()
        {
            var configServices = ServicesConfiguration.Configure();

            _memoryDb = configServices
                .GetRequiredService<MatchDayAppContext>()
                .SeedFakeData();

            _confirmacaoEmailRepositorio = new ConfirmacaoEmailRepositorio(_memoryDb);
        }

        [Fact]
        public async Task AdicionarRequisicaoAsync_ConfirmacaoEmailRepositorio_AdicionarRequisiçãoParaConfirmaçãoDeEmailDeNovoUsuario()
        {
            var usuarioId = _memoryDb.Usuarios.Last().Id;

            var result = await _confirmacaoEmailRepositorio
                .AdicionarRequisicaoAsync(usuarioId);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task ObterRequisicaoPorChaveAsync_ConfirmacaoEmailRepositorio_RetornarRequisicaoPorChaveDeConfirmacao()
        {
            var chave = Guid.Parse("C9267B0B-54A1-4971-9ED7-173008905696");

            var request = await _confirmacaoEmailRepositorio
                .ObterRequisicaoPorChaveAsync(chave);

            request.ChaveConfirmacao.Should().Be(chave);
        }

        [Fact]
        public void AtualizarRequisicao_ConfirmacaoEmailRepositorio_AtualizarFlagDeConfirmacaoDeEmailDoUsuario()
        {
            var request = _memoryDb.ConfirmacaoEmails.First();

            var result = _confirmacaoEmailRepositorio
                .AtualizarRequisicao(request);

            result.Should().BeTrue();
        }

    }
}
