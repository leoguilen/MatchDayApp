using MatchDayApp.Domain.Entities.Base;
using MatchDayApp.Domain.Entities.Enum;

namespace MatchDayApp.Domain.Entities
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool ConfirmedEmail { get; set; } = false;
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public UserType UserType { get; set; }
        public string Avatar { get; set; } = string.Empty;
        public bool Deleted { get; set; } = false;
        public virtual UserTeam UserTeam { get; set; }
    }
}
