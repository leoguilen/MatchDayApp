using MatchDayApp.Domain.Especificacoes.Base;

namespace MatchDayApp.Domain.Especificacoes.Usuario
{
    public class UsuarioContendoEmailOuUsernameEspecificacao : BaseSpecification<Entidades.Usuario>
    {
        public UsuarioContendoEmailOuUsernameEspecificacao(string input)
            : base(u => u.Email.Contains(input) || u.Username.Contains(input))
        {
        }

        public UsuarioContendoEmailOuUsernameEspecificacao()
            : base(null)
        {
        }
    }
}
