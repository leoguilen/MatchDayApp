using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchDayApp.Domain.Entities
{
    public class UserConfirmEmail
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime RequestedAt { get; set; }
        public Guid UserId { get; set; }
        public Guid ConfirmKey { get; set; }
        public bool Confirmed { get; set; } = false;

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; private set; }
    }
}
