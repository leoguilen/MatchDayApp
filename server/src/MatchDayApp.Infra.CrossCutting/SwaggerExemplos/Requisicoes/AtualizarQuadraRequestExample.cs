using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Quadra;
using Swashbuckle.AspNetCore.Filters;

namespace MatchDayApp.Infra.CrossCutting.SwaggerExemplos.Requisicoes
{
    public class AtualizarQuadraRequestExample : IExamplesProvider<AtualizarQuadraRequest>
    {
        public AtualizarQuadraRequest GetExamples()
        {
            return new AtualizarQuadraRequest
            {
                Nome = "Novo nome",
                Imagem = "Nova imagem",
                PrecoHora = 100,
                Telefone = "(11) 1020-4444",
                Endereco = "Rua dos tigos, 911 - Maracanã/RO",
                Cep = "09909-776",
                Latitude = -28.90044,
                Longitude = -55.903344
            };
        }
    }
}
