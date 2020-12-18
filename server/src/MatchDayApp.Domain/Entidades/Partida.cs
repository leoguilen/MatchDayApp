using MatchDayApp.Domain.Entidades.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchDayApp.Domain.Entidades
{
    public class Partida
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid PrimeiroTimeId { get; set; }
        public bool PrimeiroTimeConfirmado { get; set; }
        public Guid SegundoTimeId { get; set; }
        public bool SegundoTimeConfirmado { get; set; }
        public Guid QuadraFutebolId { get; set; }
        public int HorasPartida { get; set; }
        public DateTime DataPartida { get; set; }
        public StatusPartida StatusPartida { get; set; }

        [ForeignKey(nameof(PrimeiroTimeId))]
        public Time PrimeiroTime { get; set; }

        [ForeignKey(nameof(SegundoTimeId))]
        public Time SegundoTime { get; set; }

        [ForeignKey(nameof(QuadraFutebolId))]
        public QuadraFutebol QuadraFutebol { get; set; }
    }
}
