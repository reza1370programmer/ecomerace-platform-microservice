

using Domain.ValueObjects;
using Identity.Domain.Entities;
using Identity.Domain.Repositories;
using Identity.Domain.ValueObjects;
using Identity.Infrastructure.Persistence.Efcore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Persistence.Repositories
{
    public class UserRespository : IUserRespository
    {
        private readonly IdentityContext _context;

        public UserRespository(IdentityContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUSerById(UserId userId, CancellationToken cancellationToken = default)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        }

        public async Task<User?> GetUSerByEmail(Email email, CancellationToken cancellationToken = default)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

        }

        public async Task<bool> UserEmailExists(Email email, CancellationToken cancellationToken = default)
        {
            return await _context.Users.AnyAsync(x => x.Email == email, cancellationToken);
        }

        public async Task AddUser(User user, CancellationToken cancellationToken = default)
        {
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateUser(User user, CancellationToken cancellationToken = default)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<User>> GetAllUsers(CancellationToken cancellationToken = default)
        {
            return await _context.Users.ToListAsync(cancellationToken);
        }
    }
}
