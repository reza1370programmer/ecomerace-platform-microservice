

using Identity.Domain.Entities;
using Identity.Domain.Repositories;
using Identity.Domain.ValueObjects;
using Identity.Infrastructure.Persistence.Efcore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Persistence.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IdentityContext _context;

        public RefreshTokenRepository(IdentityContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken> GetRefreshToken(string refreshToken, CancellationToken cancellationToken = default)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshToken, cancellationToken);
        }

        public async Task<IEnumerable<RefreshToken>> GetByUserId(UserId userId, CancellationToken cancellationToken = default)
        {
            return await _context.RefreshTokens.Where(x => x.UserId == userId).ToListAsync(cancellationToken);

        }

        public async Task AddRefreshToken(RefreshToken refreshToken, CancellationToken cancellationToken = default)
        {
            await _context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateRefreshToken(RefreshToken refreshToken, CancellationToken cancellationToken = default)
        {
            _context.RefreshTokens.Update(refreshToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RevokeAllRefreshTokens(UserId userId, CancellationToken cancellationToken = default)
        {
            var allUSersTokens = await _context.RefreshTokens.Where(x => x.UserId == userId).ToListAsync(cancellationToken);
            foreach (var refreshToken in allUSersTokens)
            {
                refreshToken.Revoke();
            }
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
