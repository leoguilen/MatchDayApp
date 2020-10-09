using MatchDayApp.Domain.Entities.Base;
using MatchDayApp.Domain.Entities.Enum;
using Microsoft.AspNetCore.Identity;
using System;

namespace MatchDayApp.Domain.Entities
{
    public class User : IdentityUser<Guid>, IEntityBase<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserType UserType { get; set; }
        public string Avatar { get; set; }
    }
}
