using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Quadra;
using Swashbuckle.AspNetCore.Filters;

namespace MatchDayApp.Infra.CrossCutting.SwaggerExemplos.Requisicoes
{
    public class CriarQuadraRequestExample : IExamplesProvider<CriarQuadraRequest>
    {
        public CriarQuadraRequest GetExamples()
        {
            return new CriarQuadraRequest
            {
                Nome = "Quadra nome",
                Imagem = "Quadra imagem",
                PrecoHora = 110,
                Telefone = "(11) 4412-2012",
                Endereco = "Al. Rio branco, 402 - Centro/SP",
                Cep = "12345-600",
                Latitude = -23.90044,
                Longitude = -40.903344
            };
        }
    }
}
