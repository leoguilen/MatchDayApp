using MatchDayApp.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Interfaces
{
    public interface IUserService
    {
        Task<IReadOnlyCollection<UserModel>> GetUsersListAsync();
        Task<UserModel> GetUserByIdAsync(Guid userId);
        Task<UserModel> GetUserByEmailAsync(string userEmail);
        Task<bool> UpdateUserAsync(UserModel user);
        Task<bool> DeleteUserAsync(Guid userId);
    }
}
