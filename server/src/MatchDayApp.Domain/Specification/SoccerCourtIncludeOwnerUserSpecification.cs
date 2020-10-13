using MatchDayApp.Domain.Entities;
using MatchDayApp.Domain.Specification.Base;
using System;

namespace MatchDayApp.Domain.Specification
{
    public class SoccerCourtIncludeOwnerUserSpecification : BaseSpecification<SoccerCourt>
    {
        public SoccerCourtIncludeOwnerUserSpecification(string name)
            : base(sc => sc.Name.Contains(name))
        {
            AddInclude(sc => sc.OwnerUser);
        }

        public SoccerCourtIncludeOwnerUserSpecification(Guid id) 
            : base(sc => sc.Id == id)
        {
            AddInclude(sc => sc.OwnerUser);
        }

        public SoccerCourtIncludeOwnerUserSpecification()
            : base(null)
        {
            AddInclude(sc => sc.OwnerUser);
        }
    }
}
