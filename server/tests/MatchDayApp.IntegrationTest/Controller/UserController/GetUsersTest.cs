using FluentAssertions;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Query;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Response;
using MatchDayApp.Infra.CrossCutting.V1;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace MatchDayApp.IntegrationTest.Controller.UserController
{
    [Trait("UserController", "Get")]
    public class GetUsersTest : ControllerTest
    {
        private readonly string _requestUri = ApiRoutes.User.GetAll;

        private readonly PaginationQuery _pagination;

        public GetUsersTest(CustomWebApplicationFactory factory,
            ITestOutputHelper output) : base(factory, output)
        {
            _pagination = new PaginationQuery
            {
                PageNumber = 1,
                PageSize = 10
            };
        }

        [Fact]
        public async Task GetAll_UserController_GetAllUsers()
        {
            await AuthenticateAsync();

            var response = await HttpClientTest
                .GetAsync(_requestUri);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var userResponse = await response.Content
                .ReadAsAsync<PagedResponse<UserResponse>>();

            userResponse.Data.Should().SatisfyRespectively(
                user1 =>
                {
                    user1.Username.Should().Be("test1");
                    user1.Email.Should().Be("test1@email.com");
                },
                user2 =>
                {
                    user2.Username.Should().Be("test2");
                    user2.Email.Should().Be("test2@email.com");
                },
                user3 =>
                {
                    user3.Username.Should().Be("test3");
                    user3.Email.Should().Be("test3@email.com");
                });

            Output.WriteLine($"Response: {JsonSerializer.Serialize(userResponse)}");
        }
    }
}
