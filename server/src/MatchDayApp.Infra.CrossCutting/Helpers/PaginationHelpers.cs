using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Query;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Respostas;
using MatchDayApp.Infra.CrossCutting.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MatchDayApp.Infra.CrossCutting.Helpers
{
    public static class PaginationHelpers
    {
        public static PagedResponse<T> CriarRespostaPaginada<T>(IUriService uriService, PaginacaoQuery pagination, List<T> response)
        {
            var nextPage = pagination.NumeroPagina >= 1
                ? uriService.GetAllUri(pagination.NumeroPagina + 1, pagination.QuantidadePagina).ToString()
                : null;

            var previousPage = pagination.NumeroPagina - 1 >= 1
                ? uriService.GetAllUri(pagination.NumeroPagina - 1, pagination.QuantidadePagina).ToString()
                : null;

            return new PagedResponse<T>
            {
                Dados = response,
                NumeroPagina = pagination.NumeroPagina >= 1 ? pagination.NumeroPagina : (int?)null,
                QuantidadePagina = pagination.QuantidadePagina >= 1 ? pagination.QuantidadePagina : (int?)null,
                ProximaPagina = response.Any() ? nextPage : null,
                PaginaAnterior = previousPage
            };
        }
    }
}
