using MatchDayApp.Domain.Entities.Enum;

namespace MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Auth
{
    public class RegisterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string ConfirmPassword { get; set; }
        public UserType UserType { get; set; }
        public string Avatar { get; set; }
    }
}
