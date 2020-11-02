using MatchDayApp.Application.Models;

namespace MatchDayApp.Infra.CrossCutting.Contract.V1.Response.Auth
{
    public class AuthSuccessResponse
    {
        public string Message { get; set; }
        public string Token { get; set; }
        public UserModel User { get; set; }
    }
}
