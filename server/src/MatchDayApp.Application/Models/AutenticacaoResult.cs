using System.Collections.Generic;

namespace MatchDayApp.Application.Models
{
    public class AutenticacaoResult
    {
        public string Mensagem { get; set; }
        public bool Sucesso { get; set; }
        public string Token { get; set; }
        public UsuarioModel Usuario { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
