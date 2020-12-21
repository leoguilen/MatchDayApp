using Bogus;
using FluentAssertions;
using MatchDayApp.Domain.Resources;
using MatchDayApp.Infra.CrossCutting.Contratos.V1;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Auth;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Respostas.Auth;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace MatchDayApp.IntegrationTest.Controller.AutenticacaoController
{
    [Trait("AutenticacaoController", "Login")]
    public class LoginTest : ControllerTest
    {
        private readonly string _requestUri = ApiRotas.Autenticacao.Login;
        private readonly LoginRequest _loginRequest;

        public LoginTest(CustomWebApplicationFactory factory,
            ITestOutputHelper output) : base(factory, output)
        {
            _loginRequest = new LoginRequest
            {
                Email = "test2@email.com",
                Senha = "test321"
            };
        }

        [Fact]
        public async Task Login_AutenticacaoController_RespostaDeFalhaQuandoEmailEhInvalido()
        {
            _loginRequest.Email = Faker.Person.Email;

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _loginRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Mensagem.Should()
                .Be(Dicionario.ME004);
            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dicionario.MV001 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_loginRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        [Fact]
        public async Task Login_AutenticacaoController_RespostaComFalhaQuandoSenhaEhInvalida()
        {
            _loginRequest.Senha = Faker.Internet.Password();

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _loginRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Mensagem.Should()
                .Be(Dicionario.ME004);
            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dicionario.MV002 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_loginRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        [Fact]
        public async Task Login_AuthenticationController_RespostaComFalhaQuandoUsuarioEstaDeletado()
        {
            var loginUserDeletedRequest = new LoginRequest
            {
                Email = "test1@email.com",
                Senha = "test123"
            };

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, loginUserDeletedRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Mensagem.Should()
                .Be(Dicionario.ME004);
            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dicionario.MV001 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_loginRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        [Fact]
        public async Task Login_AuthenticationController_RespostaComSucessoEUsuarioRecebeTokenDeAcesso()
        {
            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _loginRequest);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var authResponse = await response.Content.ReadAsAsync<AutenticacaoComSucessoResponse>();

            authResponse.Mensagem.Should().Be(Dicionario.MS001);
            authResponse.Token.Should().NotBeNullOrEmpty();
            authResponse.Usuario.Email.Should().Be(_loginRequest.Email);

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_loginRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }
    }
}
