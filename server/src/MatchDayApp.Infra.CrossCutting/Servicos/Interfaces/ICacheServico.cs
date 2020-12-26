using System;

namespace MatchDayApp.Infra.CrossCutting.Servicos.Interfaces
{
    public interface ICacheServico
    {
        T GetCachedResponse<T>(string key);
        string GetCachedResponse(string key);
        void SetCacheResponse<T>(string key, T response);
        void SetCacheResponse<T>(string key, T response, TimeSpan timeLive);
        void RemoveCacheResponse(string key);
    }
}
