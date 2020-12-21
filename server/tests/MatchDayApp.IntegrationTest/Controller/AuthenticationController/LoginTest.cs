using Bogus;
using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace MatchDayApp.IntegrationTest.Controller.AuthenticationController
{
    [Trait("AuthenticationController", "Login")]
    public class LoginTest : ControllerTest
    {
        private readonly string _requestUri = ApiRotas.Authentication.Login;
        private readonly LoginRequest _loginRequest;

        public LoginTest(CustomWebApplicationFactory factory,
            ITestOutputHelper output) : base(factory, output)
        {
            _loginRequest = new LoginRequest
            {
                Email = "test2@email.com",
                Password = "test321"
            };
        }

        [Fact]
        public async Task Login_AuthenticationController_FailedResponseIfEmailIsInvalid()
        {
            _loginRequest.Email = Faker.Person.Email;

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _loginRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Message.Should()
                .Be(Dictionary.ME004);
            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dictionary.MV001 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_loginRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        [Fact]
        public async Task Login_AuthenticationController_FailedResponseIfPasswordIsInvalid()
        {
            _loginRequest.Password = Faker.Internet.Password();

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _loginRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Message.Should()
                .Be(Dictionary.ME004);
            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dictionary.MV002 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_loginRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        [Fact]
        public async Task Login_AuthenticationController_FailedResponseIfUserIsDeleted()
        {
            var loginUserDeletedRequest = new LoginRequest
            {
                Email = "test1@email.com",
                Password = "test123"
            };

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, loginUserDeletedRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Message.Should()
                .Be(Dictionary.ME004);
            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dictionary.MV001 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_loginRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        [Fact]
        public async Task Login_AuthenticationController_SuccessResponse()
        {
            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _loginRequest);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var authResponse = await response.Content.ReadAsAsync<AutenticacaoComSucessoResponse>();

            authResponse.Message.Should().Be(Dictionary.MS001);
            authResponse.Token.Should().NotBeNullOrEmpty();
            authResponse.User.Email.Should().Be(_loginRequest.Email);

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_loginRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }
    }
}
