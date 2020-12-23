using System;

namespace MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Partida
{
    public class ConfirmarPartidaRequest
    {
        public Guid TimeId { get; set; }
        public Guid PartidaId { get; set; }
    }
}
