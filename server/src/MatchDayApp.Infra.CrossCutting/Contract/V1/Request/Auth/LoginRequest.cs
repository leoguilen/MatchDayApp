namespace MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Auth
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
