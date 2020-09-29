using System;

namespace MatchDayApp.Domain.Entities.Base
{
    public abstract class Entity : EntityBase<Guid>
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        public DateTime CreatedAt { get; protected set; }
    }
}
