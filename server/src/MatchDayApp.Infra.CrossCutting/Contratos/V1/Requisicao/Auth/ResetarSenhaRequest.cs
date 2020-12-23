namespace MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Auth
{
    public class ResetarSenhaRequest
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Senha { get; set; }
        public string ConfirmacaoSenha { get; set; }
    }
}
