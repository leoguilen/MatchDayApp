using MatchDayApp.Domain.Entities.Enum;

namespace MatchDayApp.Infra.CrossCutting.Contract.V1.Request.User
{
    public class UpdateUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public UserType UserType { get; set; }
        public string Avatar { get; set; }
    }
}
