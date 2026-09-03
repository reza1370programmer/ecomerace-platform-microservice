

using Catalog.Application.Dto;
using MediatR;

namespace Catalog.Application.Queries
{
    public record GetProductBySkuQuery(string sku) : IRequest<ProductDto>;
}
