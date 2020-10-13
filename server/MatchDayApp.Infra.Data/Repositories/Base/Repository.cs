using MatchDayApp.Domain.Entities.Base;
using MatchDayApp.Domain.Repository.Base;
using MatchDayApp.Infra.Data.Data;
using System;

namespace MatchDayApp.Infra.Data.Repositories.Base
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
