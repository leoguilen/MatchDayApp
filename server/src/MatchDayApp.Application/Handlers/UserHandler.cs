using MatchDayApp.Application.Commands.User;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Queries.User;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Handlers
{
    public class UserHandler :
        IRequestHandler<DeleteUserCommand, bool>,
        IRequestHandler<UpdateUserCommand, bool>,
        IRequestHandler<GetUserDetailsByEmailQuery, UserModel>,
        IRequestHandler<GetUserDetailsByIdQuery, UserModel>,
        IRequestHandler<GetUsersQuery, IReadOnlyList<UserModel>>
    {
        private readonly IUserService _userService;

        public UserHandler(IUserService userService)
        {
            _userService = userService ?? throw new System.ArgumentNullException(nameof(userService));
        }

        public async Task<IReadOnlyList<UserModel>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetUsersListAsync();
        }

        public async Task<UserModel> Handle(GetUserDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetUserByIdAsync(request.Id);
        }

        public async Task<UserModel> Handle(GetUserDetailsByEmailQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetUserByEmailAsync(request.Email);
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.UpdateUserAsync(request.UpdateUser);
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.DeleteUserAsync(request.Id);
        }
    }
}
