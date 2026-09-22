

using Identity.Domain.Entities;
using Identity.Domain.ValueObjects;

namespace Identity.Domain.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetRefreshToken(string refreshToken, CancellationToken cancellationToken = default);
        Task<IEnumerable<RefreshToken>> GetByUserId(UserId userId, CancellationToken cancellationToken = default);
        Task AddRefreshToken(RefreshToken refreshToken, CancellationToken cancellationToken = default);
        Task UpdateRefreshToken(RefreshToken refreshToken, CancellationToken cancellationToken = default);
        Task RevokeAllRefreshTokens(UserId userId, CancellationToken cancellationToken = default);
    }
}
