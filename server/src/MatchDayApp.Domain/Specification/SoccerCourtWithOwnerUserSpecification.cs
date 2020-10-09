using MatchDayApp.Domain.Entities;
using MatchDayApp.Domain.Specification.Base;
using System;

namespace MatchDayApp.Domain.Specification
{
    public class SoccerCourtWithOwnerUserSpecification : BaseSpecification<SoccerCourt>
    {
        public SoccerCourtWithOwnerUserSpecification(string name)
            : base(sc => sc.Name.Contains(name))
        {
            AddInclude(sc => sc.OwnerUser);
        }

        public SoccerCourtWithOwnerUserSpecification(Guid id) 
            : base(sc => sc.Id == id)
        {
            AddInclude(sc => sc.OwnerUser);
        }

        public SoccerCourtWithOwnerUserSpecification()
            : base(null)
        {
            AddInclude(sc => sc.OwnerUser);
        }
    }
}
