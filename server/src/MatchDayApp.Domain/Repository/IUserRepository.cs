using MatchDayApp.Domain.Entities;
using MatchDayApp.Domain.Repository.Base;
using System;
using System.Threading.Tasks;

namespace MatchDayApp.Domain.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<bool> AddRequestConfirmEmailAsync(Guid userId);
    }
}
