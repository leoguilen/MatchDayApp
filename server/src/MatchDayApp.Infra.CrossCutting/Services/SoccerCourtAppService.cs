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

        public SoccerCourtAppService(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> AddSoccerCourtAsync(SoccerCourtModel soccerCourt)
        {
            var addSoccerCourtCommand = new AddSoccerCourtCommand
            {
                SoccerCourt = soccerCourt
            };

            var result = await _mediator.Send(addSoccerCourtCommand);

            return result;
        }

        public async Task<bool> DeleteSoccerCourtAsync(Guid soccerCourtId)
        {
            var deleteSoccerCourtCommand = new DeleteSoccerCourtCommand
            {
                Id = soccerCourtId
            };

            var result = await _mediator.Send(deleteSoccerCourtCommand);

            return result;
        }

        public async Task<SoccerCourtModel> GetSoccerCourtByIdAsync(Guid soccerCourtId)
        {
            var getSoccerCourtByIdQuery = new GetSoccerCourtDetailsByIdQuery
            {
                Id = soccerCourtId
            };

            var soccerCourt = await _mediator.Send(getSoccerCourtByIdQuery);

            return soccerCourt;
        }

        public async Task<IReadOnlyList<SoccerCourtModel>> GetSoccerCourtsByGeoLocalizationAsync(GetSoccerCourtsByGeoRequest request)
        {
            var getSoccerCourtsByGeoQuery = new GetSoccerCourtsByGeoLocalizationQuery
            {
                Lat = request.Lat,
                Long = request.Long
            };

            var soccerCourts = await _mediator.Send(getSoccerCourtsByGeoQuery);

            return soccerCourts;
        }

        public async Task<IReadOnlyList<SoccerCourtModel>> GetSoccerCourtsListAsync(PaginationQuery pagination = null)
        {
            var getSoccerCourtsQuery = new GetSoccerCourtsQuery { };

            var soccerCourts = await _mediator.Send(getSoccerCourtsQuery);

            var skip = (pagination.PageNumber - 1) * pagination.PageSize;
            
            return soccerCourts
                .Skip(skip)
                .Take(pagination.PageSize)
                .ToList();
        }

        public async Task<bool> UpdateSoccerCourtAsync(Guid soccerCourtId, SoccerCourtModel soccerCourt)
        {
            var updateSoccerCourtCommand = new UpdateSoccerCourtCommand
            {
                Id = soccerCourtId,
                SoccerCourt = soccerCourt
            };

            var result = await _mediator.Send(updateSoccerCourtCommand);

            return result;
        }
    }
}
