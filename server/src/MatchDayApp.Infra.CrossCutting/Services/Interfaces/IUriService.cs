using System;

namespace MatchDayApp.Infra.CrossCutting.Services.Interfaces
{
    public interface IUriService
    {
        Uri GetAllUri(int pageNumber = 1, int pageSize= 100);
    }
}
