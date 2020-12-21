using MatchDayApp.Application.Models;
using MediatR;
using System.Collections.Generic;

namespace MatchDayApp.Application.Queries.SoccerCourt
{
    public class GetSoccerCourtsByGeoLocalizationQuery : IRequest<IReadOnlyList<QuadraModel>>
    {
        public double Lat { get; set; }
        public double Long { get; set; }
    }
}
