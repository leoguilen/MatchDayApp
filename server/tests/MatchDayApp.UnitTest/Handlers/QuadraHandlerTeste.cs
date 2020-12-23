using Bogus;
using FluentAssertions;
using MatchDayApp.Application.Comandos.Quadra;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Queries.Quadra;
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
    [Trait("Handler", "Quadra")]
    public class QuadraHandlerTeste
    {
        private readonly IMediator _mediator;
        private readonly MatchDayAppContext _memoryDb;
        private readonly Guid _quadraId;
        private readonly Faker _faker;

        public QuadraHandlerTeste()
        {
            var cfg = ServicesConfiguration.Configure();

            _memoryDb = cfg.GetRequiredService<MatchDayAppContext>()
                .SeedFakeData();

            _mediator = cfg.GetRequiredService<IMediator>();
            _quadraId = _memoryDb.Quadras.Last().Id;

            _faker = new Faker("pt_BR");
        }

        [Fact]
        public async Task Handle_QuadraHandler_RetornarListaComTodasQuadrasDoSistema()
        {
            var query = new ObterQuadrasQuery { };

            var quadrasResult = await _mediator.Send(query);

            quadrasResult.Should().HaveCount(3);
        }

        [Fact]
        public async Task Handle_QuadraHandler_RetornarQuadraPorId()
        {
            var query = new ObterQuadraPorIdQuery
            {
                Id = _quadraId
            };

            var quadraResult = await _mediator.Send(query);

            quadraResult.Nome.Should().Be("Soccer Court 1");
        }

        [Fact]
        public async Task Handle_QuadraHandler_RetornarQuadrasProximasALocalizacao()
        {
            var query = new ObterQuadraPorLocalizacaoQuery
            {
                Lat = -23.1087742,
                Long = -46.5546822
            };

            var quadrasResult = await _mediator.Send(query);

            quadrasResult.Should().HaveCount(2);
        }

        [Fact]
        public async Task Handle_QuadraHandler_AdicionarQuadra()
        {
            var comando = new AdicionarQuadraCommand
            {
                Quadra = new QuadraModel
                {
                    Nome = _faker.Company.CompanyName(),
                    Imagem = _faker.Image.PicsumUrl(),
                    PrecoHora = _faker.Random.Decimal(90, 130),
                    Telefone = _faker.Phone.PhoneNumber("(##) ####-####"),
                    Endereco = _faker.Address.FullAddress(),
                    Cep = _faker.Address.ZipCode("#####-###"),
                    Latitude = _faker.Address.Latitude(),
                    Longitude = _faker.Address.Longitude(),
                    UsuarioProprietarioId = _memoryDb.Usuarios.Last().Id
                }
            };

            var cmdResult = await _mediator.Send(comando);

            cmdResult.Should().NotBeNull();

            var quadraInserida = _memoryDb.Quadras.Last();

            quadraInserida.Nome.Should().Be(comando.Quadra.Nome);
            quadraInserida.Imagem.Should().Be(comando.Quadra.Imagem);
            quadraInserida.PrecoHora.Should().Be(comando.Quadra.PrecoHora);
            quadraInserida.Telefone.Should().Be(comando.Quadra.Telefone);
            quadraInserida.Endereco.Should().Be(comando.Quadra.Endereco);
            quadraInserida.Cep.Should().Be(comando.Quadra.Cep);
            quadraInserida.Latitude.Should().Be(comando.Quadra.Latitude);
            quadraInserida.Longitude.Should().Be(comando.Quadra.Longitude);
            quadraInserida.UsuarioProprietarioId.Should().Be(comando.Quadra.UsuarioProprietarioId);
        }

        [Fact]
        public async Task Handle_QuadraHandler_AtualizarQuadra()
        {
            var comando = new AtualizarQuadraCommand
            {
                Id = _quadraId,
                Quadra = new QuadraModel
                {
                    Nome = _faker.Company.CompanyName(),
                    Imagem = _faker.Image.PicsumUrl()
                }
            };

            var cmdResult = await _mediator.Send(comando);

            cmdResult.Should().NotBeNull();

            var updatedSoccerCourt = _memoryDb.Quadras.Last();

            updatedSoccerCourt.Nome.Should().Be(comando.Quadra.Nome);
            updatedSoccerCourt.Imagem.Should().Be(comando.Quadra.Imagem);
        }

        [Fact]
        public async Task Handle_QuadraHandler_DeletarQuadra()
        {
            var comando = new DeletarQuadraCommand
            {
                Id = _quadraId
            };

            var cmdResult = await _mediator.Send(comando);

            cmdResult.Should().BeTrue();
        }
    }
}
