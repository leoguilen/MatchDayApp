using MatchDayApp.Domain.Entidades;
using MatchDayApp.Domain.Especificacoes.Base;
using MatchDayApp.Domain.Helpers;

namespace MatchDayApp.Domain.Especificacoes.Quadra
{
    public class QuadraProximaAoUsuarioEspecificacao : BaseSpecification<QuadraFutebol>
    {
        public QuadraProximaAoUsuarioEspecificacao(double lat, double lon, bool ordenarPorProximidade = true)
            : base(sc => CalculadorDistanciaCoordenadasHelper.EhProximo(lat, lon, sc.Latitude, sc.Longitude))
        {
            if (ordenarPorProximidade)
                ApplyOrderBy(sc => CalculadorDistanciaCoordenadasHelper
                    .CalcularParaMetros(lat, lon, sc.Latitude, sc.Longitude));

            AddInclude(sc => sc.UsuarioProprietario);
        }

        public QuadraProximaAoUsuarioEspecificacao()
            : base(null)
        {
            AddInclude(sc => sc.UsuarioProprietario);
        }
    }
}
