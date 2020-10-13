using MatchDayApp.Domain.Entities;
using MatchDayApp.Domain.Specification.Base;

namespace MatchDayApp.Domain.Specification.UserSpec
{
    public class UserContainEmailOrUsernameSpecification : BaseSpecification<User>
    {
        public UserContainEmailOrUsernameSpecification(string input)
            : base(u => u.Email.Contains(input) || u.Username.Contains(input))
        {
        }

        public UserContainEmailOrUsernameSpecification()
            : base(null)
        {
        }
    }
}
