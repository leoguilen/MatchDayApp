using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace MatchDayApp.IntegrationTest.Controller.UserController
{
    [Trait("UserController", "Get")]
    public class GetUsersTest : ControllerTest
    {
        private readonly string _requestUri = ApiRotas.User.GetAll;

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
                .ReadAsAsync<PagedResponse<UsuarioResponse>>();

            userResponse.Data.Should().SatisfyRespectively(
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
