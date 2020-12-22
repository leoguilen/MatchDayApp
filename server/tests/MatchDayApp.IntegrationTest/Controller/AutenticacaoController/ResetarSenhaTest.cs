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
    [Trait("AutenticacaoController", "Resetar Senha")]
    public class ResetarSenhaTest : ControllerTest
    {
        private readonly string _requestUri = ApiRotas.Autenticacao.ResetarSenha;
        private readonly ResetarSenhaRequest _resetarSenhaRequest;

        public ResetarSenhaTest(CustomWebApplicationFactory factory,
            ITestOutputHelper output) : base(factory, output)
        {
            _resetarSenhaRequest = new ResetarSenhaRequest
            {
                Email = "test2@email.com",
                Senha = "Test@321",
                ConfirmacaoSenha = "Test@321"
            };
        }

        #region Email Validation

        [Fact]
        public async Task ResetarSenha_AutenticacaoController_RespostaComFalhaSeEmailEhInvalido()
        {
            _resetarSenhaRequest.Email = Faker.Random.String2(15, 20);

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _resetarSenhaRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dicionario.MV011 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_resetarSenhaRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        #endregion

        #region Senha Validation

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task ResetarSenha_AutenticacaoController_RespostaComFalhaSeSenhaEhNulaOuVazia(string senhaInvalida)
        {
            _resetarSenhaRequest.Senha = senhaInvalida;

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _resetarSenhaRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dicionario.MV013 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_resetarSenhaRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        [Fact]
        public async Task ResetarSenha_AutenticacaoController_RespostaComFalhaSeSenhaNaoCorresponderComPadraoForte()
        {
            var invalidPassword = Faker.Internet.Password();
            _resetarSenhaRequest.Senha = invalidPassword;
            _resetarSenhaRequest.ConfirmacaoSenha = invalidPassword;

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _resetarSenhaRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dicionario.MV014 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_resetarSenhaRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        #endregion

        #region Confirmar Senha Validation

        [Fact]
        public async Task ResetarSenha_AutenticacaoController_RespostaComFalhaSeConfirmacaoDeSenhaNaoForIgualASenha()
        {
            _resetarSenhaRequest.ConfirmacaoSenha = Faker.Internet.Password();

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _resetarSenhaRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dicionario.MV015 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_resetarSenhaRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        #endregion

        [Fact]
        public async Task ResetarSenha_AutenticacaoController_RespostaComFalhaSeEmailNaoExiste()
        {
            _resetarSenhaRequest.Email = Faker.Person.Email;

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _resetarSenhaRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Mensagem.Should()
                .Be(Dicionario.ME001);

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_resetarSenhaRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        [Fact]
        public async Task ResetarSenha_AutenticacaoController_RespostaComFalhaSeUsuarioEstaDeletado()
        {
            _resetarSenhaRequest.Email = "test1@email.com";

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _resetarSenhaRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Mensagem.Should()
                .Be(Dicionario.ME001);

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_resetarSenhaRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        [Fact]
        public async Task ResetarSenha_AutenticacaoController_RespostaDeSucessoESenhaDoUsuarioResetada()
        {
            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _resetarSenhaRequest);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComSucessoResponse>();

            authResponse.Mensagem.Should()
                .Be(Dicionario.MS002);

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_resetarSenhaRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }
    }
}
