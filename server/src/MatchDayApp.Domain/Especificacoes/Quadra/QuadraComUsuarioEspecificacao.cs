using MatchDayApp.Domain.Entidades;
using MatchDayApp.Domain.Especificacoes.Base;
using System;

namespace MatchDayApp.Domain.Especificacoes.Quadra
{
    public class QuadraComUsuarioEspecificacao : BaseSpecification<QuadraFutebol>
    {
        public QuadraComUsuarioEspecificacao(Guid usuarioPropId)
            : base(sc => sc.UsuarioProprietarioId == usuarioPropId)
        {
            AddInclude(sc => sc.UsuarioProprietario);
        }

        public QuadraComUsuarioEspecificacao(string nomeQuadra)
            : base(sc => sc.Nome.Contains(nomeQuadra))
        {
            AddInclude(sc => sc.UsuarioProprietario);
        }

        public QuadraComUsuarioEspecificacao()
            : base(null)
        {
            AddInclude(sc => sc.UsuarioProprietario);
        }
    }
}
