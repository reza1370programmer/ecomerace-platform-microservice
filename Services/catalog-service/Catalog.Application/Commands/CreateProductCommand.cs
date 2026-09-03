

using MediatR;

namespace Catalog.Application.Commands
{
    public record CreateProductCommand(
        string Name,
        decimal Price,
        string Currency,
        string ShortDescription,
        string? LongDescription,
        string Sku,
        Guid CategoryId
        ) : IRequest<Guid>;
}
