namespace MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Query
{
    public class PaginationQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 100;
    }
}
