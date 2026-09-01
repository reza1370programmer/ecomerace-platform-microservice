

using Catalog.Domain.Entity;
using Catalog.Domain.Repositories;
using Catalog.Infrastructure.Persistence.EfCore;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CatalogContext _context;

        public CategoryRepository(CatalogContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Category category, CancellationToken cancellationToken = default)
        {
            await _context.Categories.AddAsync(category, cancellationToken);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var category = await FindByIdAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<IEnumerable<Category>> FindAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Categories.ToListAsync(cancellationToken);
        }

        public async Task<Category> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(Category category, CancellationToken cancellationToken = default)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
