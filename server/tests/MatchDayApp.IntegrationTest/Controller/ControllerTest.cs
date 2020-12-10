using Bogus;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Auth;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Response.Auth;
using MatchDayApp.Infra.CrossCutting.V1;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace MatchDayApp.IntegrationTest.Controller
{
    public class ControllerTest : IClassFixture<CustomWebApplicationFactory>
    {
        protected readonly HttpClient HttpClientTest;
        protected readonly ITestOutputHelper Output;
        protected readonly Faker Faker = new Faker("pt_BR"); 

        public ControllerTest(CustomWebApplicationFactory factory, ITestOutputHelper output)
        {
            Output = output;
            HttpClientTest = factory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            HttpClientTest.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        private async Task<string> GetJwtAsync()
        {
            var response = await HttpClientTest.PostAsJsonAsync(
                ApiRoutes.Authentication.Login,
                new LoginRequest
                {
                    Email = "test2@email.com",
                    Password = "test321"
                });

            var authResponse = await response.Content
                .ReadAsAsync<AuthSuccessResponse>();

            return authResponse.Token;
        }
    }
}
