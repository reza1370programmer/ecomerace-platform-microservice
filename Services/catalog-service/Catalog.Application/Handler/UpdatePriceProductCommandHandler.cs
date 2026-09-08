

using Catalog.Application.Commands;
using Catalog.Application.Interfaces;
using Catalog.Domain.Aggregates;
using Catalog.Domain.Repositories;
using Domain.ValueObjects;
using MediatR;
using Messaging.Contracts;

namespace Catalog.Application.Handler
{
    public class UpdatePriceProductCommandHandler : IRequestHandler<UpdatePriceProductCommand>
    {
        public readonly IProductRepository _productRepository;
        public readonly IEventBusService _busService;

        public UpdatePriceProductCommandHandler(IProductRepository productRepository, IEventBusService busService)
        {
            _productRepository = productRepository;
            _busService = busService;
        }

        public async Task Handle(UpdatePriceProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindByIdAsync(request.ProductId);
            if (product == null) throw new ArgumentException($"the product with id {request.ProductId} not found");
            var productAgg = ProductAggregate.CreateProduct(product.Name, product.Price, product.Description, product.Sku, product.CategoryId);
            var oldPrice = productAgg.Product.Price;
            productAgg.UpdateProductPrice(Money.Create(request.NewPrice, request.currency));
            await _productRepository.UpdateAsync(productAgg.Product, cancellationToken);
            await _busService.PublishAsync(new ProductPriceUpdateIntegrationEvent()
            {
                NewPrice = request.NewPrice,
                OldPrice = oldPrice.Amount,
                ProductId = request.ProductId,
                UpdateAt = (DateTime)productAgg.Product.UpdateAt!
            }, cancellationToken);
        }
    }
}
