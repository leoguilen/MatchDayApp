using AutoMapper;
using MatchDayApp.Application.Comandos.Time;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Queries.Time;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Query;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Time;
using MatchDayApp.Infra.CrossCutting.Servicos.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.CrossCutting.Servicos
{
    public class TimeAppServico : ITimeAppServico
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TimeAppServico(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<TimeModel> AdicionarTimeAsync(CriarTimeRequest request)
        {
            var adicionarTimeCommand = new AdicionarTimeCommand
            {
                Time = _mapper
                    .Map<TimeModel>(request)
            };

            var result = await _mediator
                .Send(adicionarTimeCommand);

            return result;
        }

        public async Task<TimeModel> AtualizarTimeAsync(Guid timeId, AtualizarTimeRequest request)
        {
            var atualizarTimeCommand = new AtualizarTimeCommand
            {
                Id = timeId,
                Time = _mapper
                    .Map<TimeModel>(request)
            };

            var result = await _mediator
                .Send(atualizarTimeCommand);

            return result;
        }

        public async Task<bool> DeletarTimeAsync(Guid timeId)
        {
            var deletarTimeCommand = new DeletarTimeCommand
            {
                Id = timeId
            };

            var result = await _mediator
                .Send(deletarTimeCommand);

            return result;
        }

        public async Task<TimeModel> ObterTimePorIdAsync(Guid timeId)
        {
            var obterTimePorIdQuery = new ObterTimePorIdQuery
            {
                Id = timeId
            };

            var time = await _mediator
                .Send(obterTimePorIdQuery);

            return time;
        }

        public async Task<IReadOnlyList<TimeModel>> ObterTimesAsync(PaginacaoQuery pagination = null)
        {
            var obterTimesQuery = new ObterTimesQuery { };

            var times = await _mediator
                .Send(obterTimesQuery);

            var skip = (pagination.NumeroPagina - 1) * pagination.QuantidadePagina;

            return times
                .Skip(skip)
                .Take(pagination.QuantidadePagina)
                .ToList();
        }
    }
}
