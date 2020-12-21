using System;

namespace MatchDayApp.Infra.CrossCutting.Contratos.V1.Respostas
{
    public class UsuarioResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public int TipoUsuario { get; set; }
        public string Avatar { get; set; }
    }
}
