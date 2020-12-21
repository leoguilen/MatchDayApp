using MatchDayApp.Application.Models.Base;
using MatchDayApp.Domain.Entidades.Enum;
using System;

namespace MatchDayApp.Application.Models
{
    public class PartidaModel : BaseModel
    {
        public Guid PrimeiroTimeId { get; set; }
        public bool PrimeiroTimeConfirmado { get; set; }
        public Guid SegundoTimeId { get; set; }
        public bool SegundoTimeConfirmado { get; set; }
        public Guid QuadraFutebolId { get; set; }
        public int HorasPartida { get; set; }
        public DateTime DataPartida { get; set; }
        public StatusPartida StatusPartida { get; set; }
    }
}
