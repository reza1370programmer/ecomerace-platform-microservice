

using Catalog.Application.Dto;
using MediatR;

namespace Catalog.Application.Queries
{
    public record GetProductsByCategoryQuery(Guid CategoryId):IRequest<IEnumerable<ProductDto>>;
    {
    }
}
