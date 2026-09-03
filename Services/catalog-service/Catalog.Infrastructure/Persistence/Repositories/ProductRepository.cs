
using Catalog.Domain.Entity;
using Catalog.Domain.Repositories;
using Catalog.Infrastructure.Persistence.EfCore;
using Microsoft.EntityFrameworkCore;


namespace Catalog.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _context;

        public ProductRepository(CatalogContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
        {
            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Product>> FindAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Products.Include(x => x.Category).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Product>> FindByCategoryId(Guid categoryId, CancellationToken cancellationToken = default)
        {
            return await _context.Products.Include(x => x.Category).Where(x => x.CategoryId == categoryId).ToListAsync(cancellationToken);
        }

        public async Task<Product> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Product> FindBySKUAsync(string sku, CancellationToken cancellationToken = default)
        {
            return await _context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Sku.Value == sku, cancellationToken);
        }

        public async Task RemoveAsync(Guid Id, CancellationToken cancellationToken = default)
        {
            var product = await FindByIdAsync(Id, cancellationToken);
            if(product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
