namespace MatchDayApp.Domain.Entidades.Base
{
    public interface IEntityBase<TId>
    {
        TId Id { get; }
    }
}
