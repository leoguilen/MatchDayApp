using MatchDayApp.Domain.Entities;
using MatchDayApp.Domain.Specification.Base;
using System;

namespace MatchDayApp.Domain.Specification
{
    public class TeamWithOwnerUserSpecification : BaseSpecification<Team>
    {
        public TeamWithOwnerUserSpecification(string name)
            : base(t => t.Name.Contains(name))
        {
            AddInclude(t => t.OwnerUser);
        }

        public TeamWithOwnerUserSpecification(Guid id)
            : base(t => t.Id == id)
        {
            AddInclude(t => t.OwnerUser);
        }

        public TeamWithOwnerUserSpecification()
            : base(null)
        {
            AddInclude(t => t.OwnerUser);
        }
    }
}
