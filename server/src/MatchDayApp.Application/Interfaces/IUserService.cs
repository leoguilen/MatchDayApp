using MatchDayApp.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Interfaces
{
    public interface IUserService
    {
        Task<IReadOnlyList<UserModel>> GetUsersListAsync();
        Task<UserModel> GetUserByIdAsync(Guid userId);
        Task<UserModel> GetUserByEmailAsync(string userEmail);
        Task<bool> UpdateUserAsync(Guid userId, UserModel user);
        Task<bool> DeleteUserAsync(Guid userId);
    }
}
