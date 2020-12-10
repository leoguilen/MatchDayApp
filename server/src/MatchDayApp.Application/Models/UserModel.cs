using MatchDayApp.Application.Models.Base;
using MatchDayApp.Domain.Entities.Enum;

namespace MatchDayApp.Application.Models
{
    public class UserModel : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public UserType UserType { get; set; }
        public string Avatar { get; set; }
    }
}
