

namespace Identity.Domain.ValueObjects
{
    public class PasswordHash
    {
        public string Value { get; init; }


        private PasswordHash(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("value cannot be empty");
            Value = value;
        }

        public static PasswordHash Create(string value)
        {
            return new PasswordHash(value);
        }

        public override string ToString() { return Value; }
    }
}
