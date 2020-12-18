using MatchDayApp.Domain.Entidades.Base;
using MatchDayApp.Domain.Repositorios.Base;
using MatchDayApp.Infra.Data.Data;
using System;

namespace MatchDayApp.Infra.Data.Repositorios.Base
{
    public class Repository<T> : RepositoryBase<T, Guid>, IRepository<T>
        where T : class, IEntityBase<Guid>
    {
        public Repository(MatchDayAppContext context)
            : base(context)
        {
        }
    }
}
