

using Identity.Domain.ValueObjects;

namespace Identity.Domain.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; private set; }

        public UserId UserId { get; private set; }
        public string Token { get; private set; }
        public DateTime ExpiresAt { get; private set; }
        public DateTime CreateAt { get; private set; }
        public bool IsRevoked { get; private set; }//ینی تغییری در محتوای توکن شده یا نه
        public DateTime? RevokedAt { get; private set; }

        private RefreshToken() { }

        private RefreshToken(UserId userId, string token, DateTime expiresAt)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Token = token;
            ExpiresAt = expiresAt;
            IsRevoked = false;
        }
        public static RefreshToken Create(UserId userId, string token, int epirationDays = 7)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException("Token Cannot be  Empty Token");
            }

            var expiresAt = DateTime.UtcNow.AddDays(epirationDays);
            return new RefreshToken(userId, token, expiresAt);
        }

        public bool IsExpired() => ExpiresAt < DateTime.UtcNow;

        public bool IsVaild() => !IsExpired() && !IsRevoked;

        public void Revoke()
        {
            IsRevoked = true;
            RevokedAt = DateTime.UtcNow;
        }

    }
}
