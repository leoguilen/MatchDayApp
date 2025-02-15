﻿using MatchDayApp.Domain.Entidades;
using System;
using System.Threading.Tasks;

namespace MatchDayApp.Domain.Repositorios
{
    public interface IConfirmacaoEmailRepositorio
    {
        Task<Guid> AdicionarRequisicaoAsync(Guid usuarioId);
        bool AtualizarRequisicao(ConfirmacaoEmail confirmacaoEmail);
        Task<ConfirmacaoEmail> ObterRequisicaoPorChaveAsync(Guid chave);
    }
}
