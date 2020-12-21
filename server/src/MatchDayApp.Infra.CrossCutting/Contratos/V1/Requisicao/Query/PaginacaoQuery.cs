namespace MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Query
{
    public class PaginacaoQuery
    {
        public int NumeroPagina { get; set; } = 1;
        public int QuantidadePagina { get; set; } = 100;
    }
}
