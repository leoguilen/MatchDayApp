using System;

namespace MatchDayApp.Infra.CrossCutting.Servicos.Interfaces
{
    public interface IUriServico
    {
        Uri GetTimeUri(string timeId);
        Uri GetUsuarioUri(string usuarioId);
        Uri GetQuadraUri(string quadraId);
        Uri GetAllUri(int pageNumber = 1, int pageSize = 100);
    }
}
