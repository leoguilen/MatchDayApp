using Bogus;
using FluentAssertions;
using MatchDayApp.Domain.Entidades.Enum;
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
    [Trait("AutenticacaoController", "Registrar Usuario")]
    public class RegistrarUsuarioTest : ControllerTest
    {
        private readonly string _requestUri = ApiRotas.Autenticacao.RegistrarUsuario;
        private readonly RegistrarUsuarioRequest _registrarUsuarioRequest;

        public RegistrarUsuarioTest(CustomWebApplicationFactory factory,
            ITestOutputHelper output) : base(factory, output)
        {
            _registrarUsuarioRequest = new RegistrarUsuarioRequest
            {
                Nome = Faker.Random.String2(4, 10),
                Sobrenome = Faker.Random.String2(4, 10),
                Username = Faker.Person.UserName,
                Email = Faker.Person.Email,
                Senha = "Teste@123",
                ConfirmacaoSenha = "Teste@123",
                TipoUsuario = TipoUsuario.Jogador,
                Avatar = Faker.Person.Avatar
            };
        }

        #region Nome Validation

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task RegistrarUsuario_AutenticacaoController_RespostaComFalhaSeNomeEhNuloOuVazio(string nomeInvalido)
        {
            _registrarUsuarioRequest.Nome = nomeInvalido;

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _registrarUsuarioRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dicionario.MV005 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_registrarUsuarioRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        [Fact]
        public async Task RegistrarUsuario_AutenticacaoController_RespostaComFalhaSeNomePossuiMenosDe4Caracteres()
        {
            _registrarUsuarioRequest.Nome = Faker.Random.String2(1, 3);

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _registrarUsuarioRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dicionario.MV006 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_registrarUsuarioRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        [Fact]
        public async Task RegistrarUsuario_AutenticacaoController_RespostaComFalhaSeNomeTiverCaracteresEspeciaisOuNumeros()
        {
            _registrarUsuarioRequest.Nome = Faker.Random.String2(5, 10, chars: "abc123#@$");

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _registrarUsuarioRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dicionario.MV009 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_registrarUsuarioRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        #endregion

        #region Sobrenome Validation

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task RegistrarUsuario_AutenticacaoController_RespostaComFalhaSeSobrenomeEhNuloOuVazio(string sobrenomeInvalido)
        {
            _registrarUsuarioRequest.Sobrenome = sobrenomeInvalido;

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _registrarUsuarioRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dicionario.MV007 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_registrarUsuarioRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        [Fact]
        public async Task RegistrarUsuario_AutenticacaoController_RespostaComFalhaSeSobrenomePossuirMenosDe4Caracteres()
        {
            _registrarUsuarioRequest.Sobrenome = Faker.Random.String2(1, 3);

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _registrarUsuarioRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dicionario.MV008 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_registrarUsuarioRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        [Fact]
        public async Task RegistrarUsuario_AutenticacaoController_RespostaComFalhaSeSobrenomeTiverCaracteresEspeciaisOuNumeros()
        {
            _registrarUsuarioRequest.Sobrenome = Faker.Random
                .String2(5, 10, chars: "abc123#@$");

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _registrarUsuarioRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dicionario.MV010 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_registrarUsuarioRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        #endregion

        #region Email Validation

        [Fact]
        public async Task RegistrarUsuario_AutenticacaoController_RespostaComFalhaSeEmailEhInvalido()
        {
            _registrarUsuarioRequest.Email = Faker.Random.String2(15, 20);

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _registrarUsuarioRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dicionario.MV011 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_registrarUsuarioRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        #endregion

        #region Username Validation

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task RegistrarUsuario_AutenticacaoController_RespostaComFalhaSeUsernameEhNuloOuVazio(string usernameInvalido)
        {
            _registrarUsuarioRequest.Username = usernameInvalido;

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _registrarUsuarioRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dicionario.MV012 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_registrarUsuarioRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        #endregion

        #region Senha Validation

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task RegistrarUsuario_AutenticacaoController_RespostaComFalhaSeSenhaEhNulaOuVazia(string senhaInvalida)
        {
            _registrarUsuarioRequest.Senha = senhaInvalida;

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _registrarUsuarioRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dicionario.MV013 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_registrarUsuarioRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        [Fact]
        public async Task RegistrarUsuario_AutenticacaoController_RespostaComFalhaSeSenhaNaoCorresponderAoPadraoForte()
        {
            var invalidPassword = Faker.Random.String2(10, 15);
            _registrarUsuarioRequest.Senha = invalidPassword;
            _registrarUsuarioRequest.ConfirmacaoSenha = invalidPassword;

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _registrarUsuarioRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dicionario.MV014 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_registrarUsuarioRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        #endregion

        #region Confirmar Senha Validation

        [Fact]
        public async Task RegistrarUsuario_AutenticacaoController_RespostaComFalhaSeConfirmacaoDeSenhaForDiferenteDaSenha()
        {
            _registrarUsuarioRequest.ConfirmacaoSenha = Faker.Internet.Password();

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _registrarUsuarioRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dicionario.MV015 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_registrarUsuarioRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        #endregion

        [Fact]
        public async Task RegistrarUsuario_AutenticacaoController_RespostaComFalhaSeEmailJaExiste()
        {
            var existingEmail = "test2@email.com";
            _registrarUsuarioRequest.Email = existingEmail;

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _registrarUsuarioRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Mensagem.Should().Be(Dicionario.ME005);
            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dicionario.MV003 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_registrarUsuarioRequest)} 
                              | Resultado teste: {response.StatusCode}");

        }

        [Fact]
        public async Task RegistrarUsuario_AutenticacaoController_RespostaComSucessoEUsuarioRegistrado()
        {
            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _registrarUsuarioRequest);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComSucessoResponse>();

            authResponse.Mensagem.Should().Be(Dicionario.MS003);
            authResponse.Token.Should().NotBeNullOrEmpty();
            authResponse.Usuario.Email.Should().Be(_registrarUsuarioRequest.Email);

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_registrarUsuarioRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }
    }
}
