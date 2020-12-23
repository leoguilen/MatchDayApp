using MatchDayApp.Domain.Entidades.Enum;
using MatchDayApp.Domain.Especificacoes.Base;

namespace MatchDayApp.Domain.Especificacoes.Usuario
{
    public class UsuarioContendoTipoUsuarioEspecificacao : BaseSpecification<Entidades.Usuario>
    {
        public UsuarioContendoTipoUsuarioEspecificacao(TipoUsuario tipoUsuario)
            : base(u => u.TipoUsuario.ToString().Contains(tipoUsuario.ToString()))
        {
        }

        public UsuarioContendoTipoUsuarioEspecificacao()
            : base(null)
        {
        }
    }
}
