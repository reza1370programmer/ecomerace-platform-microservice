

using Catalog.Domain.Entity;

namespace Catalog.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Product> FindBySKUAsync(string sku, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> FindByCategoryId(Guid categoryId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> FindAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Product product, CancellationToken cancellationToken = default);
        Task UpdateAsync(Product product, CancellationToken cancellationToken = default);
        Task RemoveAsync(Guid Id, CancellationToken cancellationToken = default);
    }
}
