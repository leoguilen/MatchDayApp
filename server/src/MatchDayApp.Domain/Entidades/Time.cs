using MatchDayApp.Domain.Entidades.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchDayApp.Domain.Entidades
{
    public class Time : Entity
    {
        public string Nome { get; set; }
        public string Imagem { get; set; }
        public int QtdIntegrantes { get; set; }
        public Guid UsuarioProprietarioId { get; set; }

        [ForeignKey(nameof(UsuarioProprietarioId))]
        public virtual Usuario UsuarioProprietario { get; set; }
    }
}
