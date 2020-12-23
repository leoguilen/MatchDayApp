using System;

namespace MatchDayApp.Domain.Entidades.Base
{
    public abstract class Entity : EntityBase<Guid>
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
            CriadoEm = DateTime.Now;
        }

        public DateTime CriadoEm { get; protected set; }
    }
}
