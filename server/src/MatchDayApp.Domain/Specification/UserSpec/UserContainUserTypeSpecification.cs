using MatchDayApp.Domain.Entities;
using MatchDayApp.Domain.Entities.Enum;
using MatchDayApp.Domain.Specification.Base;

namespace MatchDayApp.Domain.Specification.UserSpec
{
    public class UserContainUserTypeSpecification : BaseSpecification<User>
    {
        public UserContainUserTypeSpecification(UserType userType)
            : base(u => u.UserType.ToString().Contains(userType.ToString()))
        {
        }

        public UserContainUserTypeSpecification()
            : base(null)
        {
        }
    }
}
