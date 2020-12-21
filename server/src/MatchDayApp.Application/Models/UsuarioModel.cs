using MatchDayApp.Application.Models.Base;
using MatchDayApp.Domain.Entidades.Enum;

namespace MatchDayApp.Application.Models
{
    public class UsuarioModel : BaseModel
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public string Avatar { get; set; }
    }
}
