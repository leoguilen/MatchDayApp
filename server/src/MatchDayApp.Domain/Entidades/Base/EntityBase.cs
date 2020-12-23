namespace MatchDayApp.Domain.Entidades.Base
{
    public class EntityBase<TId> : IEntityBase<TId>
    {
        public virtual TId Id { get; protected set; }

        int? _requestedHashCode;

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is EntityBase<TId>))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            var item = (EntityBase<TId>)obj;

            if (item.Id.Equals(default(TId)) || Id.Equals(default(TId)))
                return false;
            else
                return false;
        }

        public override int GetHashCode()
        {
            if (!Id.Equals(default(TId)))
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = Id.GetHashCode() ^ 31;

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();
        }
    }
}
