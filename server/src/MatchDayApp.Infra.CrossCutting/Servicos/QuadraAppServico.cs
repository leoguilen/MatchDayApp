using AutoMapper;
using MatchDayApp.Application.Comandos.Quadra;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Queries.Quadra;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Quadra;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Query;
using MatchDayApp.Infra.CrossCutting.Servicos.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.CrossCutting.Servicos
{
    public class QuadraAppServico : IQuadraAppServico
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public QuadraAppServico(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<QuadraModel> AdicionarQuadraAsync(CriarQuadraRequest request)
        {
            var adicionadrQuadraCommand = new AdicionarQuadraCommand
            {
                Quadra = _mapper
                    .Map<QuadraModel>(request)
            };

            var result = await _mediator
                .Send(adicionadrQuadraCommand);

            return result;
        }

        public async Task<QuadraModel> AtualizarQuadraAsync(Guid quadraId, AtualizarQuadraRequest request)
        {
            var atualizarQuadraCommand = new AtualizarQuadraCommand
            {
                Id = quadraId,
                Quadra = _mapper
                    .Map<QuadraModel>(request)
            };

            var result = await _mediator
                .Send(atualizarQuadraCommand);

            return result;
        }

        public async Task<bool> DeletarQuadraAsync(Guid quadraId)
        {
            var deletarQuadraCommand = new DeletarQuadraCommand
            {
                Id = quadraId
            };

            var result = await _mediator
                .Send(deletarQuadraCommand);

            return result;
        }

        public async Task<QuadraModel> ObterQuadraPorIdAsync(Guid quadraId)
        {
            var obterQuadraPorIdQuery = new ObterQuadraPorIdQuery
            {
                Id = quadraId
            };

            var quadra = await _mediator
                .Send(obterQuadraPorIdQuery);

            return quadra;
        }

        public async Task<IReadOnlyList<QuadraModel>> ObterQuadrasAsync(PaginacaoQuery pagination = null)
        {
            var obterQuadrasQuery = new ObterQuadrasQuery { };

            var quadras = await _mediator
                .Send(obterQuadrasQuery);

            var skip = (pagination.NumeroPagina - 1) * pagination.QuantidadePagina;

            return quadras
                .Skip(skip)
                .Take(pagination.QuantidadePagina)
                .ToList();
        }

        public async Task<IReadOnlyList<QuadraModel>> ObterQuadrasPorLocalizacaoAsync(ObterQuadraPorLocalizacaoRequest request)
        {
            var obterQuadraPorLocalizacaoQuery = new ObterQuadraPorLocalizacaoQuery
            {
                Lat = request.Lat,
                Long = request.Long
            };

            var quadras = await _mediator
                .Send(obterQuadraPorLocalizacaoQuery);

            return quadras;
        }
    }
}
