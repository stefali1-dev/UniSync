using UniSync.Domain.Common;

namespace UniSync.Domain.Entities
{
    public class Category : AuditableEntity
    {
        private Category(string categoryName)
        {
            CategoryId = Guid.NewGuid();
            CategoryName = categoryName;
        }

        public Guid CategoryId { get; private set; }
        public string CategoryName { get; private set; } = string.Empty;
        public List<Event>? Events { get; private set; }

        public static Result<Category> Create(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                return Result<Category>.Failure("Category Name is required.");
            }
            return Result<Category>.Success(new Category(categoryName));
        }

        public void AttachEvent(Event eventItem)
        {
            if (Events == null)
            {
                Events = new List<Event>();
            }
            Events.Add(eventItem);
        }
    }
}
