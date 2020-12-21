using MatchDayApp.Application.Comandos.Partida;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Queries.Partida;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Handlers
{
    public class PartidaHandler :
        IRequestHandler<ConfirmarPartidaCommand, bool>,
        IRequestHandler<MarcarPartidaCommand, PartidaModel>,
        IRequestHandler<DesmarcarPartidaCommand, PartidaModel>,
        IRequestHandler<ObterPartidaPorIdQuery, PartidaModel>,
        IRequestHandler<ObterPartidasPorQuadraIdQuery, IEnumerable<PartidaModel>>,
        IRequestHandler<ObterPartidasPorTimeIdQuery, IEnumerable<PartidaModel>>,
        IRequestHandler<ObterPartidasQuery, IReadOnlyList<PartidaModel>>
    {
        private readonly IPartidaServico _partidaServico;

        public PartidaHandler(IPartidaServico partidaServico)
        {
            _partidaServico = partidaServico
                ?? throw new System.ArgumentNullException(nameof(partidaServico));
        }

        public async Task<bool> Handle(ConfirmarPartidaCommand request, CancellationToken cancellationToken)
        {
            return await _partidaServico
                .ConfirmarPartidaAsync(request.TimeId, request.PartidaId);
        }

        public async Task<PartidaModel> Handle(MarcarPartidaCommand request, CancellationToken cancellationToken)
        {
            return await _partidaServico
                .MarcarPartidaAsync(request.Partida);
        }

        public async Task<PartidaModel> Handle(DesmarcarPartidaCommand request, CancellationToken cancellationToken)
        {
            return await _partidaServico
                .DesmarcarPartidaAsync(request.PartidaId);
        }

        public async Task<PartidaModel> Handle(ObterPartidaPorIdQuery request, CancellationToken cancellationToken)
        {
            return await _partidaServico
                .ObterPartidaPorIdAsync(request.PartidaId);
        }

        public async Task<IEnumerable<PartidaModel>> Handle(ObterPartidasPorQuadraIdQuery request, CancellationToken cancellationToken)
        {
            return await _partidaServico
                .ObterPartidaPorQuadraIdAsync(request.QuadraId);
        }

        public async Task<IEnumerable<PartidaModel>> Handle(ObterPartidasPorTimeIdQuery request, CancellationToken cancellationToken)
        {
            return await _partidaServico
                .ObterPartidaPorTimeIdAsync(request.TimeId);
        }

        public async Task<IReadOnlyList<PartidaModel>> Handle(ObterPartidasQuery request, CancellationToken cancellationToken)
        {
            return await _partidaServico
                .ObterPartidasAsync();
        }
    }
}
