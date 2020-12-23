using MatchDayApp.Application.Models;
using MediatR;

namespace MatchDayApp.Application.Queries.Usuario
{
    public class ObterUsuarioPorEmailQuery : IRequest<UsuarioModel>
    {
        public string Email { get; set; }
    }
}
