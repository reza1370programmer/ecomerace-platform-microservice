

using Catalog.Application.Dto;
using Catalog.Application.Queries;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Handler
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        public readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var pro = await _productRepository.FindByIdAsync(request.ProductId, cancellationToken);
            if (pro is null) return null;
            return new ProductDto()
            {
                Id = pro.Id,
                CategoryId = pro.CategoryId,
                CategoryName = pro.Category.Name,
                CreateAt = pro.CreateAt,
                Currency = pro.Price.Currency,
                Price = pro.Price.Amount,
                LongDescription = pro.Description.LongDescription,
                ShortDescription = pro.Description.ShorDescription,
                Name = pro.Name,
                Sku = pro.Sku.Value
            };
        }
    }
}
