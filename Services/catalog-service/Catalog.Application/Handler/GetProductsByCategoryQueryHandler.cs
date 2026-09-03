

using Catalog.Application.Dto;
using Catalog.Application.Queries;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Handler
{
    public class GetProductsByCategoryQueryHandler : IRequestHandler<GetProductsByCategoryQuery, IEnumerable<ProductDto>>
    {
        public readonly IProductRepository _productRepository;

        public GetProductsByCategoryQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var pro = await _productRepository.FindByCategoryId(request.CategoryId);
            return pro.Select(x => new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.Name,
                Sku = x.Sku.Value,
                Price = x.Price.Amount,
                Currency = x.Price.Currency,
                CreateAt = x.CreateAt,
                LongDescription = x.Description.LongDescription,
                ShortDescription = x.Description.ShorDescription
            });
        }
    }
}
