using AutoMapper;
using MatchDayApp.Application.Commands.SoccerCourt;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Queries.SoccerCourt;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Query;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.SoccerCourt;
using MatchDayApp.Infra.CrossCutting.Services.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.CrossCutting.Services
{
    public class SoccerCourtAppService : ISoccerCourtAppService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SoccerCourtAppService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> AddSoccerCourtAsync(CriarQuadraRequest request)
        {
            var addSoccerCourtCommand = new AdicionarQuadraCommand
            {
                SoccerCourt = _mapper
                    .Map<QuadraModel>(request)
            };

            var result = await _mediator.Send(addSoccerCourtCommand);

            return result;
        }

        public async Task<bool> DeleteSoccerCourtAsync(Guid soccerCourtId)
        {
            var deleteSoccerCourtCommand = new DeletarQuadraCommand
            {
                Id = soccerCourtId
            };

            var result = await _mediator.Send(deleteSoccerCourtCommand);

            return result;
        }

        public async Task<QuadraModel> GetSoccerCourtByIdAsync(Guid soccerCourtId)
        {
            var getSoccerCourtByIdQuery = new GetSoccerCourtDetailsByIdQuery
            {
                Id = soccerCourtId
            };

            var soccerCourt = await _mediator.Send(getSoccerCourtByIdQuery);

            return soccerCourt;
        }

        public async Task<IReadOnlyList<QuadraModel>> GetSoccerCourtsByGeoLocalizationAsync(ObterQuadraPorLocalizacaoRequest request)
        {
            var getSoccerCourtsByGeoQuery = new GetSoccerCourtsByGeoLocalizationQuery
            {
                Lat = request.Lat,
                Long = request.Long
            };

            var soccerCourts = await _mediator.Send(getSoccerCourtsByGeoQuery);

            return soccerCourts;
        }

        public async Task<IReadOnlyList<QuadraModel>> GetSoccerCourtsListAsync(PaginationQuery pagination = null)
        {
            var getSoccerCourtsQuery = new GetSoccerCourtsQuery { };

            var soccerCourts = await _mediator.Send(getSoccerCourtsQuery);

            var skip = (pagination.PageNumber - 1) * pagination.PageSize;

            return soccerCourts
                .Skip(skip)
                .Take(pagination.PageSize)
                .ToList();
        }

        public async Task<bool> UpdateSoccerCourtAsync(Guid soccerCourtId, AtualizarQuadraRequest request)
        {
            var updateSoccerCourtCommand = new AtualizarQuadraCommand
            {
                Id = soccerCourtId,
                SoccerCourt = _mapper
                    .Map<QuadraModel>(request)
            };

            var result = await _mediator.Send(updateSoccerCourtCommand);

            return result;
        }
    }
}
