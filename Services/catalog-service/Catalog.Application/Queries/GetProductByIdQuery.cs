

using Catalog.Application.Dto;
using MediatR;

namespace Catalog.Application.Queries
{
    public record GetProductByIdQuery(Guid ProductId) : IRequest<ProductDto>;
}
