using MatchDayApp.Domain.Entidades.Enum;

namespace MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Usuario
{
    public class AtualizarUsuarioRequest
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public string Avatar { get; set; }
    }
}
