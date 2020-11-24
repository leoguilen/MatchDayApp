using MatchDayApp.Application.Models;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Query;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.CrossCutting.Services.Interfaces
{
    public interface IUserAppService
    {
        Task<IReadOnlyList<UserModel>> GetUsersListAsync(PaginationQuery pagination = null);
        Task<UserModel> GetUserByIdAsync(Guid userId);
        Task<UserModel> GetUserByEmailAsync(string userEmail);
        Task<bool> UpdateUserAsync(Guid userId, UpdateUserRequest request);
        Task<bool> DeleteUserAsync(Guid userId);
    }
}
