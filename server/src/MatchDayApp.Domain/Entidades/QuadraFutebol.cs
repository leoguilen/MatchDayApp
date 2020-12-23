using MatchDayApp.Domain.Entidades.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchDayApp.Domain.Entidades
{
    public class QuadraFutebol : Entity
    {
        public string Nome { get; set; }
        public string Imagem { get; set; }
        public decimal PrecoHora { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string Cep { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Guid UsuarioProprietarioId { get; set; }

        [ForeignKey(nameof(UsuarioProprietarioId))]
        public virtual Usuario UsuarioProprietario { get; set; }
    }
}
