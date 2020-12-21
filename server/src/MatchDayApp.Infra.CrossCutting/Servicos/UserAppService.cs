using AutoMapper;
using MatchDayApp.Application.Commands.User;
using MatchDayApp.Application.Events.UserEvents;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Queries.User;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Query;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.User;
using MatchDayApp.Infra.CrossCutting.Services.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.CrossCutting.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserAppService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var deleteUserCommand = new DeletarUsuarioCommand
            {
                Id = userId
            };

            var result = await _mediator.Send(deleteUserCommand);

            if (!result)
                return result;

            await _mediator.Publish(new UsuarioDeletadoEvent { Id = userId });

            return result;
        }

        public async Task<UserModel> GetUserByEmailAsync(string userEmail)
        {
            var getUserByEmailQuery = new ObterUsuarioPorEmailQuery
            {
                Email = userEmail
            };

            var user = await _mediator.Send(getUserByEmailQuery);

            return user;
        }

        public async Task<UserModel> GetUserByIdAsync(Guid userId)
        {
            var getUserByIdQuery = new ObterUsuarioPorIdQuery
            {
                Id = userId
            };

            var user = await _mediator.Send(getUserByIdQuery);

            return user;
        }

        public async Task<IReadOnlyList<UserModel>> GetUsersListAsync(PaginationQuery pagination = null)
        {
            var getUsersQuery = new ObterUsuariosQuery { };

            var users = await _mediator.Send(getUsersQuery);

            var skip = (pagination.PageNumber - 1) * pagination.PageSize;

            return users
                .Skip(skip)
                .Take(pagination.PageSize)
                .ToList();
        }

        public async Task<bool> UpdateUserAsync(Guid userId, AtualizarUsuarioRequest request)
        {
            var updateUserCommand = new AtualizarUsuarioCommand
            {
                UserId = userId,
                UpdateUser = _mapper
                    .Map<UserModel>(request)
            };

            var result = await _mediator.Send(updateUserCommand);

            return result;
        }
    }
}
