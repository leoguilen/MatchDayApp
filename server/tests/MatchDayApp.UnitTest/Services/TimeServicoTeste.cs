using AutoMapper;
using Bogus;
using FluentAssertions;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Servicos;
using MatchDayApp.Infra.Data.Data;
using MatchDayApp.UnitTest.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MatchDayApp.UnitTest.Servicos
{
    [Trait("Servicos", "Time")]
    public class TimeServicoTeste
    {
        private readonly IUnitOfWork _uow;
        private readonly ITimeServico _timeServico;
        private readonly MatchDayAppContext _memoryDb;

        private readonly Guid _timeId;

        public TimeServicoTeste()
        {
            var cfg = ServicesConfiguration.Configure();

            _memoryDb = cfg.GetRequiredService<MatchDayAppContext>()
                .SeedFakeData();
            _uow = cfg.GetRequiredService<IUnitOfWork>();

            _timeServico = new TimeServico(_uow,
                cfg.GetRequiredService<IMapper>());

            _timeId = _memoryDb.Times.Last().Id;
        }

        [Fact]
        public async Task DeletarTimeAsync_TimeServico_DeletarTime()
        {
            var cmdResult = await _timeServico
                .DeletarTimeAsync(_timeId);

            cmdResult.Should().BeTrue();

            var timeDeletado = await _uow.UsuarioRepositorio
                .GetByIdAsync(_timeId);

            timeDeletado.Should().BeNull();
        }

        [Fact]
        public async Task ObterTimePorIdAsync_TimeServico_RetornarTimePorId()
        {
            var time = await _timeServico
                .ObterTimePorIdAsync(_timeId);

            time.Nome.Should().Be("Team 1");
        }

        [Fact]
        public async Task ObterTimesAsync_TimeServico_RetornarListaComTodosTimes()
        {
            var times = await _timeServico
                .ObterTimesAsync();

            times.Should().HaveCount(3);
        }

        [Fact]
        public async Task AtualizarTimeAsync_TimeServico_AtualizarTime()
        {
            var novoNomeTime = new Faker().Company.CompanyName();
            var novaImagemTime = new Faker().Image.PicsumUrl();

            var time = await _timeServico
                .ObterTimePorIdAsync(_timeId);
            time.Nome = novoNomeTime;
            time.Imagem = novaImagemTime;

            var cmdResult = await _timeServico
                .AtualizarTimeAsync(_timeId, time);

            cmdResult.Should().NotBeNull();

            var timeAtualizado = await _timeServico
                .ObterTimePorIdAsync(_timeId);

            timeAtualizado.Nome.Should().Be(novoNomeTime);
            timeAtualizado.Imagem.Should().Be(novaImagemTime);
        }

        [Fact]
        public async Task AdicionarTimeAsync_TimeServico_AdicionarTime()
        {
            var faker = new Faker("pt_BR");
            var novoTime = new TimeModel
            {
                Nome = faker.Company.CompanyName(),
                Imagem = faker.Image.PicsumUrl(),
                QtdIntegrantes = faker.Random.Int(10, 20),
                UsuarioProprietarioId = _memoryDb.Usuarios.First().Id
            };

            var cmdResult = await _timeServico
                .AdicionarTimeAsync(novoTime);

            cmdResult.Should().NotBeNull();

            var timeInserido = _memoryDb.Times.Last();

            timeInserido.Nome.Should().Be(novoTime.Nome);
            timeInserido.Imagem.Should().Be(novoTime.Imagem);
            timeInserido.QtdIntegrantes.Should().Be(novoTime.QtdIntegrantes);
            timeInserido.UsuarioProprietarioId.Should().Be(novoTime.UsuarioProprietarioId);
        }
    }
}
