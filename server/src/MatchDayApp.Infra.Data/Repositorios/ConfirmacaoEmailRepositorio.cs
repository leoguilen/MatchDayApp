using MatchDayApp.Domain.Entidades;
using MatchDayApp.Domain.Repositorios;
using MatchDayApp.Infra.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.Data.Repositorios
{
    public class ConfirmacaoEmailRepositorio : IConfirmacaoEmailRepositorio
    {
        private readonly MatchDayAppContext _context;

        public ConfirmacaoEmailRepositorio(MatchDayAppContext context)
            => _context = context;

        public async Task<Guid> AdicionarRequisicaoAsync(Guid usuarioId)
        {
            var confirmacaoEmail = new ConfirmacaoEmail
            {
                Id = Guid.NewGuid(),
                RequisicaoEm = DateTime.Now,
                UsuarioId = usuarioId,
                ChaveConfirmacao = Guid.NewGuid()
            };

            var result = await _context.ConfirmacaoEmails
                .AddAsync(confirmacaoEmail);

            var saved = _context.SaveChanges();

            if(saved > 0)
                return confirmacaoEmail.ChaveConfirmacao;

            return Guid.Empty;
        }

        public bool AtualizarRequisicao(ConfirmacaoEmail confirmacaoEmail)
        {
            confirmacaoEmail.Confirmado = true;

            var result = _context.ConfirmacaoEmails
                .Update(confirmacaoEmail);

            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public async Task<ConfirmacaoEmail> ObterRequisicaoPorChaveAsync(Guid chave)
        {
            return await _context.ConfirmacaoEmails
                .SingleOrDefaultAsync(x => x.ChaveConfirmacao == chave && x.Confirmado == false);
        }
    }
}
