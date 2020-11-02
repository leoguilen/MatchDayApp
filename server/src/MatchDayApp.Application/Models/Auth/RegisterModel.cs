using MatchDayApp.Domain.Entities.Enum;

namespace MatchDayApp.Application.Models.Auth
{
    public class RegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public UserType UserType { get; set; }
        public string Avatar { get; set; }
    }
}
