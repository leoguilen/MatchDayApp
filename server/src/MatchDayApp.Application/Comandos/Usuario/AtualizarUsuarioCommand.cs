using MatchDayApp.Application.Models;
using MediatR;
using System;

namespace MatchDayApp.Application.Comandos.Usuario
{
    public class AtualizarUsuarioCommand : IRequest<UsuarioModel>
    {
        public Guid UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}
