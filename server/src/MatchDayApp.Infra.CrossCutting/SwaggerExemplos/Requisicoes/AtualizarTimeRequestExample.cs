using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Time;
using Swashbuckle.AspNetCore.Filters;

namespace MatchDayApp.Infra.CrossCutting.SwaggerExemplos.Requisicoes
{
    public class AtualizarTimeRequestExample : IExamplesProvider<AtualizarTimeRequest>
    {
        public AtualizarTimeRequest GetExamples()
        {
            return new AtualizarTimeRequest
            {
                Nome = "Novo nome",
                Imagem = "Nova imagem"
            };
        }
    }
}
