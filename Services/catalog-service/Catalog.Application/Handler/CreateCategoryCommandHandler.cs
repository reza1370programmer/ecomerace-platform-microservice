

using Catalog.Application.Commands;
using Catalog.Domain.Entity;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Handler
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        public readonly ICategoryRepository categoryRepository;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = Category.Create(Guid.NewGuid(), request.Name);
            await categoryRepository.AddAsync(category);
            return category.Id;
        }
    }
}
