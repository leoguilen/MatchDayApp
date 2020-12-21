using MatchDayApp.Infra.CrossCutting.Contratos.V1;
using MatchDayApp.Infra.CrossCutting.Servicos.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;

namespace MatchDayApp.Infra.CrossCutting.Servicos
{
    public class UriServico : IUriServico
    {
        private readonly string _baseUri;

        public UriServico(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetAllUri(int pageNumber = 1, int pageSize = 100)
        {
            var modifieldUri = QueryHelpers.AddQueryString(_baseUri, new Dictionary<string, string>(
                new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("pageNumber", pageNumber.ToString()),
                    new KeyValuePair<string, string>("pageSize", pageSize.ToString())
                }));

            return new Uri(modifieldUri);
        }

        public Uri GetQuadraUri(string quadraId)
        {
            return new Uri(_baseUri + ApiRotas.Quadra.Get.Replace("{id}", quadraId));
        }

        public Uri GetTimeUri(string timeId)
        {
            return new Uri(_baseUri + ApiRotas.Time.Get.Replace("{id}", timeId));
        }

        public Uri GetUsuarioUri(string usuarioId)
        {
            return new Uri(_baseUri + ApiRotas.Usuario.Get.Replace("{id}", usuarioId));
        }
    }
}
