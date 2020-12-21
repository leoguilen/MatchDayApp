using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Time;
using Swashbuckle.AspNetCore.Filters;

namespace MatchDayApp.Infra.CrossCutting.SwaggerExemplos.Requisicoes
{
    public class CriarTimeRequestExample : IExamplesProvider<CriarTimeRequest>
    {
        public CriarTimeRequest GetExamples()
        {
            return new CriarTimeRequest
            {
                Nome = "Nome time",
                Imagem = "Imagem time"
            };
        }
    }
}
