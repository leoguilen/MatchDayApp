using System.Collections.Generic;

namespace MatchDayApp.Infra.CrossCutting.Contratos.V1.Respostas.Auth
{
    public class AutenticacaoComFalhaResponse
    {
        public string Mensagem { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
