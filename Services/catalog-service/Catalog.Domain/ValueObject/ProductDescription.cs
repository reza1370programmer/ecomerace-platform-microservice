

namespace Catalog.Domain.ValueObject
{
    public record ProductDescription
    {
        public string ShorDescription { get; init; }
        public string? LongDescription { get; init; }

        public ProductDescription(string shorDescription, string? longDescription)
        {
            if (string.IsNullOrEmpty(shorDescription))
                throw new ArgumentNullException("shorDescription cannot be empty");
            if (shorDescription.Length > 200)
                throw new ArgumentException("shortDescription cannot be more than 200 characters");
            if (!string.IsNullOrEmpty(longDescription) && longDescription.Length > 2000)
                throw new ArgumentException("longDescription cannot be more than 2000 characters");

            ShorDescription = shorDescription;
            LongDescription = longDescription;
        }
        public static ProductDescription Create(string shorDescription, string longDescription)
        {
            return new ProductDescription(shorDescription, longDescription);
        }
    }
}
