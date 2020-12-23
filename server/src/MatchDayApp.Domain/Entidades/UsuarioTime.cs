using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchDayApp.Domain.Entidades
{
    public class UsuarioTime
    {
        public Guid Id { get; private set; }
        public Guid UsuarioId { get; set; }
        public Guid TimeId { get; set; }
        public bool Aceito { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        public virtual Usuario Usuario { get; set; }

        [ForeignKey(nameof(TimeId))]
        public virtual Time Time { get; set; }
    }
}
