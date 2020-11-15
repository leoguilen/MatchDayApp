using MatchDayApp.Infra.CrossCutting.Services.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;

namespace MatchDayApp.Infra.CrossCutting.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
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
    }
}
