namespace MatchDayApp.Domain.Entities.Enum
{
    public enum MatchStatus : int
    {
        Confirmed = 1,
        Canceled = 2,
        WaitingForConfirmation = 3,
        Finished = 4
    }
}
