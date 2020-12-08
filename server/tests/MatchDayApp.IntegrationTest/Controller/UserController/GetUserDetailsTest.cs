using FluentAssertions;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Response;
using MatchDayApp.Infra.CrossCutting.V1;
using MatchDayApp.Infra.Data.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace MatchDayApp.IntegrationTest.Controller.UserController
{
    [Trait("UserController", "Get")]
    public class GetUserDetailsTest : ControllerTest
    {
        private readonly string _requestUri = ApiRoutes.User.Get;

        private readonly ITestOutputHelper _output;
        private readonly MatchDayAppContext _memoryDb;

        public GetUserDetailsTest(CustomWebApplicationFactory factory,
            ITestOutputHelper output) : base(factory)
        {
            _memoryDb = factory.Services
                .GetRequiredService<MatchDayAppContext>()
                .SeedFakeData();

            _output = output;
        }

        //[Fact]
        public async Task Get_UserController_GetUserDetailsById()
        {
            var userId = _memoryDb.Users.Last().Id;

            await AuthenticateAsync();

            var response = await HttpClientTest
                .GetAsync(_requestUri.Replace("{id}", userId.ToString()));

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var userResponse = await response.Content
                .ReadAsAsync<Response<UserResponse>>();

            userResponse.Data.Id.Should().Be(userId);
            userResponse.Data.Username.Should().Be("test3");
            userResponse.Data.Email.Should().Be("test3@email.com");

            _output.WriteLine($"Response: {JsonSerializer.Serialize(userResponse)}");
        }
    }
}
