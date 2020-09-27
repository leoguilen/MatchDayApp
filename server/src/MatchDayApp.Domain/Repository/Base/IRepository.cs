using MatchDayApp.Domain.Entities.Base;
using System;

namespace MatchDayApp.Domain.Interfaces.Base
{
    public interface IRepository<T> : IRepositoryBase<T, Guid> where T : IEntityBase<Guid>
    {
    }
}
