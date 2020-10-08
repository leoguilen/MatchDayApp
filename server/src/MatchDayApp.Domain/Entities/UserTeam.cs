using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchDayApp.Domain.Entities
{
    public class UserTeam
    {
        protected UserTeam()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public Guid UserId { get; set; }
        public Guid TeamId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        [ForeignKey(nameof(TeamId))]
        public virtual Team Team { get; set; }
    }
}
