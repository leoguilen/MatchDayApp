using MatchDayApp.Application.Comandos.Time;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Queries.Time;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Handlers
{
    public class TimeHandler :
        IRequestHandler<AdicionarTimeCommand, TimeModel>,
        IRequestHandler<DeletarTimeCommand, bool>,
        IRequestHandler<AtualizarTimeCommand, TimeModel>,
        IRequestHandler<ObterTimePorIdQuery, TimeModel>,
        IRequestHandler<ObterTimesQuery, IReadOnlyList<TimeModel>>
    {
        private readonly ITimeServico _timeServico;

        public TimeHandler(ITimeServico timeServico)
        {
            _timeServico = timeServico
                ?? throw new System.ArgumentNullException(nameof(timeServico));
        }

        public async Task<TimeModel> Handle(AdicionarTimeCommand request, CancellationToken cancellationToken)
        {
            return await _timeServico.AdicionarTimeAsync(request.Time);
        }

        public async Task<bool> Handle(DeletarTimeCommand request, CancellationToken cancellationToken)
        {
            return await _timeServico.DeletarTimeAsync(request.Id);
        }

        public async Task<TimeModel> Handle(AtualizarTimeCommand request, CancellationToken cancellationToken)
        {
            return await _timeServico.AtualizarTimeAsync(request.Id, request.Time);
        }

        public async Task<TimeModel> Handle(ObterTimePorIdQuery request, CancellationToken cancellationToken)
        {
            return await _timeServico.ObterTimePorIdAsync(request.Id);
        }

        public async Task<IReadOnlyList<TimeModel>> Handle(ObterTimesQuery request, CancellationToken cancellationToken)
        {
            return await _timeServico.ObterTimesAsync();
        }
    }
}
