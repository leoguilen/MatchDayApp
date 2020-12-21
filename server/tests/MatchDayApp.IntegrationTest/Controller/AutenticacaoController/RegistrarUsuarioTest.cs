using Bogus;
using FluentAssertions;
using MatchDayApp.Domain.Entidades.Enum;
using MatchDayApp.Infra.CrossCutting.Contratos.V1;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Auth;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace MatchDayApp.IntegrationTest.Controller.AutenticacaoController
{
    [Trait("AutenticacaoController", "Register")]
    public class RegistrarUsuarioTest : ControllerTest
    {
        private readonly string _requestUri = ApiRotas.Autenticacao.RegistrarUsuario;
        private readonly RegistrarUsuarioRequest _registerRequest;

        public RegistrarUsuarioTest(CustomWebApplicationFactory factory,
            ITestOutputHelper output) : base(factory, output)
        {
            _registerRequest = new RegistrarUsuarioRequest
            {
                Nome = "Mateus",
                Sobrenome = "Silva",
                Username = "mateus.silva",
                Email = "mateussilva@email.com",
                Senha = "Mateus@123",
                ConfirmacaoSenha = "Mateus@123",
                TipoUsuario = TipoUsuario.Jogador,
                Avatar = "avatar.png"
            };
        }

        #region FirstName Validation

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task Register_AuthenticationController_FailedResponseIfFirstNameIsNullOrEmpty(string invalidFirstName)
        {
            _registerRequest.FirstName = invalidFirstName;

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _registerRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dictionary.MV005 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_registerRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        [Fact]
        public async Task Register_AuthenticationController_FailedResponseIfFirstNameIsLessThan4()
        {
            _registerRequest.FirstName = Faker.Random.String2(1, 3);

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _registerRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dictionary.MV006 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_registerRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        [Fact]
        public async Task Register_AuthenticationController_FailedResponseIfFirstNameHasSpecialCaractersOrNumber()
        {
            _registerRequest.FirstName = Faker.Random.String2(5, 10, chars: "abc123#@$");

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _registerRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dictionary.MV009 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_registerRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        #endregion

        #region LastName Validation

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task Register_AuthenticationController_FailedResponseIfLastNameIsNullOrEmpty(string invalidLastName)
        {
            _registerRequest.LastName = invalidLastName;

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _registerRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dictionary.MV007 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_registerRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        [Fact]
        public async Task Register_AuthenticationController_FailedResponseIfLastNameIsLessThan4()
        {
            _registerRequest.LastName = Faker.Random.String2(1, 3);

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _registerRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dictionary.MV008 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_registerRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        [Fact]
        public async Task Register_AuthenticationController_FailedResponseIfLastNameHasSpecialCaractersOrNumber()
        {
            _registerRequest.LastName = Faker.Random.String2(5, 10, chars: "abc123#@$");

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _registerRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dictionary.MV010 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_registerRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        #endregion

        #region Email Validation

        [Fact]
        public async Task Register_AuthenticationController_FailedResponseIfEmailIsInvalid()
        {
            _registerRequest.Email = Faker.Random.String2(15, 20);

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _registerRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dictionary.MV011 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_registerRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        #endregion

        #region Username Validation

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task Register_AuthenticationController_FailedResponseIfUsernameIsNullOrEmpty(string invalidUsername)
        {
            _registerRequest.UserName = invalidUsername;

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _registerRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dictionary.MV012 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_registerRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        #endregion

        #region Password Validation

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task Register_AuthenticationController_FailedResponseIfPasswordIsNullOrEmpty(string invalidPassword)
        {
            _registerRequest.Password = invalidPassword;

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _registerRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dictionary.MV013 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_registerRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        [Fact]
        public async Task Register_AuthenticationController_FailedResponseIfPasswordIsNotMatchStrongPattern()
        {
            var invalidPassword = Faker.Internet.Password();
            _registerRequest.Password = invalidPassword;
            _registerRequest.ConfirmPassword = invalidPassword;

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _registerRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dictionary.MV014 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_registerRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        #endregion

        #region ConfirmPassword Validation

        [Fact]
        public async Task Register_AuthenticationController_FailedResponseIfConfirmPasswordIsNotEqualPassword()
        {
            _registerRequest.ConfirmPassword = Faker.Internet.Password();

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _registerRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dictionary.MV015 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_registerRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        #endregion

        [Fact]
        public async Task Register_AuthenticationController_FailedResponseIfEmailAlreadyExists()
        {
            var existingEmail = "test2@email.com";
            _registerRequest.Email = existingEmail;

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _registerRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Message.Should().Be(Dictionary.ME005);
            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dictionary.MV003 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_registerRequest)} 
                              | Resultado teste: {response.StatusCode}");

        }

        [Fact]
        public async Task Register_AuthenticationController_SuccessResponseAndRegisterUser()
        {
            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _registerRequest);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComSucessoResponse>();

            authResponse.Message.Should().Be(Dictionary.MS003);
            authResponse.Token.Should().NotBeNullOrEmpty();
            authResponse.User.Email.Should().Be(_registerRequest.Email);

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_registerRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }
    }
}
