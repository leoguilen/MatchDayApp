using FluentAssertions;
using MatchDayApp.Infra.CrossCutting.Contratos.V1;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Query;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Respostas;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace MatchDayApp.IntegrationTest.Controller.UsuarioController
{
    [Trait("UsuarioController", "GetAll")]
    public class ObterUsuariosTeste : ControllerTest
    {
        private readonly string _requestUri = ApiRotas.Usuario.GetAll;

        private readonly PaginacaoQuery _pagination;

        public ObterUsuariosTeste(CustomWebApplicationFactory factory,
            ITestOutputHelper output) : base(factory, output)
        {
            _pagination = new PaginacaoQuery
            {
                NumeroPagina = 1,
                QuantidadePagina = 10
            };
        }

        [Fact]
        public async Task GetAll_UsuariosController_RetornaListaComTodosUsuariosDoSistema()
        {
            await AuthenticateAsync();

            var response = await HttpClientTest
                .GetAsync(_requestUri);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var userResponse = await response.Content
                .ReadAsAsync<PagedResponse<UsuarioResponse>>();

            userResponse.Dados.Should().SatisfyRespectively(
                user1 =>
                {
                    user1.Username.Should().Be("test2");
                    user1.Email.Should().Be("test2@email.com");
                },
                user2 =>
                {
                    user2.Username.Should().Be("test3");
                    user2.Email.Should().Be("test3@email.com");
                });
        }
    }
}
