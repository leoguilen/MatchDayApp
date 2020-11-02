using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Auth;
using Swashbuckle.AspNetCore.Filters;

namespace MatchDayApp.Infra.CrossCutting.SwaggerExamples.Request
{
    public class UserLoginRequestExample : IExamplesProvider<LoginRequest>
    {
        public LoginRequest GetExamples()
        {
            return new LoginRequest
            {
                Email = "mateussilva@email.com",
                Password = "mateus123"
            };
        }
    }
}
