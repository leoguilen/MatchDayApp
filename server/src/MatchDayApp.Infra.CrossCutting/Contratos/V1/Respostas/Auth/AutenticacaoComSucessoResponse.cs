using MatchDayApp.Application.Models;

namespace MatchDayApp.Infra.CrossCutting.Contratos.V1.Respostas.Auth
{
    public class AutenticacaoComSucessoResponse
    {
        public string Mensagem { get; set; }
        public string Token { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}
