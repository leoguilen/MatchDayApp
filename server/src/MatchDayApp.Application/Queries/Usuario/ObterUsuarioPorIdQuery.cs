using MatchDayApp.Application.Models;
using MediatR;
using System;

namespace MatchDayApp.Application.Queries.Usuario
{
    public class ObterUsuarioPorIdQuery : IRequest<UsuarioModel>
    {
        public Guid Id { get; set; }
    }
}
