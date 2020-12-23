using AutoMapper;
using MatchDayApp.Application.Comandos.Partida;
using MatchDayApp.Application.Events.Quadra;
using MatchDayApp.Application.Events.Time;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Queries.Partida;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Partida;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Query;
using MatchDayApp.Infra.CrossCutting.Servicos.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.CrossCutting.Servicos
{
    public class PartidaAppServico : IPartidaAppServico
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PartidaAppServico(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator
                ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> ConfirmarPartidaAsync(ConfirmarPartidaRequest request)
        {
            var confirmarPartidaCommand = new ConfirmarPartidaCommand
            {
                TimeId = request.TimeId,
                PartidaId = request.PartidaId
            };

            var result = await _mediator
                .Send(confirmarPartidaCommand);

            return result;
        }

        public async Task<PartidaModel> DesmarcarPartidaAsync(Guid partidaId)
        {
            var desmarcarPartidaCommand = new DesmarcarPartidaCommand
            {
                PartidaId = partidaId
            };

            var result = await _mediator
                .Send(desmarcarPartidaCommand);

            await _mediator.Publish(new TimePartidaCanceladaEvent { PartidaId = partidaId });
            await _mediator.Publish(new QuadraPartidaCanceladaEvent { PartidaId = partidaId });

            return result;
        }

        public async Task<PartidaModel> MarcarPartidaAsync(MarcarPartidaRequest request)
        {
            var partidaModel = _mapper
                    .Map<PartidaModel>(request);

            var marcarPartidaCommand = new MarcarPartidaCommand
            {
                Partida = partidaModel
            };

            var result = await _mediator
                .Send(marcarPartidaCommand);

            await _mediator.Publish(new TimePartidaMarcadaEvent { Partida = partidaModel });
            await _mediator.Publish(new QuadraPartidaMarcadaEvent { Partida = partidaModel });

            return result;
        }

        public async Task<PartidaModel> ObterPartidaPorIdAsync(Guid partidaId)
        {
            var obterPartidaPorIdQuery = new ObterPartidaPorIdQuery
            {
                PartidaId = partidaId
            };

            var partida = await _mediator
                .Send(obterPartidaPorIdQuery);

            return partida;
        }

        public async Task<IEnumerable<PartidaModel>> ObterPartidaPorQuadraIdAsync(Guid quadraId)
        {
            var obterPartidasPorQuadraIdQuery = new ObterPartidasPorQuadraIdQuery
            {
                QuadraId = quadraId
            };

            var partidas = await _mediator
                .Send(obterPartidasPorQuadraIdQuery);

            return partidas;
        }

        public async Task<IEnumerable<PartidaModel>> ObterPartidaPorTimeIdAsync(Guid timeId)
        {
            var obterPartidasPorTimeIdQuery = new ObterPartidasPorTimeIdQuery
            {
                TimeId = timeId
            };

            var partidas = await _mediator
                .Send(obterPartidasPorTimeIdQuery);

            return partidas;
        }

        public async Task<IReadOnlyList<PartidaModel>> ObterPartidasAsync(PaginacaoQuery pagination = null, PartidaFilterQuery filter = null)
        {
            var obterPartidasQuery = new ObterPartidasQuery { };

            var partidas = await _mediator
                .Send(obterPartidasQuery);

            var skip = (pagination.NumeroPagina - 1) * pagination.QuantidadePagina;

            if (!(filter is null))
            {
                return partidas
                    .Where(m => m.QuadraFutebolId == filter.QuadraId)
                    .Where(m => m.PrimeiroTimeId == filter.TimeId || m.SegundoTimeId == filter.TimeId)
                    .Skip(skip)
                    .Take(pagination.QuantidadePagina)
                    .ToList();
            }

            return partidas
                    .Skip(skip)
                    .Take(pagination.QuantidadePagina)
                    .ToList();
        }
    }
}
