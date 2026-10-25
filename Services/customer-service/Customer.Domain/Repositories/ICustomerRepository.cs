namespace Customer.Domain.Repositories;

public interface ICustomerRepository
{
    Task<Entities.Customer?> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Entities.Customer?> GetCustomerByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task AddCustomerAsync(Entities.Customer customer, CancellationToken cancellationToken = default);
    Task UpdateCustomerAsync(Entities.Customer customer, CancellationToken cancellationToken = default);
}