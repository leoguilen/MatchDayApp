using MatchDayApp.Domain.Common.Helpers;
using MatchDayApp.Domain.Entities;
using MatchDayApp.Domain.Specification.Base;

namespace MatchDayApp.Domain.Specification.SoccerCourtSpec
{
    public class SoccerCourtNearbyToUserSpecification : BaseSpecification<SoccerCourt>
    {
        public SoccerCourtNearbyToUserSpecification(double lat, double lon, bool ordenatedByProximity = true)
            : base(sc => CalculateCoordenateDistance.IsNear(lat, lon, sc.Latitude, sc.Longitude))
        {
            if (ordenatedByProximity)
                ApplyOrderBy(sc => CalculateCoordenateDistance
                    .CalculeToMeters(lat, lon, sc.Latitude, sc.Longitude));

            AddInclude(sc => sc.OwnerUser);
        }

        public SoccerCourtNearbyToUserSpecification()
            : base(null)
        {
            AddInclude(sc => sc.OwnerUser);
        }
    }
}
