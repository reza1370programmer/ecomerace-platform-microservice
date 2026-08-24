

using System.Text.RegularExpressions;

namespace Catalog.Domain.ValueObject
{
    public record SKU
    {
        private static readonly Regex Sku_Regex = new(@"^[A-Z0-9-_]+$", RegexOptions.Compiled);
        public string Value { get; init; }

        private SKU(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("SKU cannot be empty", nameof(value));
            if (!Sku_Regex.IsMatch(value))
                throw new ArgumentException("SKU format is not valid");
            Value = value;
        }
        public static SKU Create(string value)
        {
            return new SKU(value);
        }
    }
}
