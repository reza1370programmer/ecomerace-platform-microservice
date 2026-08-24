

using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public record Email
    {
        private static readonly Regex EmailRegex = new Regex("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public string Value { get; init; }

        private Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException($"Email Value {value} is invalid");
            if (!EmailRegex.IsMatch(value)) throw new ArgumentException($"Email value {value} is invalid");
            Value = value.ToLowerInvariant();
        }
        public static Email Create(string value)
        {
            return new Email(value);
        }
        public override string ToString()
        {
            return Value;
        }

    }
}
