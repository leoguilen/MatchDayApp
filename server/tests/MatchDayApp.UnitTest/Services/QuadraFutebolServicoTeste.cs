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
    [Trait("Servicos", "Quadra de Futebol")]
    public class QuadraFutebolServicoTeste
    {
        private readonly IUnitOfWork _uow;
        private readonly IQuadraFutebolServico _quadraServico;
        private readonly MatchDayAppContext _memoryDb;

        private readonly Guid _quadraId;

        public QuadraFutebolServicoTeste()
        {
            var cfg = ServicesConfiguration.Configure();

            _memoryDb = cfg.GetRequiredService<MatchDayAppContext>()
                .SeedFakeData();
            _uow = cfg.GetRequiredService<IUnitOfWork>();

            _quadraServico = new QuadraFutebolServico(_uow,
                cfg.GetRequiredService<IMapper>());

            _quadraId = _memoryDb.Quadras.Last().Id;
        }

        [Fact]
        public async Task DeletarQuadraAsync_QuadraFutebolServico_DeletarQuadra()
        {
            var cmdResult = await _quadraServico
                .DeletarQuadraAsync(_quadraId);

            cmdResult.Should().BeTrue();

            var quadraDeletada = await _uow.QuadraFutebolRepositorio
                .GetByIdAsync(_quadraId);

            quadraDeletada.Should().BeNull();
        }

        [Fact]
        public async Task ObterQuadraPorIdAsync_QuadraFutebolServico_RetornarQuadraPorId()
        {
            var quadra = await _quadraServico
                .ObterQuadraPorIdAsync(_quadraId);

            quadra.Nome.Should().Be("Soccer Court 1");
            quadra.Endereco.Should().Be("Av. teste 10, teste");
        }

        [Fact]
        public async Task ObterQuadrasAsync_QuadraFutebolServico_RetornarListaComTodasQuadras()
        {
            var quadras = await _quadraServico
                .ObterQuadrasAsync();

            quadras.Should().HaveCount(3);
        }

        [Fact]
        public async Task ObterQuadrasPorLocalizacaoAsync_QuadraFutebolServico_RetornarQuadraPorLocalizacaoMaisProxima()
        {
            (double lat, double lon) = (-23.1087742, -46.5546822);

            var quadras = await _quadraServico
                .ObterQuadrasPorLocalizacaoAsync(lat, lon);

            quadras.Should().HaveCount(2);
        }

        [Fact]
        public async Task AtualizarQuadraAsync_QuadraFutebolServico_AtualizarQuadra()
        {
            var novoNomeQuadra = new Faker().Company.CompanyName();
            var novoEnderecoQuadra = new Faker().Address.FullAddress();

            var quadra = await _quadraServico
                .ObterQuadraPorIdAsync(_quadraId);
            quadra.Nome = novoNomeQuadra;
            quadra.Endereco = novoEnderecoQuadra;

            var cmdResult = await _quadraServico
                .AtualizarQuadraAsync(_quadraId, quadra);

            cmdResult.Should().NotBeNull();

            var quadraAtualizada = await _quadraServico
                .ObterQuadraPorIdAsync(_quadraId);

            quadraAtualizada.Nome.Should().Be(novoNomeQuadra);
            quadraAtualizada.Endereco.Should().Be(novoEnderecoQuadra);
        }

        [Fact]
        public async Task AdicionarQuadraAsync_QuadraFutebolServico_AdicionarQuadra()
        {
            var faker = new Faker("pt_BR");
            var novaQuadra = new QuadraModel
            {
                Nome = faker.Company.CompanyName(),
                Imagem = faker.Image.PicsumUrl(),
                PrecoHora = faker.Random.Decimal(90, 130),
                Telefone = faker.Phone.PhoneNumber("(##) ####-####"),
                Endereco = faker.Address.FullAddress(),
                Cep = faker.Address.ZipCode("#####-###"),
                Latitude = faker.Address.Latitude(),
                Longitude = faker.Address.Longitude(),
                UsuarioProprietarioId = _memoryDb.Usuarios.Last().Id
            };

            var cmdResult = await _quadraServico
                .AdicionarQuadraAsync(novaQuadra);

            cmdResult.Should().NotBeNull();

            var quadraInserida = _memoryDb.Quadras.Last();

            quadraInserida.Nome.Should().Be(novaQuadra.Nome);
            quadraInserida.Imagem.Should().Be(novaQuadra.Imagem);
            quadraInserida.PrecoHora.Should().Be(novaQuadra.PrecoHora);
            quadraInserida.Telefone.Should().Be(novaQuadra.Telefone);
            quadraInserida.Endereco.Should().Be(novaQuadra.Endereco);
            quadraInserida.Cep.Should().Be(novaQuadra.Cep);
            quadraInserida.Latitude.Should().Be(novaQuadra.Latitude);
            quadraInserida.Longitude.Should().Be(novaQuadra.Longitude);
            quadraInserida.UsuarioProprietarioId.Should().Be(novaQuadra.UsuarioProprietarioId);
        }
    }
}
