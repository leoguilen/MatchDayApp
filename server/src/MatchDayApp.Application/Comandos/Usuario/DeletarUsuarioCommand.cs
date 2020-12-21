using MediatR;
using System;

namespace MatchDayApp.Application.Comandos.Usuario
{
    public class DeletarUsuarioCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
