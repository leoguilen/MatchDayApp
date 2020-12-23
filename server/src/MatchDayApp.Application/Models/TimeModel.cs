using MatchDayApp.Application.Models.Base;
using System;

namespace MatchDayApp.Application.Models
{
    public class TimeModel : BaseModel
    {
        public string Nome { get; set; }
        public string Imagem { get; set; }
        public int QtdIntegrantes { get; set; } = 0;
        public Guid UsuarioProprietarioId { get; set; }
    }
}
