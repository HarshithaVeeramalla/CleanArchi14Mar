namespace Domain.Common
{
    public abstract class BaseEntity : IEquatable<BaseEntity>
    {
        protected BaseEntity(Guid id)
        {
            Id = id;
        }

        public override bool Equals(object? obj)
        {
            return EvaluateEntityEquality(obj);
        }

        public bool Equals(BaseEntity? other)
        {
            return EvaluateEntityEquality(other);
        }

        private bool EvaluateEntityEquality(object? obj)
        {
            if (obj is null)
                return false;
            if (obj.GetType() != GetType())
                return false;
            if (obj is not BaseEntity entity)
                return false;

            return entity.Id == Id;
        }

        public Guid Id { get; private set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}