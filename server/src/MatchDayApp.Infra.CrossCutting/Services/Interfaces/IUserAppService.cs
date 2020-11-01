using MatchDayApp.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.CrossCutting.Services.Interfaces
{
    public interface IUserAppService
    {
        Task<IReadOnlyList<UserModel>> GetUsersListAsync();
        Task<UserModel> GetUserByIdAsync(Guid userId);
        Task<UserModel> GetUserByEmailAsync(string userEmail);
        Task<bool> UpdateUserAsync(UserModel user);
        Task<bool> DeleteUserAsync(Guid userId);
    }
}
