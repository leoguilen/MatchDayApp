using AutoMapper;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var user = await _uow.Users
                .GetByIdAsync(userId);

            if (user != null)
            {
                user.Deleted = true;
                await _uow.Users.SaveAsync(user);
                return true;
            }

            return false;
        }

        public async Task<UserModel> GetUserByEmailAsync(string userEmail)
        {
            var user = await _uow.Users
                .GetByEmailAsync(userEmail);

            if (user != null)
            {
                return _mapper.Map<UserModel>(user);
            }

            return null;
        }

        public async Task<UserModel> GetUserByIdAsync(Guid userId)
        {
            var user = await _uow.Users
                .GetByIdAsync(userId);

            if (user != null)
            {
                return _mapper.Map<UserModel>(user);
            }

            return null;
        }

        public async Task<IReadOnlyList<UserModel>> GetUsersListAsync()
        {
            var users = await _uow.Users.ListAllAsync();
            return _mapper.Map<IReadOnlyList<UserModel>>(users);
        }

        public async Task<bool> UpdateUserAsync(UserModel userModel)
        {
            var user = await _uow.Users
                .GetByEmailAsync(userModel.Email);

            if (user != null)
            {
                user.FirstName = userModel.FirstName ?? user.FirstName;
                user.LastName = userModel.LastName ?? user.LastName;
                user.Email = userModel.Email ?? user.Email;
                user.Username = userModel.Username ?? user.Username;
                user.UserType = userModel.UserType;
                user.Avatar = userModel.Avatar ?? user.Avatar;

                await _uow.Users.SaveAsync(user);
                return true;
            }

            return false;
        }
    }
}
