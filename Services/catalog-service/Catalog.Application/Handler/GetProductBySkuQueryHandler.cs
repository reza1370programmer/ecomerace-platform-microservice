using Catalog.Application.Dto;
using Catalog.Application.Queries;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Handler
{
    public class GetProductBySkuQueryHandler : IRequestHandler<GetProductBySkuQuery, ProductDto>
    {
        public readonly IProductRepository _productRepository;

        public GetProductBySkuQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto> Handle(GetProductBySkuQuery request, CancellationToken cancellationToken)
        {
            var pro = await _productRepository.FindBySKUAsync(request.sku);
            if (pro == null) throw new ArgumentNullException("the product is null");
            return new ProductDto
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
