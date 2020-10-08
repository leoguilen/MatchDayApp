using System;

namespace MatchDayApp.Domain.Entities
{
    public class ScheduledMatch
    {
        protected ScheduledMatch()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public Guid FirstTeamId { get; set; }
        public Guid SecondTeamId { get; set; }
        public Guid SoccerCourtId { get; set; }
        public int MatchTime { get; set; } = 1;
        public DateTime MatchDate { get; set; }
    }
}
