using Bogus;
using FluentAssertions;
using MatchDayApp.Application.Comandos.Time;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Queries.Time;
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
    [Trait("Handler", "Time")]
    public class TimeHandlerTeste
    {
        private readonly IMediator _mediator;
        private readonly MatchDayAppContext _memoryDb;
        private readonly Guid _timeId;
        private readonly Faker _faker;

        public TimeHandlerTeste()
        {
            var cfg = ServicesConfiguration.Configure();

            _memoryDb = cfg.GetRequiredService<MatchDayAppContext>()
                .SeedFakeData();

            _mediator = cfg.GetRequiredService<IMediator>();
            _timeId = _memoryDb.Times.Last().Id;

            _faker = new Faker("pt_BR");
        }

        [Fact]
        public async Task Handle_TeamHandler_RetornarListaComTodosTimes()
        {
            var query = new ObterTimesQuery { };

            var timesResult = await _mediator.Send(query);

            timesResult.Should().HaveCount(3);
        }

        [Fact]
        public async Task Handle_TeamHandler_RetornarTimePorId()
        {
            var query = new ObterTimePorIdQuery
            {
                Id = _timeId
            };

            var timeResult = await _mediator.Send(query);

            timeResult.Nome.Should().Be("Team 1");
        }

        [Fact]
        public async Task Handle_TeamHandler_AdicionarNovoTime()
        {
            var comando = new AdicionarTimeCommand
            {
                Time = new TimeModel
                {
                    Nome = _faker.Company.CompanyName(),
                    Imagem = _faker.Image.PicsumUrl(),
                    QtdIntegrantes = _faker.Random.Int(10, 20),
                    UsuarioProprietarioId = _memoryDb.Usuarios.ToList()[1].Id
                }
            };

            var cmdResult = await _mediator.Send(comando);

            cmdResult.Should().NotBeNull();

            var timeInserido = _memoryDb.Times.Last();

            timeInserido.Nome.Should().Be(comando.Time.Nome);
            timeInserido.Imagem.Should().Be(comando.Time.Imagem);
            timeInserido.QtdIntegrantes.Should().Be(comando.Time.QtdIntegrantes);
            timeInserido.UsuarioProprietarioId.Should().Be(comando.Time.UsuarioProprietarioId);
        }

        [Fact]
        public async Task Handle_TeamHandler_AtualizarTime()
        {
            var comando = new AtualizarTimeCommand
            {
                Id = _timeId,
                Time = new TimeModel
                {
                    Nome = _faker.Company.CompanyName(),
                    Imagem = _faker.Image.PicsumUrl()
                }
            };

            var cmdResult = await _mediator.Send(comando);

            cmdResult.Should().NotBeNull();

            var updatedTeam = _memoryDb.Times.Last();

            updatedTeam.Nome.Should().Be(comando.Time.Nome);
            updatedTeam.Imagem.Should().Be(comando.Time.Imagem);
        }

        [Fact]
        public async Task Handle_TeamHandler_DeletarTime()
        {
            var comando = new DeletarTimeCommand
            {
                Id = _timeId
            };

            var cmdResult = await _mediator.Send(comando);

            cmdResult.Should().BeTrue();
        }
    }
}
