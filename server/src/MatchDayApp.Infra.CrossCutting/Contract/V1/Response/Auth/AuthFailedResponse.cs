using System.Collections.Generic;

namespace MatchDayApp.Infra.CrossCutting.Contract.V1.Response.Auth
{
    public class AuthFailedResponse
    {
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
