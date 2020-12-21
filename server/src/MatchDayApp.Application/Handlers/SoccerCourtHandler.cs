using MatchDayApp.Application.Commands.SoccerCourt;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Queries.SoccerCourt;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Handlers
{
    public class SoccerCourtHandler :
        IRequestHandler<AddSoccerCourtCommand, bool>,
        IRequestHandler<DeleteSoccerCourtCommand, bool>,
        IRequestHandler<UpdateSoccerCourtCommand, bool>,
        IRequestHandler<GetSoccerCourtDetailsByIdQuery, QuadraModel>,
        IRequestHandler<GetSoccerCourtsQuery, IReadOnlyList<QuadraModel>>,
        IRequestHandler<GetSoccerCourtsByGeoLocalizationQuery, IReadOnlyList<QuadraModel>>
    {
        private readonly IQuadraFutebolServico _soccerCourtService;

        public SoccerCourtHandler(IQuadraFutebolServico soccerCourtService)
        {
            _soccerCourtService = soccerCourtService ?? throw new System.ArgumentNullException(nameof(soccerCourtService));
        }

        public async Task<bool> Handle(AddSoccerCourtCommand request, CancellationToken cancellationToken)
        {
            return await _soccerCourtService.AddSoccerCourtAsync(request.SoccerCourt);
        }

        public async Task<bool> Handle(DeleteSoccerCourtCommand request, CancellationToken cancellationToken)
        {
            return await _soccerCourtService.DeleteSoccerCourtAsync(request.Id);
        }

        public async Task<bool> Handle(UpdateSoccerCourtCommand request, CancellationToken cancellationToken)
        {
            return await _soccerCourtService.UpdateSoccerCourtAsync(request.Id, request.SoccerCourt);
        }

        public async Task<QuadraModel> Handle(GetSoccerCourtDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            return await _soccerCourtService.GetSoccerCourtByIdAsync(request.Id);
        }

        public async Task<IReadOnlyList<QuadraModel>> Handle(GetSoccerCourtsQuery request, CancellationToken cancellationToken)
        {
            return await _soccerCourtService.GetSoccerCourtsListAsync();
        }

        public async Task<IReadOnlyList<QuadraModel>> Handle(GetSoccerCourtsByGeoLocalizationQuery request, CancellationToken cancellationToken)
        {
            return await _soccerCourtService.GetSoccerCourtsByGeoLocalizationAsync(request.Lat, request.Long);
        }
    }
}
