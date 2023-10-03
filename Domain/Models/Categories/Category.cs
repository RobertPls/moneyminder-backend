using Domain.ValueObjects;
using SharedKernel.Core;

namespace Domain.Models.Categories
{
    public class Category : AggregateRoot<Guid>
    {
        public GuidValue UserId { get; private set; }
        public CategoryNameValue Name { get; private set; }

        public Category(Guid userId, string name) 
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Name = name;
        }
    }
}
