

namespace Identity.Domain.ValueObjects
{
    public class UserId
    {
        public Guid Value { get; init; }

        private UserId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentNullException("value cannot be empty");
            }
            Value = value;
        }

        public static UserId Create(Guid value)
        {
            return new UserId(value);
        }
        public static UserId CreateNew()
        {
            return new UserId(Guid.NewGuid());
        }
        public override string ToString() => Value.ToString();
    }
}
