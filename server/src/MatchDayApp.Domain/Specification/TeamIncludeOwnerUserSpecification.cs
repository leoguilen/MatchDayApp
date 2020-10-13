using MatchDayApp.Domain.Entities;
using MatchDayApp.Domain.Specification.Base;
using System;

namespace MatchDayApp.Domain.Specification
{
    public class TeamIncludeOwnerUserSpecification : BaseSpecification<Team>
    {
        public TeamIncludeOwnerUserSpecification(string name)
            : base(t => t.Name.Contains(name))
        {
            AddInclude(t => t.OwnerUser);
        }

        public TeamIncludeOwnerUserSpecification(Guid id)
            : base(t => t.Id == id)
        {
            AddInclude(t => t.OwnerUser);
        }

        public TeamIncludeOwnerUserSpecification()
            : base(null)
        {
            AddInclude(t => t.OwnerUser);
        }
    }
}
