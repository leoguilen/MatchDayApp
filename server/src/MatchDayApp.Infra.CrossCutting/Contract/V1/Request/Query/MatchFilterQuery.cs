using System;

namespace MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Query
{
    public class MatchFilterQuery
    {
        public Guid SoccerCourtId { get; set; }
        public Guid TeamId { get; set; }
    }
}
