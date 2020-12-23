using System;
using System.Text.Json.Serialization;

namespace MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Partida
{
    public class MarcarPartidaRequest
    {
        public Guid PrimeiroTimeId { get; set; }

        [JsonIgnore]
        public bool PrimeiroTimeConfirmado { get; set; } = true;
        public Guid SegundoTimeId { get; set; }

        [JsonIgnore]
        public bool SegundoTimeConfirmado { get; set; } = false;
        public Guid QuadraFutebolId { get; set; }
        public int HorasPartida { get; set; }
        public DateTime DataPartida { get; set; }
    }
}
