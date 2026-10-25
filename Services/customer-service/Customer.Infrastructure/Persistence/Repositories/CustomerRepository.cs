
using Customer.Domain.Repositories;
using Customer.Infrastructure.Persistence.Ef_Core;
using Microsoft.EntityFrameworkCore;

namespace Customer.Infrastructure.Persistence.Repositories;

public class CustomerRepository:ICustomerRepository
{
    
    private readonly CustomerDbContext _dbContext;

    public CustomerRepository(CustomerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Domain.Entities.Customer?> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
       return await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<Domain.Entities.Customer?> GetCustomerByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Customers.FirstOrDefaultAsync(x=>x.Email.Value == email.ToLowerInvariant(), cancellationToken);
    }

    public  async Task AddCustomerAsync(Domain.Entities.Customer customer, CancellationToken cancellationToken = default)
    {
      await _dbContext.Customers.AddAsync(customer, cancellationToken);
      await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public  async Task UpdateCustomerAsync(Domain.Entities.Customer customer, CancellationToken cancellationToken = default)
    {
        _dbContext.Customers.Update(customer);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}