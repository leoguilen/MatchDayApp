using MatchDayApp.Domain.Entidades.Base;
using System;

namespace MatchDayApp.Domain.Repositorios.Base
{
    public interface IRepository<T> : IRepositoryBase<T, Guid>
        where T : IEntityBase<Guid>
    {
    }
}
