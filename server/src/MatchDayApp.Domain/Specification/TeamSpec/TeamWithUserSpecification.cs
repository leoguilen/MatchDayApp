using MatchDayApp.Domain.Entities;
using MatchDayApp.Domain.Specification.Base;
using System;

namespace MatchDayApp.Domain.Specification.TeamSpec
{
    public class TeamWithUserSpecification : BaseSpecification<Team>
    {
        public TeamWithUserSpecification(Guid ownerUserId)
            : base(t => t.OwnerUserId == ownerUserId)
        {
            AddInclude(t => t.OwnerUser);
        }

        public TeamWithUserSpecification(string teamName)
            : base(t => t.Name.Contains(teamName))
        {
            AddInclude(t => t.OwnerUser);
        }

        public TeamWithUserSpecification()
            : base(null)
        {
            AddInclude(t => t.OwnerUser);
        }
    }
}
