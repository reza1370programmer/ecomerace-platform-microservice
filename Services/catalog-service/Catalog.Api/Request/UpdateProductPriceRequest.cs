namespace Catalog.Api.Request
{
    public class UpdateProductPriceRequest
    {
        public decimal NewPrice { get; set; }
        public string  Currency { get; init; }

    }
}
