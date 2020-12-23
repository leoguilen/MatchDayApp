using System;
using System.ComponentModel.DataAnnotations;

namespace MatchDayApp.Domain.Entidades
{
    public class ConfirmacaoEmail
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime RequisicaoEm { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid ChaveConfirmacao { get; set; }
        public bool Confirmado { get; set; } = false;
    }
}
