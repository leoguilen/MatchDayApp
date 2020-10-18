using MatchDayApp.Domain.Entities;
using MatchDayApp.Domain.Specification.Base;
using System;

namespace MatchDayApp.Domain.Specification.SoccerCourtSpec
{
    public class SoccerCourtWithUserSpecification : BaseSpecification<SoccerCourt>
    {
        public SoccerCourtWithUserSpecification(Guid ownerUserId)
            : base(sc => sc.OwnerUserId == ownerUserId)
        {
            AddInclude(sc => sc.OwnerUser);
        }

        public SoccerCourtWithUserSpecification(string soccerCourtName)
            : base(sc => sc.Name.Contains(soccerCourtName))
        {
            AddInclude(sc => sc.OwnerUser);
        }

        public SoccerCourtWithUserSpecification()
            : base(null)
        {
            AddInclude(sc => sc.OwnerUser);
        }
    }
}
