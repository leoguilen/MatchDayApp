using MatchDayApp.Domain.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchDayApp.Domain.Entities
{
    public class Team : Entity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int TotalPlayers { get; set; }
        public Guid OwnerUserId { get; set; }

        [ForeignKey(nameof(OwnerUserId))]
        public virtual User OwnerUser { get; set; }
    }
}
