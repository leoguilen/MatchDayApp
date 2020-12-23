using MatchDayApp.Domain.Entidades.Enum;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Auth;
using Swashbuckle.AspNetCore.Filters;

namespace MatchDayApp.Infra.CrossCutting.SwaggerExemplos.Requisicoes
{
    public class RegistrarUsuarioRequestExample : IExamplesProvider<RegistrarUsuarioRequest>
    {
        public RegistrarUsuarioRequest GetExamples()
        {
            return new RegistrarUsuarioRequest
            {
                Nome = "Teste",
                Sobrenome = "Exemplo",
                Username = "teste.exemplo",
                Email = "teste.exemplo@email.com",
                Senha = "Teste@exemplo123",
                ConfirmacaoSenha = "Teste@exemplo123",
                Telefone = "+55 (11)1234-5678",
                TipoUsuario = TipoUsuario.Jogador,
                Avatar = "avatar.png"
            };
        }
    }
}
