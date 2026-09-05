using Catalog.Application.Dto;
using MediatR;


namespace Catalog.Application.Queries
{
    public record GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>;
}
