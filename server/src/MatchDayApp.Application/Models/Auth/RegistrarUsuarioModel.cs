using MatchDayApp.Domain.Entidades.Enum;

namespace MatchDayApp.Application.Models.Auth
{
    public class RegistrarUsuarioModel
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Username { get; set; }
        public string Senha { get; set; }
        public string ConfirmacaoSenha { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public string Avatar { get; set; }
    }
}
