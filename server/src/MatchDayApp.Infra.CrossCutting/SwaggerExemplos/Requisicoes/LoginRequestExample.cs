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
                Email = "teste.exemplo@email.com",
                Senha = "Teste@exemplo123"
            };
        }
    }
}
