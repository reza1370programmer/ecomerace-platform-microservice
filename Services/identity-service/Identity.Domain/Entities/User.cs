

using Domain.ValueObjects;
using Identity.Domain.ValueObjects;

namespace Identity.Domain.Entities
{
    public class User
    {
        public UserId Id { get; private set; }
        public Email Email { get; private set; }
        public PasswordHash PasswordHash { get; private set; }
        public List<string> Roles { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreateAt { get; private set; }
        public DateTime? UpdateAt { get; private set; }

        public User()
        {

        }
        private User(UserId id, Email email, PasswordHash passwordHash)
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
            Roles = new List<string>() { "user" };
            IsActive = true;
            CreateAt = DateTime.UtcNow;
        }
        public static User Create(Email email, PasswordHash passwordHash)
        {
            return new User(UserId.CreateNew(), email, passwordHash);
        }
        public void ChangePassword(PasswordHash newPasswordHash)
        {
            PasswordHash = newPasswordHash;
            UpdateAt = DateTime.UtcNow;
        }
        public void AddRole(string role)
        {
            if (!Roles.Contains(role)) Roles.Add(role);
        }
        public void RemoveRole(string role)
        {
            if (Roles.Contains(role)) Roles.Remove(role);
            UpdateAt = DateTime.UtcNow;
        }
        public void Deactivate()
        {
            IsActive = false;
            UpdateAt= DateTime.UtcNow;
        }
        public void Activate()
        {
            IsActive = true;
            UpdateAt= DateTime.UtcNow;
        }


    }
}
