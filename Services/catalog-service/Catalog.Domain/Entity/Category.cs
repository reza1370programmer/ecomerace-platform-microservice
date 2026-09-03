

namespace Catalog.Domain.Entity
{
    public class Category
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public DateTime? CreateAt { get; private set; }
        public DateTime? UpdateAt { get; private set; }

        public Category()
        {

        }

        private Category(Guid id, string name)
        {

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Name Of Category cannot be null or empty");
            Id = id;
            Name = name;
            CreateAt = DateTime.UtcNow;
        }
        public static Category Create(Guid id, string name) { return new Category(id, name); }
        public void Update(string name)
        {

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Name Of Category cannot be null or empty");
            Name = name;
            UpdateAt = DateTime.UtcNow;
        }
    }
}
