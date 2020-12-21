using Bogus;
using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace MatchDayApp.IntegrationTest.Controller.AutenticacaoController
{
    [Trait("AutenticacaoController", "ResetPassword")]
    public class ResetarSenhaTest : ControllerTest
    {
        private readonly string _requestUri = ApiRotas.Authentication.ResetPassword;
        private readonly ResetarSenhaRequest _resetPassRequest;

        public ResetarSenhaTest(CustomWebApplicationFactory factory,
            ITestOutputHelper output) : base(factory, output)
        {
            _resetPassRequest = new ResetarSenhaRequest
            {
                Email = "test2@email.com",
                Password = "Test@321",
                ConfirmPassword = "Test@321"
            };
        }

        #region Email Validation

        [Fact]
        public async Task ResetPassword_AuthenticationController_FailedResponseIfEmailIsInvalid()
        {
            _resetPassRequest.Email = Faker.Random.String2(15, 20);

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _resetPassRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dictionary.MV011 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_resetPassRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        #endregion

        #region Password Validation

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task ResetPassword_AuthenticationController_FailedResponseIfPasswordIsNullOrEmpty(string invalidPassword)
        {
            _resetPassRequest.Password = invalidPassword;

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _resetPassRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dictionary.MV013 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_resetPassRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        [Fact]
        public async Task ResetPassword_AuthenticationController_FailedResponseIfPasswordIsNotMatchStrongPattern()
        {
            var invalidPassword = Faker.Internet.Password();
            _resetPassRequest.Password = invalidPassword;
            _resetPassRequest.ConfirmPassword = invalidPassword;

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _resetPassRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dictionary.MV014 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_resetPassRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        #endregion

        #region ConfirmPassword Validation

        [Fact]
        public async Task ResetPassword_AuthenticationController_FailedResponseIfConfirmPasswordIsNotEqualPassword()
        {
            _resetPassRequest.ConfirmPassword = Faker.Internet.Password();

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _resetPassRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Errors.Should()
                .HaveCount(1).And
                .BeEquivalentTo(new[] { Dictionary.MV015 });

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_resetPassRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        #endregion

        [Fact]
        public async Task ResetPassword_AuthenticationController_FailedResponseIfEmailNotExists()
        {
            _resetPassRequest.Email = Faker.Person.Email;

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _resetPassRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Message.Should()
                .Be(Dictionary.ME001);

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_resetPassRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        [Fact]
        public async Task ResetPassword_AuthenticationController_FailedResponseIfUserIsDeleted()
        {
            _resetPassRequest.Email = "test1@email.com";

            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _resetPassRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComFalhaResponse>();

            authResponse.Message.Should()
                .Be(Dictionary.ME001);

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_resetPassRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }

        [Fact]
        public async Task ResetPassword_AuthenticationController_SuccessResponse()
        {
            var response = await HttpClientTest
                .PostAsJsonAsync(_requestUri, _resetPassRequest);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var authResponse = await response.Content
                .ReadAsAsync<AutenticacaoComSucessoResponse>();

            authResponse.Message.Should()
                .Be(Dictionary.MS002);

            Output.WriteLine($@"Valor entrada: {JsonSerializer.Serialize(_resetPassRequest)} 
                              | Resultado teste: {response.StatusCode}");
        }
    }
}
