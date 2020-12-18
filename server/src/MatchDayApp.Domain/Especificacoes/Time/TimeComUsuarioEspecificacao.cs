using MatchDayApp.Domain.Especificacoes.Base;
using System;

namespace MatchDayApp.Domain.Especificacoes.Time
{
    public class TimeComUsuarioEspecificacao : BaseSpecification<Entidades.Time>
    {
        public TimeComUsuarioEspecificacao(Guid usuarioPropId)
            : base(t => t.UsuarioProprietarioId == usuarioPropId)
        {
            AddInclude(t => t.UsuarioProprietario);
        }

        public TimeComUsuarioEspecificacao(string nomeTime)
            : base(t => t.Nome.Contains(nomeTime))
        {
            AddInclude(t => t.UsuarioProprietario);
        }

        public TimeComUsuarioEspecificacao()
            : base(null)
        {
            AddInclude(t => t.UsuarioProprietario);
        }
    }
}
