using Domain.ValueObjects;
using SharedKernel.Core;

namespace Domain.Models.Categories
{
    public class Category : AggregateRoot<Guid>
    {
        public Guid UserProfileId { get; private set; }
        public CategoryNameValue Name { get; private set; }
        public bool IsDefault{ get; private set; }


        public Category(Guid userProfileId, string name, bool isDefault) 
        {
            if (userProfileId == Guid.Empty)
            {
                throw new BussinessRuleValidationException("The user cannot be empty");
            }

            Id = Guid.NewGuid();
            UserProfileId = userProfileId;
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
