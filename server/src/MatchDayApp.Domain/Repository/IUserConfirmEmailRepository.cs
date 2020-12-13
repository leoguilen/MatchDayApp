using MatchDayApp.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace MatchDayApp.Domain.Repository
{
    public interface IUserConfirmEmailRepository
    {
        Task<bool> AddRequestAsync(Guid userId);
        bool UpdateRequest(UserConfirmEmail confirmEmail);
        Task<UserConfirmEmail> GetRequestByKeyAsync(Guid key);
    }
}
