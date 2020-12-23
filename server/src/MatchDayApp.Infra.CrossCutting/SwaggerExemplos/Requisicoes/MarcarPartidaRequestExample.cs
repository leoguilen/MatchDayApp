using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Partida;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace MatchDayApp.Infra.CrossCutting.SwaggerExemplos.Requisicoes
{
    public class MarcarPartidaRequestExample : IExamplesProvider<MarcarPartidaRequest>
    {
        public MarcarPartidaRequest GetExamples()
        {
            return new MarcarPartidaRequest
            {
                PrimeiroTimeId = Guid.NewGuid(),
                SegundoTimeId = Guid.NewGuid(),
                QuadraFutebolId = Guid.NewGuid(),
                DataPartida = DateTime.Now,
                HorasPartida = 1
            };
        }
    }
}
