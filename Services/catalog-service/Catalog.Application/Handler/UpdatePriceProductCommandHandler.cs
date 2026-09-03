

using Catalog.Application.Commands;
using Catalog.Domain.Aggregates;
using Catalog.Domain.Repositories;
using Domain.ValueObjects;
using MediatR;

namespace Catalog.Application.Handler
{
    public class UpdatePriceProductCommandHandler : IRequestHandler<UpdatePriceProductCommand>
    {
        public readonly IProductRepository _productRepository;

        public UpdatePriceProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Handle(UpdatePriceProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindByIdAsync(request.ProductId);
            if (product == null) throw new ArgumentException($"the product with id {request.ProductId} not found");
            var productAgg = ProductAggregate.CreateProduct(product.Name, product.Price, product.Description, product.Sku, product.CategoryId);
            productAgg.UpdateProductPrice(Money.Create(request.NewPrice, request.currency));
            await _productRepository.UpdateAsync(productAgg.Product, cancellationToken);
        }
    }
}
