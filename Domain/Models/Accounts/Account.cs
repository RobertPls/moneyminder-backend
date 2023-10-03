using Domain.ValueObjects;
using SharedKernel.Core;

namespace Domain.Models.Accounts
{
    public class Account: AggregateRoot<Guid>
    {
        public GuidValue UserId { get; private set; }

        public AccountNameValue Name { get; private set; }

        public DescriptionValue Description { get; private set; }
        
        public MoneyValue Balance { get; private set; }

        public Account(Guid userId, string name, string description) 
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Name = name;
            Description = description;
            Balance = 0;
        }
    }
}
