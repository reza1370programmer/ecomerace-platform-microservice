

using Domain.ValueObjects;
using Identity.Domain.Entities;
using Identity.Domain.ValueObjects;

namespace Identity.Domain.Repositories
{
    public interface IUserRespository
    {
        Task<User?> GetUSerById(UserId userId, CancellationToken cancellationToken = default);
        Task<User?> GetUSerByEmail(Email email, CancellationToken cancellationToken = default);
        Task<bool> UserEmailExists(Email email, CancellationToken cancellationToken = default);
        Task AddUser(User user, CancellationToken cancellationToken = default);
        Task UpdateUser(User user, CancellationToken cancellationToken = default);
        Task<IEnumerable<User>> GetAllUsers(CancellationToken cancellationToken = default);
    }
}
