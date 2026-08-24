

using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public record PhoneNumber
    {
        private static readonly Regex PhoneRegex = new Regex(@"^09[0-9]{9}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public string Value { get; init; }

        private PhoneNumber(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException($"phone number {value} can not be empty");
            if (!PhoneRegex.IsMatch(value))
                throw new ArgumentException("Invalid phone number format", nameof(value));
            Value = value;
        }
        public static PhoneNumber Create(string value)
        {
            return new PhoneNumber(value);
        }

    }
}
