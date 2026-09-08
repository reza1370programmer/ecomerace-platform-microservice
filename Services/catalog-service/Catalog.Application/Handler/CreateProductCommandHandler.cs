

using Catalog.Application.Commands;
using Catalog.Domain.Aggregates;
using Catalog.Domain.Repositories;
using Catalog.Domain.ValueObject;
using Domain.ValueObjects;
using MediatR;

namespace Catalog.Application.Handler
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        public readonly IProductRepository _productRepository;
        public readonly ICategoryRepository _categoryRepository;
        public IMediator _mediator { get; set; }


        public CreateProductCommandHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IMediator mediator)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindByIdAsync(request.CategoryId, cancellationToken);
            if (category == null) throw new ArgumentException($"category with id {request.CategoryId} is null");
            var product = await _productRepository.FindBySKUAsync(request.Sku, cancellationToken);
            if (product != null) throw new ArgumentException($"the product with sku {request.Sku} is already exists");
            var productAgg = ProductAggregate.CreateProduct(request.Name, Money.Create(request.Price, request.Currency), ProductDescription.Create(request.ShortDescription, request.LongDescription), SKU.Create(request.Sku), request.CategoryId);
            foreach (var _domainEvent in productAgg.DomainEvents)
            {
                if(_domainEvent is INotification notification)
                {
                    await _mediator.Publish(_domainEvent, cancellationToken);
                }

            }
            productAgg.ClearDomainEvents();
            await _productRepository.AddAsync(productAgg.Product);
            return productAgg.Product.Id;
        }
    }
}
