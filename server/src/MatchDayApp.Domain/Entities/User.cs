using MatchDayApp.Domain.Entities.Base;

namespace MatchDayApp.Domain.Entities
{
    public class User : Entity
    {
        public User(string firstName, string lastName, string username, string email, string password, string avatar = default)
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Email = email;
            Password = password;
            Avatar = avatar;
        }

        public User() { }

        public string FirstName { get; set; }
        public string LastName { get; set;  }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool ConfirmedEmail { get; set; } = false;
        public string Password { get; set; }
        public string Salt { get; set; } = "abc";
        public string Avatar { get; set; }
        public bool Deleted { get; set; } = false;
    }
}
