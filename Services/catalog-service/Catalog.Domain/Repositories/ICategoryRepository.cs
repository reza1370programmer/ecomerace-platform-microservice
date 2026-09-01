

using Catalog.Domain.Entity;

namespace Catalog.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Category>> FindAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Category category, CancellationToken cancellationToken = default);
        Task UpdateAsync(Category category, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
