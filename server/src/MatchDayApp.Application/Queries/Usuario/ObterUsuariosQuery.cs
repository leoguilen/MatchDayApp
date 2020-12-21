using MatchDayApp.Application.Models;
using MediatR;
using System.Collections.Generic;

namespace MatchDayApp.Application.Queries.Usuario
{
    public class ObterUsuariosQuery : IRequest<IReadOnlyList<UsuarioModel>>
    {
        public int NumPagina { get; set; }
        public int QtndPagina { get; set; }
    }
}
