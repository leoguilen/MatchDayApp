using MatchDayApp.Application.Comandos.Quadra;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Queries.Quadra;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Handlers
{
    public class QuadraHandler :
        IRequestHandler<AdicionarQuadraCommand, QuadraModel>,
        IRequestHandler<DeletarQuadraCommand, bool>,
        IRequestHandler<AtualizarQuadraCommand, QuadraModel>,
        IRequestHandler<ObterQuadraPorIdQuery, QuadraModel>,
        IRequestHandler<ObterQuadrasQuery, IReadOnlyList<QuadraModel>>,
        IRequestHandler<ObterQuadraPorLocalizacaoQuery, IReadOnlyList<QuadraModel>>
    {
        private readonly IQuadraFutebolServico _quadraServico;

        public QuadraHandler(IQuadraFutebolServico quadraServico)
        {
            _quadraServico = quadraServico
                ?? throw new System.ArgumentNullException(nameof(quadraServico));
        }

        public async Task<QuadraModel> Handle(AdicionarQuadraCommand request, CancellationToken cancellationToken)
        {
            return await _quadraServico.AdicionarQuadraAsync(request.Quadra);
        }

        public async Task<bool> Handle(DeletarQuadraCommand request, CancellationToken cancellationToken)
        {
            return await _quadraServico.DeletarQuadraAsync(request.Id);
        }

        public async Task<QuadraModel> Handle(AtualizarQuadraCommand request, CancellationToken cancellationToken)
        {
            return await _quadraServico.AtualizarQuadraAsync(request.Id, request.Quadra);
        }

        public async Task<QuadraModel> Handle(ObterQuadraPorIdQuery request, CancellationToken cancellationToken)
        {
            return await _quadraServico.ObterQuadraPorIdAsync(request.Id);
        }

        public async Task<IReadOnlyList<QuadraModel>> Handle(ObterQuadrasQuery request, CancellationToken cancellationToken)
        {
            return await _quadraServico.ObterQuadrasAsync();
        }

        public async Task<IReadOnlyList<QuadraModel>> Handle(ObterQuadraPorLocalizacaoQuery request, CancellationToken cancellationToken)
        {
            return await _quadraServico.ObterQuadrasPorLocalizacaoAsync(request.Lat, request.Long);
        }
    }
}
