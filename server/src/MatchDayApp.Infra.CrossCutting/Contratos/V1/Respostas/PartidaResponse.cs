using MatchDayApp.Domain.Entidades.Enum;
using System;

namespace MatchDayApp.Infra.CrossCutting.Contratos.V1.Respostas
{
    public class PartidaResponse
    {
        public Guid PrimeiroTimeId { get; set; }
        public string PrimeiroTimeNome { get; set; }
        public bool PrimeiroTimeConfirmado { get; set; }
        public Guid SegundoTimeId { get; set; }
        public string SegundoTimeNome { get; set; }
        public bool SegundoTimeConfirmado { get; set; }
        public Guid QuadraFutebolId { get; set; }
        public string QuadraFutebolNome { get; set; }
        public int HorasPartida { get; set; }
        public DateTime DataPartida { get; set; }
        public StatusPartida StatusPartida { get; set; }
    }
}
