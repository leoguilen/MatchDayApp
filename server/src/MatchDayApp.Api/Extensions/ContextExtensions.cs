using Microsoft.AspNetCore.Http;
using System.Linq;

namespace MatchDayApp.Api.Extensions
{
    public static class ContextExtensions
    {
        public static string ObterUsuarioId(this HttpContext httpContext)
        {
            if (httpContext.User == null)
                return string.Empty;

            return httpContext.User.Claims
                .Single(x => x.Type == "Id").Value;
        }

        public static string ObterTipoUsuario(this HttpContext httpContext)
        {
            if (httpContext.User == null)
                return string.Empty;

            return httpContext.User.Claims
                .Single(x => x.Type == "TipoUsuario").Value;
        }
    }
}
