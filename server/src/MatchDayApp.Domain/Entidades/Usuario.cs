using MatchDayApp.Domain.Entidades.Base;
using MatchDayApp.Domain.Entidades.Enum;

namespace MatchDayApp.Domain.Entidades
{
    public class Usuario : Entity
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmado { get; set; } = false;
        public string Telefone { get; set; }
        public string Senha { get; set; }
        public string Salt { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public string Avatar { get; set; } = string.Empty;
        public bool Deletado { get; set; } = false;

        public virtual UsuarioTime UsuarioTime { get; set; }
    }
}
