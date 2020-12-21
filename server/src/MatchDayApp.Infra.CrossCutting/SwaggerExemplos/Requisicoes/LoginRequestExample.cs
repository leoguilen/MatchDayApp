using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Auth;
using Swashbuckle.AspNetCore.Filters;

namespace MatchDayApp.Infra.CrossCutting.SwaggerExemplos.Requisicoes
{
    public class LoginRequestExample : IExamplesProvider<LoginRequest>
    {
        public LoginRequest GetExamples()
        {
            return new LoginRequest
            {
                Email = "leonardoguilen1@gmail.com",
                Senha = "Leo@123"
            };
        }
    }
}
