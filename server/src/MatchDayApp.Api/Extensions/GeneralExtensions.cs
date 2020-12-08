using Microsoft.AspNetCore.Http;
using System.Linq;

namespace MatchDayApp.Api.Extensions
{
    public static class GeneralExtensions
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            if (httpContext.User == null)
                return string.Empty;

            return httpContext.User.Claims
                .Single(x => x.Type == "Id").Value;
        }

        public static string GetUserType(this HttpContext httpContext)
        {
            if (httpContext.User == null)
                return string.Empty;

            return httpContext.User.Claims
                .Single(x => x.Type == "UserType").Value;
        }
    }
}
