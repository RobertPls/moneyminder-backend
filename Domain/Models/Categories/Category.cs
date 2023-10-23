using Domain.ValueObjects;
using SharedKernel.Core;

namespace Domain.Models.Categories
{
    public class Category : AggregateRoot<Guid>
    {
        public Guid UserId { get; private set; }
        public CategoryNameValue Name { get; private set; }
        public bool IsDefault{ get; private set; }


        public Category(Guid userId, string name, bool isDefault) 
        {
            if (userId == Guid.Empty)
            {
                throw new BussinessRuleValidationException("The user cannot be empty");
            }

            Id = Guid.NewGuid();
            UserId = userId;
            Name = name;
            IsDefault = isDefault;
        }

        public Category() { }

        public void UpdateName(string name)
        {
            Name = name;
        }
    }
}
