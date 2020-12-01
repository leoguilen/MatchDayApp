namespace MatchDayApp.Infra.CrossCutting.Contract.V1.Request.SoccerCourt
{
    public class GetSoccerCourtsByGeoRequest
    {
        public double Lat { get; set; }
        public double Long { get; set; }
    }
}
