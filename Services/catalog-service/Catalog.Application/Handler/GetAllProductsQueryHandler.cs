

using Catalog.Application.Dto;
using Catalog.Application.Queries;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Handler
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
    {
        public readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var pro = await _productRepository.FindAllAsync(cancellationToken);
            return pro.Select(x => new ProductDto
            {
                Id = x.Id,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.Name,
                CreateAt = x.CreateAt,
                Currency = x.Price.Currency,
                Price = x.Price.Amount,
                LongDescription = x.Description.LongDescription,
                ShortDescription = x.Description.ShorDescription,
                Name = x.Name,
                Sku = x.Sku.Value
            });
        }
    }
}
