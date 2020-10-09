using System;

namespace MatchDayApp.Domain.Entities
{
    public class ScheduleMatch
    {
        protected ScheduleMatch()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public Guid FirstTeamId { get; set; }
        public bool FistTeamConfirmed { get; set; }
        public Guid SecondTeamId { get; set; }
        public bool SecondTeamConfirmed { get; set; }
        public Guid SoccerCourtId { get; set; }
        public int MatchTime { get; set; }
        public DateTime MatchDate { get; set; }
    }
}
