using MatchDayApp.Infra.CrossCutting.Servicos.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Text.Json;

namespace MatchDayApp.Infra.CrossCutting.Servicos
{
    public class CacheServico : ICacheServico
    {
        private readonly IMemoryCache _cache;

        public CacheServico(IMemoryCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public T GetCachedResponse<T>(string key)
        {
            if (!_cache.TryGetValue(key, out T response))
            {
                return default;
            }

            return response;
        }

        public string GetCachedResponse(string key)
        {
            if (!_cache.TryGetValue(key, out var response))
            {
                return default;
            }

            return JsonSerializer
                .Serialize(response);
        }

        public void RemoveCacheResponse(string key)
        {
            var cachedResponse = GetCachedResponse(key);

            if (!string.IsNullOrEmpty(cachedResponse))
                _cache.Remove(key);
        }

        public void SetCacheResponse<T>(string key, T response)
        {
            if (string.IsNullOrEmpty(GetCachedResponse(key)))
                _cache.Set(key, response);
        }

        public void SetCacheResponse<T>(string key, T response, TimeSpan timeLive)
        {
            if (string.IsNullOrEmpty(GetCachedResponse(key)))
                _cache.Set(key, response, timeLive);
        }
    }
}
