using MatchDayApp.Domain.Entidades;
using MatchDayApp.Domain.Repositorios.Base;
using System.Threading.Tasks;

namespace MatchDayApp.Domain.Repositorios
{
    public interface IUsuarioRepositorio : IRepository<Usuario>
    {
        Task<Usuario> ObterUsuarioPorEmailAsync(string email);
    }
}
