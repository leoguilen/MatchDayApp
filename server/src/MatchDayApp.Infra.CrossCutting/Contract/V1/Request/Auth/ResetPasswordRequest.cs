namespace MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Auth
{
    public class ResetPasswordRequest
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
