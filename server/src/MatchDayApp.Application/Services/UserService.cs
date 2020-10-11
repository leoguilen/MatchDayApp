using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Services
{
    public class UserService : IUserService
    {
        public Task<bool> DeleteUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> GetUserByEmailAsync(string userEmail)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> GetUserByIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<UserModel>> GetUsersListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserAsync(UserModel user)
        {
            throw new NotImplementedException();
        }
    }
}
