using System;

namespace MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Query
{
    public class PartidaFilterQuery
    {
        public Guid QuadraId { get; set; }
        public Guid TimeId { get; set; }
    }
}
