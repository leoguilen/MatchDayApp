using System.Collections.Generic;

namespace MatchDayApp.Infra.CrossCutting.Contratos.V1.Respostas
{
    public class PagedResponse<T>
    {
        public PagedResponse() { }

        public PagedResponse(IEnumerable<T> data)
        {
            Dados = data;
        }

        public IEnumerable<T> Dados { get; set; }
        public int? NumeroPagina { get; set; }
        public int? QuantidadePagina { get; set; }
        public string ProximaPagina { get; set; }
        public string PaginaAnterior { get; set; }
    }
}
