

using Catalog.Application.Dto;
using Catalog.Application.Queries;
using MediatR;

namespace Catalog.Application.Handler
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        public Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
